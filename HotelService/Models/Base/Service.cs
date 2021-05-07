using System;
using System.Collections.Generic;

#nullable disable

namespace HotelService.Models.Base
{
    public partial class Service
    {
        public Service()
        {
            Feedbacks = new HashSet<Feedback>();
            Requests = new HashSet<Request>();
        }

        public int ServiceId { get; set; }
        public int CategoryId { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Descriprion { get; set; }
        public decimal Cost { get; set; }
        public string Type { get; set; }
        public double? Rating { get; set; }
        public bool? AvailableState { get; set; }
        public bool? RepeatState { get; set; }
        public string ResponsWorker { get; set; }
        public string ImagePath { get; set; }
        public DateTime AddedDate { get; set; }

        public virtual ServiceCategory Category { get; set; }
        public virtual User ResponsWorkerNavigation { get; set; }
        public virtual ICollection<Feedback> Feedbacks { get; set; }
        public virtual ICollection<Request> Requests { get; set; }
    }
}
