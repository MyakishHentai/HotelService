using System;
using System.Collections.Generic;

#nullable disable

namespace HotelService.Models.Base
{
    public class RequestState
    {
        public int RequestId { get; set; }
        public int StateId { get; set; }
        public DateTime ChangeDate { get; set; }
        public string Comment { get; set; }

        public Request Request { get; set; }
        public State State { get; set; }
    }
}
