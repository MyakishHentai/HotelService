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
        public string Name { get; set; }
        public string Descriprion { get; set; }
        public int? ImageId { get; set; }
        public decimal Cost { get; set; }
        public string Type { get; set; }
        public double? Rating { get; set; }
        public bool? RepeatState { get; set; }
        public int? ResponsWorker { get; set; }

        public virtual CategoriesService Category { get; set; }
        public virtual Image Image { get; set; }
        public virtual User ResponsWorkerNavigation { get; set; }
        public virtual ICollection<Feedback> Feedbacks { get; set; }
        public virtual ICollection<Request> Requests { get; set; }
    }
}
