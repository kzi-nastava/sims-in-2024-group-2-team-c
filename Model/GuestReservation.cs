using BookingApp.Serializer;
using System;
using System.Collections.Generic;

namespace BookingApp.Model
{
    class GuestReservation : ISerializable
    {

        public int ReservationId { get; set; }
        public Accommodation Accommodation { get; set; }
        public DateTime StartDate { get; set; } //pocetni dan opsega za koji korisnik zeli da se traze slobodni datumi
        public DateTime EndDate { get; set; } //krajnji dan opsega za koji korisnik zeli da se traze slobodni datumi
        public int StayDurationInDays { get; set; }  // Broj dana boravka

        public DateTime CheckIn { get; set; } // Datum kada gost ulazi u smještaj
        public DateTime CheckOut { get; set; } // Datum kada gost napušta smještaj

        public int NumGuests { get; set; }

        public bool IsReserved { get; set; }  // Polje za označavanje zauzetosti


        // Listu za prikaz dostupnih datuma
        public List<AvailableDateDisplay> AvailableDates { get; set; }


        public GuestReservation()
        {
            AvailableDates = new List<AvailableDateDisplay>();
        }


        public string[] ToCSV()
        {
            string[] csvValues = { ReservationId.ToString(), Accommodation.Id.ToString(), 
                StartDate.ToString(), EndDate.ToString(), StayDurationInDays.ToString(), 
                CheckIn.ToString(), CheckOut.ToString(),
                NumGuests.ToString(), IsReserved.ToString()};
            return csvValues;

        }



        public void FromCSV(string[] values)
        {
            ReservationId = Convert.ToInt32(values[0]);
            Accommodation = new Accommodation() { Id = Convert.ToInt32(values[1]) };
            StartDate = Convert.ToDateTime(values[2]);
            EndDate = Convert.ToDateTime(values[3]);
            StayDurationInDays = Convert.ToInt32(values[4]);
            CheckIn = Convert.ToDateTime(values[5]);
            CheckOut = Convert.ToDateTime(values[6]);
            NumGuests = Convert.ToInt32(values[7]);
            IsReserved = Convert.ToBoolean(values[8]);
        }

        
    }
}
