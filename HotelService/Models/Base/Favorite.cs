#nullable disable

namespace HotelService.Models.Base
{
    public class Favorite
    {
        public string ClientId { get; set; }
        public int ServiceId { get; set; }
        public bool? ShowState { get; set; }

        public User Client { get; set; }
        public Service Service { get; set; }
    }
}
