using System.Collections.Generic;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Serializer;

namespace BookingApp.Model
{
    public class Accommodation : ISerializable
    {

        public int Id { get; set; }
        public string Name { get; set; }

        //public Location location{ get; set; }

        public string Type { get; set; }
        public int MaxGuests { get; set; }
        public int MinBookingDays { get; set; }
        public int CancellationDays { get; set; }
        public List<string> Images { get; set; }

        public Accommodation()
        {
            Images = new List<string>();
        }


        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Name, Type, MaxGuests.ToString(), MinBookingDays.ToString(), CancellationDays.ToString() };
            foreach (string imagePath in Images)
            {
                csvValues.Append($"{imagePath};");
            }
            return csvValues;
        }


        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Name = values[1];
            Type = values[2];  //bice pomereni svi za jedan kada dodamo Location, User = new User() { Id = Convert.ToInt32(values[3]) mozda ce se Lokacija praviti ovako isto jer nije int/string
            MaxGuests = Convert.ToInt32(values[3]);
            MinBookingDays = Convert.ToInt32(values[4]);
            CancellationDays = Convert.ToInt32(values[5]);
            Images = new List<string>();
            for (int i = 6; i < values.Length; i++)
            {
                Images.Add(values[i]); // Dodajemo svaku putanju slike u listu Images
            }
        }
    }
}
