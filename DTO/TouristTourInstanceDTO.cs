using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.DTO
{
    public class TouristTourInstanceDTO
    {

        public int Id { get; set; }
        public int IdTour { get; set; }
        public int MaxTourists { get; set; }
        public int ReservedTourists { get; set; }

        public DateTime Date { get; set; }

        public TouristTourInstanceDTO() { } 

        public TouristTourInstanceDTO(int id, int idTour, int maxTourists, int reservedTourists, DateTime date)
        {
            Id = id;
            IdTour = idTour;
            MaxTourists = maxTourists;
            ReservedTourists = reservedTourists;
            Date = date;
        }
    }
}
