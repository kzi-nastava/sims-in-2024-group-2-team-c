using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.DTO
{
    public class StatusDTO
    {
        public int Year {  get; set; }
        public string Language { get; set; }
        public int NumberOfCompletedTours { get; set; }
        public float AverageScore { get; set; }

        public StatusDTO() { }
    }
}
