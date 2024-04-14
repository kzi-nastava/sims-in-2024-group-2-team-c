using System;
using System.Collections.Generic;
using System.Linq;
using BookingApp.Serializer;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace BookingApp.Model
{
    public class TourVoucher : ISerializable
    {

        public int Id { get; set; }
        public int TourId { get; set;}

        public int TouristId {  get; set; }

        public DateTime ExpirationDate { get; set; }


        public TourVoucher() { }


        public TourVoucher(int id, int tourId, int touristId, DateTime expirationDate)
        {
            Id = id;
            TourId = tourId;
            TouristId = touristId;
            ExpirationDate = expirationDate;
        }

        public string[] ToCSV()
        {
            throw new NotImplementedException();
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            TourId = Convert.ToInt32(values[1]);
            TouristId = Convert.ToInt32(values[2]);
            // Date = DateTime.Parse(values[6]);
            ExpirationDate = DateTime.ParseExact(values[6].Trim(), "dd.MM.yyyy. HH:mm:ss", CultureInfo.InvariantCulture);
        }
    }
}
