using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Model
{
    public class AvailableDateDisplay
    {

        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }

        public AvailableDateDisplay(DateTime checkIn, DateTime checkOut)
        {
            CheckIn = checkIn;
            CheckOut = checkOut;
        }

    }
}
