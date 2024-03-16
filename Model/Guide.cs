using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Model
{
    class Guide : User, ISerializable
    {
        public List<Tour> Tours { get; set; }
        public Guide()
        {
        }

        public string[] ToCSV()
        {
            List<string> csvValues = new List<string>();

            csvValues.Add(Id.ToString());
            csvValues.Add(Username);
            csvValues.Add(Password);
            foreach (Tour t in Tours)
            {
                csvValues.Add(t.Id.ToString());
            }

            return csvValues.ToArray();
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Username = values[1];
            Password = values[2];
            //Tours = values[3].Split(',').Select(tour => tour.Trim()).ToList();
        }
    }
}
