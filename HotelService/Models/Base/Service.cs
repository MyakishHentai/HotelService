using System;
using System.Collections.Generic;

#nullable disable

namespace HotelService.Models.Base
{
    public class Service
    {
        public Service()
        {
            CostChanges = new HashSet<CostChange>();
            Favorites = new HashSet<Favorite>();
            Feedbacks = new HashSet<Feedback>();
            Requests = new HashSet<Request>();
        }

        public int Id { get; set; }
        public int ServiceCategoryId { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public decimal Cost { get; set; }
        public double? Rating { get; set; }
        public bool? AvailableState { get; set; }
        public bool? RepeatState { get; set; }
        public DateTime AddedDate { get; set; }

        public ServiceCategory ServiceCategory { get; set; }
        public ICollection<CostChange> CostChanges { get; set; }
        public ICollection<Favorite> Favorites { get; set; }
        public ICollection<Feedback> Feedbacks { get; set; }
        public ICollection<Request> Requests { get; set; }
    }
}
