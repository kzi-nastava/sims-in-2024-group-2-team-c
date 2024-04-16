using BookingApp.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BookingApp.Repository
{
    public class ReservationDelayRepository
    {
        private const string FilePath = "../../../Resources/Data/reservationdelay.csv";
        private readonly Serializer<ReservationDelay> _serializer;

        public ReservationDelayRepository()
        {
            _serializer = new Serializer<ReservationDelay>();
        }

        public List<ReservationDelay> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public ReservationDelay Save(ReservationDelay reservationDelay)
        {
            var reservationDelays = GetAll();
            reservationDelay.ReservationDelayId = NextId(reservationDelays);
            reservationDelays.Add(reservationDelay);
            _serializer.ToCSV(FilePath, reservationDelays);
            return reservationDelay;
        }

        private int NextId(List<ReservationDelay> reservationDelays)
        {
            if (reservationDelays.Count < 1)
            {
                return 1;
            }
            return reservationDelays.Max(r => r.ReservationDelayId) + 1;
        }

        public void Delete(ReservationDelay reservationDelay)
        {
            var reservationDelays = GetAll();
            ReservationDelay founded = reservationDelays.Find(r => r.ReservationDelayId == reservationDelay.ReservationDelayId);
            reservationDelays.Remove(founded);
            _serializer.ToCSV(FilePath, reservationDelays);
        }

        public ReservationDelay GetById(int reservationId)
        {
            var reservationDelays = GetAll();
            return reservationDelays.FirstOrDefault(r => r.ReservationDelayId == reservationId);
        }

        public void Update(ReservationDelay updatedReservationDelay)
        {
            var reservationDelays = GetAll();
            int index = reservationDelays.FindIndex(r => r.ReservationDelayId == updatedReservationDelay.ReservationDelayId);

            if (index != -1)
            {
                // Ažurira rezervaciju na odgovarajućem indeksu sa novim podacima
                reservationDelays[index] = updatedReservationDelay;
                _serializer.ToCSV(FilePath, reservationDelays);
            }
            else
            {
                // Dodajte logiku za obradu slučaja kada rezervacija nije pronađena
                // Možete baciti izuzetak ili izvršiti odgovarajuće radnje
            }
        }


    }
}