using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace BookingApp.Model
{
    public class TourInstance : ISerializable
    {
        public int Id { get; set; }
        public int IdTour { get; set; }
        public int MaxTourists { get; set; }
        public int ReservedTourists { get; set; } 
        public bool Started { get; set; }
        public bool Ended { get; set; }
        public DateTime Date { get; set; }

        public TourInstance() 
        {
            ReservedTourists = 0;
            Started = false;
            Ended = false;
        }
        public TourInstance(int idTour, int maxTourists, int reservedTourists, bool started, bool ended, DateTime date)
        {
            IdTour = idTour;
            MaxTourists = maxTourists;
            this.ReservedTourists = reservedTourists;
            this.Started = started;
            this.Ended = ended;
            Date = date;
        }
        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            IdTour = Convert.ToInt32(values[1]);
            MaxTourists = Convert.ToInt32(values[2]);
            ReservedTourists = Convert.ToInt32(values[3]);
            Started = Convert.ToBoolean(values[4]);
            Ended = Convert.ToBoolean(values[5]);
            //Date = Convert.ToDateTime(values[6]);
            //Date = DateTime.Parse(values[6]);
            Date = DateTime.ParseExact(values[6].Trim(), "dd.MM.yyyy. HH:mm:ss", CultureInfo.InvariantCulture);
           // Date = DateTime.ParseExact(values[6], "dd.MM.yyyy HH:mm:ss", CultureInfo.InvariantCulture);
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), IdTour.ToString(), MaxTourists.ToString(),
                ReservedTourists.ToString(), Started.ToString(),Ended.ToString(), Date.ToString("dd.MM.yyyy. HH:mm:ss") }; 
            return csvValues;
        }
    }
}
