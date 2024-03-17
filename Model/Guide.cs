using System;
using System.Collections.Generic;
using System.Linq;
using BookingApp.Serializer;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;


namespace BookingApp.Model
{
    class Guide : User, ISerializable
    {
        public int Id { get; set; }
        public List<int> ToursIds { get; set; }

        public Guide() { }
        public Guide(string username, string password, List<id> toursIds)
        {
            Username = username;
            Password = password;
            ToursIds = toursIds;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Username = values[1];
            Password = values[2];
            ToursIds = values[3].Split(',').Select(tour => tour.Trim()).ToList();
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Username, Password, string.Join(",", ToursIds) };
            return csvValues;
            /*List<string> csvValues = new List<string>();

            csvValues.Add(Id.ToString());
            csvValues.Add(Username);
            csvValues.Add(Password);
            foreach (Tour t in Tours)
            {
                csvValues.Add(t.Id.ToString());
            }

            return csvValues.ToArray();*/
        }
    }
}
