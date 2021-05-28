using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace HotelService.Models.Base
{
    public class Service
    {
        public Service()
        {
            Feedbacks = new HashSet<Feedback>();
            Requests = new HashSet<Request>();
        }

        public int ServiceId { get; set; }
        [Required]
        public int CategoryId { get; set; }

        [Required, RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$"), StringLength(30, MinimumLength = 3)]
        public string Title { get; set; }


        [StringLength(100, MinimumLength = 3)]
        public string Subtitle { get; set; }


        [StringLength(100, MinimumLength = 3)]
        public string Description { get; set; }

        public decimal Cost { get; set; }


        [RegularExpression(@"^[A-Z]+[a-zA-Z0-9""'\s-]*$"), StringLength(255, MinimumLength = 3)]
        public string Type { get; set; }

        [Range(1, 5)]
        public double? Rating { get; set; }
        public bool? AvailableState { get; set; }
        public bool? RepeatState { get; set; }
        public string EmployeeId { get; set; }
        public string ImagePath { get; set; }
        public DateTime AddedDate { get; set; }

        public ServiceCategory Category { get; set; }
        public User Employee { get; set; }
        public ICollection<Feedback> Feedbacks { get; set; }
        public ICollection<Request> Requests { get; set; }
    }
}
