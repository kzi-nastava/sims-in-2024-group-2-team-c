using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.DTO
{
    public class ComplexTouristRequestDTO
    {

        public string Activity { get; set; }
        public int Number { get; set; }
        public int TourRequestId { get; set; }
        public DateTime AcceptedDate { get; set; }

        public ComplexTouristRequestDTO() { }
        public ComplexTouristRequestDTO(string activity, int number, int tourRequestId,DateTime acepptedDate)
        {
            Activity = activity;
            Number = number;
            TourRequestId = tourRequestId;
            AcceptedDate = acepptedDate;
        }



    }
}
