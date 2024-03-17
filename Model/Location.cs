using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Serializer;

namespace BookingApp.Model
{
    public class Location : ISerializable
    {

        public int Id { get; set; }
        public string City {  get; set; }
        public string Country {  get; set; }

        public Location() {
            City = "";
            Country = "";
        }
        public Location(string City, string Country)
        {
            this.City = City;
            this.Country = Country;
        }
        public Location(int Id, string City, string Country)
        {
            this.Id = Id;
            this.City = City;
            this.Country = Country;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            City = values[1];
            Country = values[2];
        }

        public string[] ToCSV()
        {
            string[] csvValues = {Id.ToString(), City, Country};
            return csvValues;
        }
    }
}
