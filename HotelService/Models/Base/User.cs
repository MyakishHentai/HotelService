using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace HotelService.Models.Base
{
    public class User : IdentityUser
    {
        public User() : base()
        {
            Feedbacks = new HashSet<Feedback>();
            RoomContracts = new HashSet<RoomContract>();
            ServiceRequests = new HashSet<ServiceRequest>();
            Services = new HashSet<Service>();
        }

        [PersonalData]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Comment("Дата рождения")]
        public DateTime? BirthDate { get; set; }


        [PersonalData]
        [Required(ErrorMessage = "Не указано имя"), RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$"), StringLength(30, MinimumLength = 3)]
        [Column(TypeName = "nvarchar(30)")]
        [Comment("Имя")]
        public string FirstName { get; set; }


        [PersonalData]
        [Required(ErrorMessage = "Не указана фамилия"),  RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$"), StringLength(30, MinimumLength = 3)]
        [Column(TypeName = "nvarchar(50)")]
        [Comment("Фамилия")]
        public string LastName { get; set; }


        [PersonalData]
        [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$"), StringLength(50, MinimumLength = 3)]
        [Column(TypeName = "nvarchar(50)")]
        [Comment("Отчество/Матчество")]
        public string Patronymic { get; set; }


        [PersonalData]
        [Column(TypeName = "nvarchar(10)")]
        [Comment("Пол")]
        public string Gender { get; set; }

        [Required, RegularExpression(@"^[0-9""'\s-]*$"), StringLength(20, MinimumLength = 10)]
        [PersonalData]        
        [Column(TypeName = "nvarchar(20)")]
        [Comment("Паспорт")]
        public string Passport { get; set; }


        [PersonalData]
        [Comment("Является инностранцем")]
        public bool? ForeignerStatus { get; set; }


        [PersonalData]
        [DataType(DataType.DateTime), DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Comment("Дата регистрации")]
        public DateTime RegistrationDate { get; set; }


        [PersonalData]        
        [Column(TypeName = "nvarchar(1000)")]
        [Comment("Путь изображения")]
        public string ImagePath { get; set; }

        public Building Building { get; set; }
        public ICollection<Feedback> Feedbacks { get; set; }
        public ICollection<RoomContract> RoomContracts { get; set; }
        public ICollection<ServiceRequest> ServiceRequests { get; set; }
        public ICollection<Service> Services { get; set; }
    }

}
