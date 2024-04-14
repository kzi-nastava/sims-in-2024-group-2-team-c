using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.DTO
{
    public class ReservationDelayDTO
    {
        public int ReservationDelayId { get; set; }
        public Guest Guest { get; set; }
        public Accommodation Accommodation { get; set; }
        public DateTime NewCheckInDate { get; set; } // Novi datum prijave
        public DateTime NewCheckOutDate { get; set; }
        public ReservationDelayStatus Status { get; set; } // Status zahteva (Odobren, Odbijen, Na čekanju)

        public ReservationDelayDTO() { }

        public ReservationDelayDTO(int reservationDelayId, Guest guest, Accommodation accommodation, DateTime newCheckInDate, DateTime newCheckOutDate, ReservationDelayStatus status)
        {
            ReservationDelayId = reservationDelayId;
            Guest = guest;
            Accommodation = accommodation;
            NewCheckInDate = newCheckInDate;
            NewCheckOutDate = newCheckOutDate;
            Status = status;
        }
    }
}


