using System;

#nullable disable

namespace HotelService.Models.Base
{
    public class ServiceRequest
    {
        public string EmployeeId { get; set; }
        public int ServiceId { get; set; }
        public int BasketId { get; set; }
        public DateTime OrderTakeDate { get; set; }
        public DateTime? OrderDoneDate { get; set; }
        public string Comment { get; set; }

        public User Employee { get; set; }
        public Request Request { get; set; }
    }
}
