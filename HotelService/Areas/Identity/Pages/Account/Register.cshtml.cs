using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using HotelService.Models.Base;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HotelService.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly RoleManager<Role> m_RoleManager;
        private readonly SignInManager<User> m_SignInManager;
        private readonly UserManager<User> m_UserManager;

        public RegisterModel(
            UserManager<User> userManager,
            RoleManager<Role> roleManager,
            SignInManager<User> signInManager)
        {
            m_UserManager = userManager;
            m_RoleManager = roleManager;
            m_SignInManager = signInManager;
        }

        [BindProperty] public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (User.Identity.IsAuthenticated) Response.Redirect("/Home");
            ReturnUrl = returnUrl;
            ExternalLogins = (await m_SignInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            Url.Content("~/");
            ExternalLogins = (await m_SignInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (!ModelState.IsValid) return Page();
            var NewUser = new User
            {
                UserName = Input.Email,
                Email = Input.Email,
                RegistrationDate = DateTime.Now,
                Passport = Input.Passport,
                FirstName = Input.FirstName,
                LastName = Input.LastName
            };

            var Result = await m_UserManager.CreateAsync(NewUser, Input.Password);

            if (Result.Succeeded)
            {
                var UserNow = await m_UserManager.FindByNameAsync(NewUser.UserName);
                if (!await m_RoleManager.RoleExistsAsync(Input.Role.ToString())) await m_RoleManager.CreateAsync(new Role(Input.Role.ToString()));
                await m_UserManager.AddToRoleAsync(UserNow, Input.Role.ToString());
                await m_SignInManager.PasswordSignInAsync(NewUser, Input.Password, false, false);

                Response.Redirect("/Home");
            }

            foreach (var Error in Result.Errors) ModelState.AddModelError(string.Empty, Error.Description);

            // If we got this far, something failed, redisplay form
            return Page();
        }

        public class InputModel
        {
            [Required(ErrorMessage = "Имя обязательно")]
            [DataType(DataType.Text)]
            [Display(Name = "Имя")]
            public string FirstName { get; set; }


            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Фамилия")]
            public string LastName { get; set; }

            [Required]
            [RegularExpression(@"^[0-9]*$")]
            [StringLength(20, MinimumLength = 10)]
            [DataType(DataType.Text)]
            [Display(Name = "Паспорт")]
            public string Passport { get; set; }

            [Required(ErrorMessage = "Select Role")]
            [Display(Name = "Права доступа")]
            public RoleType Role { get; set; }


            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.",
                MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Пароль")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Подтвердите пароль")]
            [Compare("Password", ErrorMessage = "Пароли не совпадают.")]
            public string ConfirmPassword { get; set; }
        }
    }
}