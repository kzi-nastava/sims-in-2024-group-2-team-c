using BookingApp.Model;
using BookingApp.Serializer;
using BookingApp.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;

namespace BookingApp.Repository
{
    public class ReservationRepository 
    {
        private const string FilePath = "../../../Resources/Data/reservations.csv";
        private readonly Serializer<Reservation> _serializer;
        private List<Reservation> _reservations;
        
        

        public ReservationRepository()
        {
            _reservations = new List<Reservation>();
            _serializer = new Serializer<Reservation>();
            _reservations = _serializer.FromCSV(FilePath);
        }

        public List<Reservation> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public Reservation Save(Reservation reservation)
        {
            reservation.Id = NextId();
            _reservations = _serializer.FromCSV(FilePath);
            _reservations.Add(reservation);
            _serializer.ToCSV(FilePath, _reservations);
            return reservation;
        }

        public int NextId()
        {
            _reservations = _serializer.FromCSV(FilePath);
            if (_reservations.Count < 1)
            {
                return 1;
            }
            return _reservations.Max(r => r.Id) + 1;
        }

        public void Delete(Reservation reservation)
        {
            _reservations = _serializer.FromCSV(FilePath);
            Reservation founded = _reservations.Find(r => r.Id == reservation.Id);
            _reservations.Remove(founded);
            _serializer.ToCSV(FilePath, _reservations);
        }

        public Reservation Update(Reservation reservation)
        {
            _reservations = _serializer.FromCSV(FilePath);
            Reservation current = _reservations.Find(r => r.Id == reservation.Id);
            int index = _reservations.IndexOf(current);
            _reservations.Remove(current);
            _reservations.Insert(index, reservation);
            _serializer.ToCSV(FilePath, _reservations);
            return reservation;
        }

        public void LoadReservationsFromCSV(string filePath)
        {
            _reservations.Clear(); // Očisti prethodne rezervacije

            string[] lines = File.ReadAllLines(filePath);
            foreach (string line in lines)
            {
                string[] values = line.Split('|');

                int Id = Convert.ToInt32(values[0]);
                int AccommodationId = Convert.ToInt32(values[1]);
                int GuestId = Convert.ToInt32(values[2]);
                DateTime ArrivalDate = Convert.ToDateTime(values[3]);
                DateTime DepartureDate = Convert.ToDateTime(values[4]);
                bool IsReserved = Convert.ToBoolean(values[5]);

                // Kreiraj rezervaciju i dodaj je u listu
                _reservations.Add(new Reservation(Id, new Accommodation { Id = AccommodationId }, new Guest { Id = GuestId }, ArrivalDate, DepartureDate, IsReserved));
            }
        }

        public List<Reservation> Reservations
        {
            get { return _reservations; }
        }
        public DateTime GetDepartureDate(int reservationId)
        {
            Reservation reservation = _reservations.FirstOrDefault(r => r.Id == reservationId);

            if (reservation != null)
            {
                return reservation.DepartureDate;
            }
            else
            {
               
                return DateTime.MinValue; // Vraćanje DateTime.MinValue kao podrazumevane vrednosti
            }
        }

        public List<Reservation> GetReservationsByAccommodationId(int accommodationId)
        {
            return _reservations.Where(r => r.Accommodation.Id == accommodationId).ToList();
        }
    }
}
