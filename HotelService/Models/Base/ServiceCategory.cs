using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace HotelService.Models.Base
{
    public class ServiceCategory
    {
        public ServiceCategory()
        {
            InverseSubCategory = new HashSet<ServiceCategory>();
            Services = new HashSet<Service>();
        }

        public int Id { get; set; }

        [Display(Name = "Подкатегория")]
        public int? SubCategoryId { get; set; }

        [Display(Name = "Система контроля")]
        public string SystemEmployeeId { get; set; }

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

        //[RegularExpression(@"^[А-Я]+[а-яА-Я-\?!\.,()\d\s]*$", ErrorMessage = "Недопустимые символы")]
        [StringLength(512, MinimumLength = 10,
            ErrorMessage = "{0} должно содержать хотя бы {2} и максимум {1} символов.")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Display(Name = "Изображение")]
        [DataType(DataType.ImageUrl)]
        public string ImagePath { get; set; }

        [Display(Name = "Категория услуг доступна на данный момент?")]
        public bool AvailableState { get; set; }

        public ServiceCategory SubCategory { get; set; }
        public User SystemEmployee { get; set; }
        public ICollection<ServiceCategory> InverseSubCategory { get; set; }
        public ICollection<Service> Services { get; set; }
    }
}
