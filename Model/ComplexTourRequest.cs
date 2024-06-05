using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Model
{

    public enum ComplexTourRequestStatus { Accepted, Invalid, OnHold };
    public class ComplexTourRequest : ISerializable
    {

        public int Id { get; set; }
        public List<int> TourRequestIds { get; set; }

        public int TouristId { get; set; }


        public ComplexTourRequestStatus Status { get; set; }

        public ComplexTourRequest() { }

        public ComplexTourRequest(List<int> tourRequests, int touristId, ComplexTourRequestStatus status)
        {
            TourRequestIds = tourRequests;
            TouristId = touristId;
            Status = status;
        }


        public string[] ToCSV()
        {

            string[] csvValues = { Id.ToString(),  string.Join(",", TourRequestIds), TouristId.ToString(),  Status.ToString()  };

            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            TourRequestIds = values[1].Split(',').Select(int.Parse).ToList();
            TouristId = Convert.ToInt32(values[2]);
            Status = (ComplexTourRequestStatus)Enum.Parse(typeof(ComplexTourRequestStatus), values[3]);
            
            

        }




    }
}
