using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.DTO
{
    public class TouristRequestDTO
    {

        public string Activity { get; set; }
        public int Number { get; set; }
        public int TourRequestId { get; set; }

        public TouristRequestDTO() { }
        public TouristRequestDTO(string activity,int number,int tourRequestId)
        {
            Activity = activity;
            Number = number;
            TourRequestId = tourRequestId;
        }


    }
}
