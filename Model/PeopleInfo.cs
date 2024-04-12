using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace BookingApp.Model
{
    public  class PeopleInfo : ISerializable
    {

        public int Id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public bool Active { get; set; }


        public PeopleInfo() { 
            FirstName = string.Empty; LastName = string.Empty;
            Active = false;
        }

        public PeopleInfo(string firstName, string lastName, int age, bool active)
        {
            
            FirstName = firstName;
            LastName = lastName;
            Age = age;
            Active = active;

        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            FirstName = values[1];
            LastName = values[2];
            Age = Convert.ToInt32(values[3]);
            Active = Convert.ToBoolean(values[4]);

        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), FirstName, LastName, Age.ToString() , Active.ToString()};
            return csvValues;

        }
    }
}
