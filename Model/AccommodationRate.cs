using System.Collections.Generic;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Serializer;

namespace BookingApp.Model
{
    public class AccommodationRate : ISerializable
    {
        public int Id { get; set; }
        public Reservation Reservation { get; set; }
        public int Cleanliness { get; set; }
        public int OwnerRate { get; set; }
        public string Comment { get; set; }
        public List<String> Images { get; set; }

        public AccommodationRate() { }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Reservation.Id.ToString(), Cleanliness.ToString(), OwnerRate.ToString(), Comment };
            /*
            foreach (string imagePath in Images)
            {
                csvValues.Append($"{imagePath};");
            }
            */
            return csvValues;
        }


        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Reservation = new Reservation() { Id = Convert.ToInt32(values[1]) };
            Cleanliness = Convert.ToInt32(values[2]);
            OwnerRate = Convert.ToInt32(values[3]);
            Comment = values[4];
            Images = new List<string>();
            /*
            for (int i = 5; i < values.Length; i++)
            {
                Images.Add(values[i]);
            }
            */
        }


    }
}
