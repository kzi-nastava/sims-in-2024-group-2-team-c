using System;
using System.Collections.Generic;
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

        public ReservationDelay() { }

        public ReservationDelay(int reservationDelayId, Guest guest, Accommodation accommodation, DateTime newCheckInDate, DateTime newCheckOutDate, ReservationDelayStatus status)
        {
            ReservationDelayId = reservationDelayId;
            Guest = guest;
            Accommodation = accommodation;
            NewCheckInDate = newCheckInDate;
            NewCheckOutDate = newCheckOutDate;
            Status = status;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { ReservationDelayId.ToString(), Guest.Username, Accommodation.Name, NewCheckInDate.ToString(), NewCheckOutDate.ToString(), Status.ToString() };
            return csvValues;
        }


        public void FromCSV(string[] values)
        {
            ReservationDelayId = Convert.ToInt32(values[0]);
            Guest = new Guest() { Username = values[1] };
            Accommodation = new Accommodation { Name = values[2] };
            NewCheckInDate = Convert.ToDateTime(values[3]);
            NewCheckOutDate = Convert.ToDateTime(values[4]);
            Status = (ReservationDelayStatus)Enum.Parse(typeof(ReservationDelayStatus), values[5]);
        }

    }
}



        public enum ReservationDelayStatus
        {
            Pending, // Na čekanju
            Approved, // Odobren
            Rejected // Odbijen
        }

