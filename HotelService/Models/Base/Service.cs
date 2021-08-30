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
            CostChanges = new HashSet<CostChange>();
            Favorites = new HashSet<Favorite>();
            Feedbacks = new HashSet<Feedback>();
            Requests = new HashSet<Request>();
        }

        public int Id { get; set; }
        [Display(Name = "Категория")]
        public int ServiceCategoryId { get; set; }

        [Display(Name = "Заголовок")]
        [RegularExpression(@"^[А-я0-9A-z]*$")]
        [StringLength(20, MinimumLength = 10, ErrorMessage = "{0} должна содержать хотя бы {2} и максимум {1} символов.")]
        [Required(ErrorMessage = "Имя обязательно")]
        public string Title { get; set; }

        [Display(Name = "Подзаголовок")]
        public string Subtitle { get; set; }

        [Display(Name = "Описание")]
        public string Description { get; set; }
        [Display(Name = "Изображение")]
        public string ImagePath { get; set; }
        [Display(Name = "Стоимость")]
        public decimal Cost { get; set; }
        public double? Rating { get; set; }
        [Display(Name = "Доступность на текущий момент")]
        public bool AvailableState { get; set; }
        [Display(Name = "Возможность повторения")]
        public bool RepeatState { get; set; }
        public DateTime AddedDate { get; set; }

        public ServiceCategory ServiceCategory { get; set; }
        public ICollection<CostChange> CostChanges { get; set; }
        public ICollection<Favorite> Favorites { get; set; }
        public ICollection<Feedback> Feedbacks { get; set; }
        public ICollection<Request> Requests { get; set; }
    }
}
