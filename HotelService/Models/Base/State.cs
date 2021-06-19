using System;
using System.Collections.Generic;

#nullable disable

namespace HotelService.Models.Base
{
    public class State
    {
        public State()
        {
            RequestStates = new HashSet<RequestState>();
        }

        public int Id { get; set; }
        public string Value { get; set; }

        public ICollection<RequestState> RequestStates { get; set; }
    }
}
