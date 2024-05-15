using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Model
{
    public class AccommodationRatingReport 
    {
        public Accommodation Accommodation { get; set; }
        public double CleanlinessRating { get; set; }
        public double PolitenessRating { get; set; }
        public double AverageRating { get; set; }
    }
}
