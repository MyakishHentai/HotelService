using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using HotelService.Models.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HotelService.Models.ViewModels.Admin
{
    public class CreateModel
    {
        //public User User { get; set; }
        public string Id { get; set; }
        public string UserName { get; set; }


        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? BirthDate { get; set; }


       
        [Required(ErrorMessage = "Не указано имя"), RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$"), StringLength(30, MinimumLength = 3)]
        public string FirstName { get; set; }


        [Required(ErrorMessage = "Не указана фамилия"), RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$"), StringLength(30, MinimumLength = 3)]
        public string LastName { get; set; }


        [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$"), StringLength(50, MinimumLength = 3)]
        public string Patronymic { get; set; }


        public string Gender { get; set; }

        [Required, RegularExpression(@"^[0-9""'\s-]*$"), StringLength(20, MinimumLength = 10)]
        public string Passport { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public IEnumerable<string> Roles { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.",
            MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }


    public class RoleEditModel
    {
        public Role Role { get; set; }
        public IEnumerable<User> Members { get; set; }
        public IEnumerable<User> NonMembers { get; set; }
    }

    public class RoleModificationModel
    {
        [Required]
        public string RoleName { get; set; }
        public string RoleId { get; set; }
        public string[] IdsToAdd { get; set; }
        public string[] IdsToDelete { get; set; }
    }
}

