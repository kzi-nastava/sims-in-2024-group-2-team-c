using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using BookingApp.Serializer;

namespace BookingApp.Model
{
    public class OwnerStatistics 
    {
        public int Id { get; set; } 
        public Accommodation Accommodation { get; set; }


        public Dictionary<int, int> ReservationsByYear { get; set; }
        public Dictionary<int, int> CancellationsByYear { get; set; }
        public Dictionary<int, int> DelaysByYear { get; set; }

        public Dictionary<int, Dictionary<int, int>> ReservationsByMonth { get; set; }
        public Dictionary<int, Dictionary<int, int>> CancellationsByMonth { get; set; }
        public Dictionary<int, Dictionary<int, int>> DelaysByMonth { get; set; }

        public double OccupancyRate { get; set; }

    }
}
