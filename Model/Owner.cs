using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Model
{
    public class Owner : User
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public List<int> AccommodationIds { get; set; } 



        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Username = values[1];
            Password = values[2];
            Name = values[3];
            Surname = values[4];
            Email = values[5];
            PhoneNumber = values[6];
            AccommodationIds = values.Skip(7).Select(int.Parse).ToList();

        }

      
        public string[] ToCSV()
        {
            List<string> csvValues = new List<string>();
            csvValues.AddRange(base.ToCSV()); // Dodajemo osnovne korisničke podatke

            // Dodajemo vlasničke podatke
            csvValues.Add(Name);
            csvValues.Add(Surname);
            csvValues.Add(Email);
            csvValues.Add(PhoneNumber);

            // Dodajemo ID-ove smeštaja
            if (AccommodationIds != null)
            {
                csvValues.AddRange(AccommodationIds.Select(id => id.ToString()));
            }

            return csvValues.ToArray();
        }
    }
}
