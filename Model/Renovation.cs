using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Serializer;

namespace BookingApp.Model
{
    public class Renovation : ISerializable
    {
        public int Id { get; set; }
        public Accommodation Accommodation { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }   
        public int Duration { get; set; }

        public Renovation() { }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Accommodation = new Accommodation() { Name= values[1] };
            // StartDate = Convert.ToDateTime(values[2]);
            // EndDate = Convert.ToDateTime(values[3]);
            StartDate = DateTime.ParseExact(values[2].Trim(), "dd.MM.yyyy. HH:mm:ss", CultureInfo.InvariantCulture);
            EndDate = DateTime.ParseExact(values[3].Trim(), "dd.MM.yyyy. HH:mm:ss", CultureInfo.InvariantCulture);
            Duration = Convert.ToInt32(values[4]);
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Accommodation.Name,/* StartDate.ToString(),EndDate.ToString()*/StartDate.ToString("dd.MM.yyyy. HH:mm:ss"),EndDate.ToString("dd.MM.yyyy. HH:mm:ss"), Duration.ToString()};
            return csvValues;
        }
    }
}
