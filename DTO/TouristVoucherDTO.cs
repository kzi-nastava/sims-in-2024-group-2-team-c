using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.DTO
{
    public class TouristVoucherDTO
    {

        public int TourId { get; set; }

        public string TourName { get; set; }
        public int TouristId { get; set; }

        public DateTime ExpirationDate { get; set; }

        public bool IsUniversal { get; set; }

        public TouristVoucherDTO() {
            TourName = string.Empty;
        }

        public TouristVoucherDTO(int tourId,string tourName, int touristId, DateTime expirationDate, bool isUniversal)
        {
            TourId = tourId;
            TourName = tourName;
            TouristId = touristId;
            ExpirationDate = expirationDate;
            IsUniversal = isUniversal;
        }
    }
}
