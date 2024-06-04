using BookingApp.Model;
using System.Collections.Generic;

namespace BookingApp.DTO
{
    public class AccommodationReportDataDTO
    {
        public Accommodation Accommodation { get; set; }
        public Dictionary<string, double> AverageRatings { get; set; }

        // Konstruktor koji inicijalizuje svojstva klase
        public AccommodationReportDataDTO(Accommodation accommodation, Dictionary<string, double> averageRatings)
        {
            Accommodation = accommodation;
            AverageRatings = averageRatings;
        }
    }
}
