using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BookingApp.Serializer;

namespace BookingApp.Model
{
    public class RenovationAvailableDate : ISerializable
    {
        public int Id { get; set; }
        public Accommodation Accommodation { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Accommodation = new Accommodation() { Name = values[1] };
            StartDate = Convert.ToDateTime(values[2]);
            EndDate = Convert.ToDateTime(values[3]);
            Description = values[4];
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Accommodation.Name,StartDate.ToString(), EndDate.ToString(), Description };
            return csvValues;
        }
    }
}
