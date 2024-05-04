using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Serializer;

namespace BookingApp.Model
{
    public class Owner : User, ISerializable
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int NumberOfRatings { get; set; }
        public double TotalRating { get; set; }

        public List<int> AccommodationIds { get; set; }



        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Username, Password, Role.ToString(), Super.ToString(), Name, Surname, Email, PhoneNumber, NumberOfRatings.ToString(), TotalRating.ToString() };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
           
            Id = Convert.ToInt32(values[0]);
            Username = values[1];
            Password = values[2];
            if (Enum.TryParse<UserRole>(values[3], out UserRole role))
            {
                Role = role;
            }
            Super = Convert.ToBoolean(values[4]);
            Name = values[5];
            Surname = values[6];
            Email = values[7];
            PhoneNumber = values[8];
            NumberOfRatings = Convert.ToInt32(values[9]);
            TotalRating = Convert.ToDouble(values[10]);
        }

        
    }
}
