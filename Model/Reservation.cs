using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Serializer;

namespace BookingApp.Model
{
    public class Reservation : ISerializable
    {

        public int Id { get; set; }
        public int TourInstanceId;
        public int TouristCount;
        public List<int> OtherTouristIds;

        public Reservation() { 
            OtherTouristIds = new List<int>();
        }

        public Reservation(int tourInstanceId, int touristCount, List<int> otherTourists)
        {
            //Id = id;
            TourInstanceId = tourInstanceId;
            TouristCount = touristCount;
            OtherTouristIds = otherTourists;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            TourInstanceId = Convert.ToInt32(values[1]);
            TouristCount = Convert.ToInt32(values[2]);
            OtherTouristIds = values[3].Split(',').Select(id => Convert.ToInt32(id)).ToList();
        }
    

        public string[] ToCSV()
        {
            return new string[] { Id.ToString(), TourInstanceId.ToString(), TouristCount.ToString(), string.Join(",", OtherTouristIds) };
        }
    }
}
