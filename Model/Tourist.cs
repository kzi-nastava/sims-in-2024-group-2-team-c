using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Serializer;

namespace BookingApp.Model
{
    public class Tourist : User,ISerializable
    { 
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age {  get; set; }

        public bool Active { get; set; }

        List<int> ReservationIds { get; set; }

        public Tourist() { }

        public Tourist(string firstName,string lastName, int age,List<int> reservationIds) { 
        
            FirstName = firstName;  
            LastName = lastName;
            Age = age;
            Active = false;
            ReservationIds = reservationIds;
        
        }


        public void FromCSV(string[] values)
        {
            base.FromCSV(values); // Call base class's FromCSV method
            Id = Convert.ToInt32(values[5]);
            FirstName = values[6];
            LastName = values[7];
            Age = Convert.ToInt32(values[8]);
            Active = Convert.ToBoolean(values[9]);
            ReservationIds = values[10].Split(',').Select(int.Parse).ToList();
        }

        public string[] ToCSV()
        {
            string[] userCSV = base.ToCSV(); // Call base class's ToCSV method
            string[] touristCSV = { Id.ToString(), FirstName, LastName, Age.ToString(), Active.ToString(), string.Join(",", ReservationIds) };
            return userCSV.Concat(touristCSV).ToArray();
        }



    }
}
