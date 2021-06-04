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
            Favorites = new HashSet<Favorite>();
            Feedbacks = new HashSet<Feedback>();
            PriceChanges = new HashSet<PriceChange>();
            Requests = new HashSet<Request>();
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "Не указана {0}")]
        [Display(Name = "Категория услуги")]
        public int ServiceCategoryId { get; set; }

        [Required(ErrorMessage = "Не указан {0}")]
        [RegularExpression(@"^[А-Я]+[а-яА-Я\-\s]*$", ErrorMessage = "Недопустимые символы")]
        [StringLength(50, MinimumLength = 3,
            ErrorMessage = "{0} должен содержать хотя бы {2} и максимум {1} символов.")]
        [Display(Name = "Заголовок")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Не указан {0}")]
        [RegularExpression(@"^^[А-Я]+[а-яА-Я\-\?!\.,()\d\s]*$", ErrorMessage = "Недопустимые символы")]
        [StringLength(50, MinimumLength = 3,
            ErrorMessage = "{0} должен содержать хотя бы {2} и максимум {1} символов.")]
        [Display(Name = "Подзаголовок")]
        public string Subtitle { get; set; }

        [RegularExpression(@"^[А-Я]+[а-яА-Я-\?!\.,()\d\s]*$", ErrorMessage = "Недопустимые символы")]
        [StringLength(512, MinimumLength = 10,
            ErrorMessage = "{0} должно содержать хотя бы {2} и максимум {1} символов.")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Display(Name = "Изображение")]
        [DataType(DataType.ImageUrl)]
        public string ImagePath { get; set; }

        [Required(ErrorMessage = "Не указана {0}")]
        [Range(0, 99999999, ErrorMessage = "{0} должна находиться в пределах от {1} до {2}.")]
        [Display(Name = "Стоимость")]
        [DataType(DataType.Currency)]
        public decimal Cost { get; set; }

        [Range(1, 10, ErrorMessage = "{0} должен находиться в пределах от {1} до {2}.")]
        [Display(Name = "Рейтинг")]
        [DataType(DataType.Duration)]
        public double? Rating { get; set; }

        [Display(Name = "Сервис доступен на данный момент?")]
        public bool AvailableState { get; set; }

        [Display(Name = "Сервис поодерживает повторное добавление?")]
        public bool RepeatState { get; set; }

        [Display(Name = "Дата добавления")]
        [DisplayFormat(DataFormatString = "{dd.MM.yyyy:0}", ApplyFormatInEditMode = true)]
        [DataType(DataType.DateTime)]
        public DateTime AddedDate { get; set; }

        public ServiceCategory ServiceCategory { get; set; }
        public ICollection<Favorite> Favorites { get; set; }
        public ICollection<Feedback> Feedbacks { get; set; }
        public ICollection<PriceChange> PriceChanges { get; set; }
        public ICollection<Request> Requests { get; set; }
    }
}