using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Serializer;

namespace BookingApp.Model
{
    public class Tourist : User, ISerializable
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }

        public bool Active { get; set; }

        public List<int> ReservationIds { get; set; }

        public List<int> TourRequestIds { get; set; }

        public int YearlyTourNumber {get; set; }

        public Tourist() { }

        public Tourist(string firstName,string lastName, int age,List<int> reservationIds,int yearlyTourNumber) { 
        
            FirstName = firstName;  
            LastName = lastName;
            Age = age;
            Active = false;
            ReservationIds = reservationIds;
            YearlyTourNumber = yearlyTourNumber;
        
        }


        public void FromCSV(string[] values)
        {
            //base.FromCSV(values); // Call base class's FromCSV method
            Id = Convert.ToInt32(values[0]);
            Username = values[1];
            FirstName = values[2];
            LastName = values[3];
            Age = Convert.ToInt32(values[4]);
            Active = Convert.ToBoolean(values[5]);
            ReservationIds = values[6].Split(',').Select(int.Parse).ToList();
            TourRequestIds = values[7].Split(',').Select(int.Parse).ToList();
            YearlyTourNumber = Convert.ToInt32(values[8]);
        }

        public string[] ToCSV()
        {
            //string[] userCSV = base.ToCSV(); // Call base class's ToCSV method
            string[] touristCSV = { Id.ToString(),Username, FirstName, LastName, Age.ToString(), Active.ToString(), string.Join(",", ReservationIds), string.Join(",", TourRequestIds),YearlyTourNumber.ToString() };
            return touristCSV;
        }



    }
}
