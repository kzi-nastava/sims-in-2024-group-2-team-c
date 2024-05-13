using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.DTO
{
    public class StatisticTourRequestDTO
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int MonthNumberOfTours { get; set; }
        public int YearNumberOfTours { get; set; }
        public string Location { get; set; }
        public string Language { get; set; }
        public StatisticTourRequestDTO() { }
    }
}
