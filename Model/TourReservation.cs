using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Serializer;

namespace BookingApp.Model
{
    public class TourReservation : ISerializable
    {

        public int Id { get; set; }
        public int TourInstanceId;
        public int TouristsCount; //when we cancel our reservation to make it easier for us to update the number of tourists in TourInstance
        //public List<int> TouristIds;
        public int MainTouristId;
        public List<int> PeopleIds;

        public TourReservation() {
            PeopleIds = new List<int>();
        }

        public TourReservation(int tourInstanceId, int touristsCount, int mainTouristId,List<int> peopleIds)
        {
            TourInstanceId = tourInstanceId;
            TouristsCount = touristsCount;
            MainTouristId = mainTouristId;
            PeopleIds = peopleIds;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            TourInstanceId = Convert.ToInt32(values[1]);
            TouristsCount = Convert.ToInt32(values[2]);
            MainTouristId = Convert.ToInt32(values[3]);
            PeopleIds = values[4].Split(',').Select(id => Convert.ToInt32(id)).ToList();
        }
    

        public string[] ToCSV()
        {
            return new string[] { Id.ToString(), TourInstanceId.ToString(), TouristsCount.ToString(),MainTouristId.ToString(), string.Join(",", PeopleIds) };
        }
    }
}
