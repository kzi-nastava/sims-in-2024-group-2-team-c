using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.DTO
{
    public class AccommodationDTO
    {

        
        public string Name { get; set; }
        public Location Location { get; set; }
        public string Type { get; set; }
        public int MaxGuests { get; set; }
        public int MinBookingDays { get; set; }
        public int CancellationDays { get; set; }
        public Owner Owner { get; set; }
        public List<string> Images { get; set; }

        public string LocationDetails { get; set; }

        public AccommodationDTO()
        {
            Images = new List<string>();
        }


    }
}
