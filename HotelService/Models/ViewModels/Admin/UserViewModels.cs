using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HotelService.Models.Base;

namespace HotelService.Models.ViewModels.Admin
{
    public class CreateModel
    {
        //public User User { get; set; }
        public string Id { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$", ErrorMessage = "Недопустимые символы")]
        [Display(Name = "Имя пользователя")]
        public string UserName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Дата рождения")]
        public DateTime? BirthDate { get; set; }

        [Required(ErrorMessage = "Не указано {0}")]
        //[RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$", ErrorMessage = "Недопустимые символы")]
        [StringLength(30, MinimumLength = 3,
            ErrorMessage = "{0} должно содержать хотя бы {2} и максимум {1} символов.")]
        [Display(Name = "Имя")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Не указана {0}")]
        //[RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$", ErrorMessage = "Недопустимые символы")]
        [StringLength(50, MinimumLength = 3,
            ErrorMessage = "{0} должна содержать хотя бы {2} и максимум {1} символов.")]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$", ErrorMessage = "Недопустимые символы")]
        [StringLength(50, MinimumLength = 3,
            ErrorMessage = "{0} должно содержать хотя бы {2} и максимум {1} символов.")]
        [Display(Name = "Отчество")]
        public string Patronymic { get; set; }

        [Display(Name = "Пол")] public string Gender { get; set; }

        [Required(ErrorMessage = "Не указан {0}")]
        [RegularExpression(@"^[0-9""'\s-]*$", ErrorMessage = "Недопустимые символы")]
        [StringLength(20, MinimumLength = 10,
            ErrorMessage = "{0} должен содержать хотя бы {2} и максимум {1} символов.")]
        [Display(Name = "Паспорт")]
        public string Passport { get; set; }

        [RegularExpression(@"^[0-9""'\s-]*$", ErrorMessage = "Недопустимые символы")]
        [StringLength(20, MinimumLength = 6,
            ErrorMessage = "{0} должен содержать хотя бы {2} и максимум {1} символов.")]
        [Display(Name = "Телефон")]
        public string PhoneNumber { get; set; }

        [Required] [EmailAddress] public string Email { get; set; }

        public IEnumerable<string> Roles { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} должен содержать хотя бы {2} и максимум {1} символов.",
            MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Display(Name = "Изображение профиля")]
        [DataType(DataType.ImageUrl)]
        public string ImagePath { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Подтвердите пароль")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают.")]
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
        [Required] public string RoleName { get; set; }

        public string RoleId { get; set; }
        public string[] IdsToAdd { get; set; }
        public string[] IdsToDelete { get; set; }
    }
}