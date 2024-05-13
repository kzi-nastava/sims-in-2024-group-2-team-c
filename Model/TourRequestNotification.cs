using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BookingApp.Model
{
    public class TourRequestNotification : ISerializable
    {

        public int Id { get; set; }
        public int RequestId { get; set; }
        public int TourId { get; set; }

        public int TouristId { get; set; }
        public TourRequestNotification() { }

        public TourRequestNotification(int id, int requestId,int tourId, int touristId)
        {
            Id = id;
            RequestId = requestId;
            TourId = tourId;
            TouristId = touristId;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), RequestId.ToString(),TourId.ToString(),TouristId.ToString() };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            RequestId = Convert.ToInt32(values[1]);
            TourId = Convert.ToInt32(values[2]);
            TouristId = Convert.ToInt32(values[3]);
        }



    }
}
