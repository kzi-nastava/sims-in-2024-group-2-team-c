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
        public int TouristsCount; //when we cancel our reservation to make it easier for us to update the number of tourists in TourInstance
        public List<int> TouristIds;

        public Reservation() { 
            TouristIds = new List<int>();
        }

        public Reservation(int tourInstanceId, int touristsCount, List<int> otherTourists)
        {
            TourInstanceId = tourInstanceId;
            TouristsCount = touristsCount;
            TouristIds = otherTourists;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            TourInstanceId = Convert.ToInt32(values[1]);
            TouristsCount = Convert.ToInt32(values[2]);
            TouristIds = values[3].Split(',').Select(id => Convert.ToInt32(id)).ToList();
        }
    

        public string[] ToCSV()
        {
            return new string[] { Id.ToString(), TourInstanceId.ToString(), TouristsCount.ToString(), string.Join(",", TouristIds) };
        }
    }
}
