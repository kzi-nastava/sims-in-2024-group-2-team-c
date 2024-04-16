using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.DTO
{
    public class AddedNotificationDTO
    {

        public string Name {  get; set; }
        public string TourName { get; set; }

        public AddedNotificationDTO() {
            Name = string.Empty;
            TourName = string.Empty;
        }

        public AddedNotificationDTO(string name, string tourName)
        {
            Name = name;
            TourName = tourName;
        }


    }
}
