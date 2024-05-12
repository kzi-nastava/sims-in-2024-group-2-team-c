using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using BookingApp.Serializer;

namespace BookingApp.Model
{
    public class Reservation : ISerializable
    {
        public int Id { get; set; }
        public Accommodation Accommodation { get; set; }
        public Guest Guest { get; set; }
        public DateTime ArrivalDate { get; set; }   
        
        public DateTime DepartureDate { get; set; }
        public bool IsReserved { get; set; } 


        public Reservation() { }
        public Reservation(int id, Accommodation accommodation, Guest guest, DateTime startDate, DateTime endDate, bool isReserved)
        {
            Id = id;
            Accommodation = accommodation;
            Guest = guest;
            ArrivalDate = startDate;
            DepartureDate = endDate;
            IsReserved = isReserved; 
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Accommodation.Name,Guest.Username, /*ArrivalDate.ToString(), DepartureDate.ToString(),*/ArrivalDate.ToString("dd.MM.yyyy. HH:mm:ss"),DepartureDate.ToString("dd.MM.yyyy. HH:mm:ss"), IsReserved.ToString() };
            return csvValues;
        }


        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Accommodation = new Accommodation() { Name=values[1] };
            Guest = new Guest() {  Username=values[2] };
           // ArrivalDate = Convert.ToDateTime(values[3]);
            ArrivalDate = DateTime.ParseExact(values[3].Trim(), "dd.MM.yyyy. HH:mm:ss", CultureInfo.InvariantCulture);
            //DepartureDate = Convert.ToDateTime(values[4]);
            DepartureDate = DateTime.ParseExact(values[4].Trim(), "dd.MM.yyyy. HH:mm:ss", CultureInfo.InvariantCulture);
            IsReserved = Convert.ToBoolean(values[5]);
        }
    }
}
