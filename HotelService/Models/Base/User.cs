using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

#nullable disable

namespace HotelService.Models.Base
{
    public class User : IdentityUser
    {
        public User()
        {
            Articles = new HashSet<Article>();
            Favorites = new HashSet<Favorite>();
            Feedbacks = new HashSet<Feedback>();
            RoomContracts = new HashSet<RoomContract>();
            ServiceCategories = new HashSet<ServiceCategory>();
        }

        [Required(ErrorMessage = "Не указано {0}")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$", ErrorMessage = "Недопустимые символы")]
        [StringLength(30, MinimumLength = 3,
            ErrorMessage = "{0} должно содержать хотя бы {2} и максимум {1} символов.")]
        [Display(Name = "Имя")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Не указана {0}")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$", ErrorMessage = "Недопустимые символы")]
        [StringLength(50, MinimumLength = 3,
            ErrorMessage = "{0} должна содержать хотя бы {2} и максимум {1} символов.")]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$", ErrorMessage = "Недопустимые символы")]
        [StringLength(50, MinimumLength = 3,
            ErrorMessage = "{0} должно содержать хотя бы {2} и максимум {1} символов.")]
        [Display(Name = "Отчество")]
        public string Patronymic { get; set; }

        [Display(Name = "Пол")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Не указан {0}")]
        [RegularExpression(@"^[0-9""'\s-]*$", ErrorMessage = "Недопустимые символы")]
        [StringLength(20, MinimumLength = 10,
            ErrorMessage = "{0} должен содержать хотя бы {2} и максимум {1} символов.")]
        [Display(Name = "Паспорт")]
        public string Passport { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Дата рождения")]
        public DateTime? BirthDate { get; set; }

        [Display(Name = "Является иностранцем?")]
        public bool? ForeignerStatus { get; set; }

        [Display(Name = "Дата регистрации")]
        public DateTime RegistrationDate { get; set; }

        [Display(Name = "Изображение профиля")]
        [DataType(DataType.ImageUrl)]
        public string ImagePath { get; set; }

        public Building Building { get; set; }
        public ICollection<Article> Articles { get; set; }
        public ICollection<Favorite> Favorites { get; set; }
        public ICollection<Feedback> Feedbacks { get; set; }
        public ICollection<RoomContract> RoomContracts { get; set; }
        public ICollection<ServiceCategory> ServiceCategories { get; set; }
    }

}
