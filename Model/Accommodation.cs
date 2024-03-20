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
        public Location Location { get; set; }
        public string Type { get; set; }
        public int MaxGuests { get; set; }
        public int MinBookingDays { get; set; }
        public int CancellationDays { get; set; }
        public Owner Owner { get; set; } 
        public List<string> Images { get; set; }

        public Accommodation()
        {
            Images = new List<string>();
        }


        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Name, Location.Id.ToString(), Type, MaxGuests.ToString(), MinBookingDays.ToString(), CancellationDays.ToString(),/*Owner.Id.ToString()*/ };
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
            Location = new Location() { Id = Convert.ToInt32(values[2]) };
            Type = values[3];
            MaxGuests = Convert.ToInt32(values[4]);
            MinBookingDays = Convert.ToInt32(values[5]);
            CancellationDays = Convert.ToInt32(values[6]);
            //Owner = new Owner() { Id = Convert.ToInt32(values[7]) };
            Images = new List<string>();
            for (int i = 7; i < values.Length; i++)
            {
                Images.Add(values[i]); 
            }
        }
    }
}

