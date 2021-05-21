using System;
using System.Collections.Generic;

#nullable disable

namespace HotelService.Models.Base
{
    public class Article
    {
        public int ArticleId { get; set; }
        public string Title { get; set; }
        public string Review { get; set; }
        public DateTime WritingDate { get; set; }
    }
}
