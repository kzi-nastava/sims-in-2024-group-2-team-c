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
    public class ReservationDelay : ISerializable
    {
            public int ReservationDelayId { get; set; } 
            public Guest Guest { get; set; }    
            public Accommodation Accommodation { get; set; }
            public DateTime NewCheckInDate { get; set; } 
            public DateTime NewCheckOutDate { get; set; }
            public ReservationDelayStatus Status { get; set; } 
            public string Explanation { get; set; }
        


        public ReservationDelay() { }

            public ReservationDelay(int reservationDelayId, Guest guest, Accommodation accommodation, DateTime newCheckInDate, DateTime newCheckOutDate, ReservationDelayStatus status, string explanation)
        {
            ReservationDelayId = reservationDelayId;
            Guest = guest;
            Accommodation = accommodation;
            NewCheckInDate = newCheckInDate;
            NewCheckOutDate = newCheckOutDate;
            Status = status;
            Explanation = explanation;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { ReservationDelayId.ToString(), Guest.Username, Accommodation.Name, /*NewCheckInDate.ToString(), NewCheckOutDate.ToString(),*/NewCheckInDate.ToString("dd.MM.yyyy. HH:mm:ss"),NewCheckOutDate.ToString("dd.MM.yyyy. HH:mm:ss"), Status.ToString(), Explanation };
            return csvValues;
        }

        
        public void FromCSV(string[] values)
        {
            ReservationDelayId = Convert.ToInt32(values[0]);
            Guest = new Guest() { Username=values[1] };
            Accommodation = new Accommodation { Name=values[2] };
            NewCheckInDate = DateTime.ParseExact(values[3].Trim(), "dd.MM.yyyy. HH:mm:ss", CultureInfo.InvariantCulture);// Convert.ToDateTime(values[3]);
            NewCheckOutDate = DateTime.ParseExact(values[4].Trim(), "dd.MM.yyyy. HH:mm:ss", CultureInfo.InvariantCulture);//Convert.ToDateTime(values[4]);
            Status = (ReservationDelayStatus)Enum.Parse(typeof(ReservationDelayStatus), values[5]);
            Explanation = values[6];
        }

    }
    }

    

    public enum ReservationDelayStatus
        {
            Pending, // Na čekanju
            Approved, // Odobren
            Rejected // Odbijen
        }

    

