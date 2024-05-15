using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;

namespace BookingApp.Model
{
    public class GuestReservation : ISerializable
    {

        public int ReservationId { get; set; }
        public Accommodation Accommodation { get; set; }

        public int GuestId { get; set; }
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
            string[] csvValues = { ReservationId.ToString(), Accommodation.Id.ToString(), GuestId.ToString(),

               /* StartDate.ToString(), EndDate.ToString()*/ StartDate.ToString("dd.MM.yyyy. HH:mm:ss"),EndDate.ToString("dd.MM.yyyy. HH:mm:ss"), StayDurationInDays.ToString(),

                /*CheckIn.ToString(), CheckOut.ToString(),*/ CheckIn.ToString("dd.MM.yyyy. HH:mm:ss"),CheckOut.ToString("dd.MM.yyyy. HH:mm:ss"),
                NumGuests.ToString(), IsReserved.ToString()};
            return csvValues;

        }



        public void FromCSV(string[] values)
        {
            ReservationId = Convert.ToInt32(values[0]);
            Accommodation = new Accommodation() { Id = Convert.ToInt32(values[1]) };
            //GuestId = Convert.ToInt32(values[2]);

            if (int.TryParse(values[2], out int guestId))
            {
                GuestId = guestId;
            }
            else
            {
                MessageBox.Show("Greška: Vrednost za GuestId nije validna.");
            }

            StartDate = DateTime.ParseExact(values[3].Trim(), "dd.MM.yyyy. HH:mm:ss", CultureInfo.InvariantCulture);//Convert.ToDateTime(values[3]);
            EndDate = DateTime.ParseExact(values[4].Trim(), "dd.MM.yyyy. HH:mm:ss", CultureInfo.InvariantCulture);//Convert.ToDateTime(values[4]);
            StayDurationInDays = Convert.ToInt32(values[5]);
            CheckIn = DateTime.ParseExact(values[6].Trim(), "dd.MM.yyyy. HH:mm:ss", CultureInfo.InvariantCulture);//Convert.ToDateTime(values[6]);
            CheckOut = DateTime.ParseExact(values[7].Trim(), "dd.MM.yyyy. HH:mm:ss", CultureInfo.InvariantCulture); //Convert.ToDateTime(values[7]);
            NumGuests = Convert.ToInt32(values[8]);
            IsReserved = Convert.ToBoolean(values[9]);
        }


    }
}