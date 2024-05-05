using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.DTO
{
    public class HomeTourDTO
    {
        public int TourId { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public List<string> Images { get; set; }


        

        public HomeTourDTO() { }

        public HomeTourDTO(int tourId, string name, string location, List<string> images) { 
            TourId = tourId;
            Name = name;
            Location = location;
            Images = images;

            
        }



    }
}
