using System;

#nullable disable

namespace HotelService.Models.Base
{
    public class Article
    {
        public int Id { get; set; }
        public string AuthorId { get; set; }
        public string Title { get; set; }
        public string Review { get; set; }
        public string ImagePath { get; set; }
        public DateTime WriteDate { get; set; }

        public User Author { get; set; }
    }
}
