using System;
using System.Collections.Generic;
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

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public string Gender { get; set; }
        public string Passport { get; set; }
        public DateTime? BirthDate { get; set; }
        public bool? ForeignerStatus { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string ImagePath { get; set; }

        public Building Building { get; set; }
        public ICollection<Article> Articles { get; set; }
        public ICollection<Favorite> Favorites { get; set; }
        public ICollection<Feedback> Feedbacks { get; set; }
        public ICollection<RoomContract> RoomContracts { get; set; }
        public ICollection<ServiceCategory> ServiceCategories { get; set; }
    }

}
