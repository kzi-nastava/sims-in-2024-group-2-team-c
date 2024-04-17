using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.DTO
{
    public class GuestReservationDTO
    {

        public int Id { get; set; }
        public String Name { get; set; }

        public String Location { get; set; }

        public String Type { get; set; }

        public String ImageUrl { get; set; }

        public DateTime CheckIn { get; set; }

        public DateTime CheckOut { get; set; }




    }
}
