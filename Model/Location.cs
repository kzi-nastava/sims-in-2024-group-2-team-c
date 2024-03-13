using BookingApp.Serializer;
using System;

namespace BookingApp.Model
{
    public class Location : ISerializable
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public void FromCSV(string[] values)
        {
            //string[] csvValues = values.Split(' ');
            Id = Convert.ToInt32(values[0]);
            City = values[1];
            Country = values[2];
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), City, Country };
            return csvValues;
        }
    }
}
