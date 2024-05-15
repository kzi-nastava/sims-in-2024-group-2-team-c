using BookingApp.Interfaces;
using BookingApp.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Repository;
using System.Reflection;

namespace BookingApp.Repository
{
    public class AccommodationRateRepository : IAccommodationRateRepository
    {

        private const string FilePath = "../../../Resources/Data/accommodationRate.csv";
        private readonly Serializer<AccommodationRate> _serializer;
        private List<AccommodationRate> _accommodationRates;
        private GuestReservationRepository guestReservationRepository;

        private ReservationRepository reservationRepository;


        public List<AccommodationRate> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }


        public AccommodationRateRepository()
        {
            _serializer = new Serializer<AccommodationRate>();
            _accommodationRates = _serializer.FromCSV(FilePath);
            guestReservationRepository = new GuestReservationRepository(); 
            reservationRepository = new ReservationRepository();
        }

        public String GetFilePath()
        {
            return FilePath;
        }

        public void Save(AccommodationRate accommodationRate)

        {
            accommodationRate.Id = NextId();
            _accommodationRates = _serializer.FromCSV(FilePath);
            _accommodationRates.Add(accommodationRate);
            _serializer.ToCSV(FilePath, _accommodationRates);
        }

        public int NextId()
        {
            _accommodationRates = _serializer.FromCSV(FilePath);
            if (_accommodationRates.Count < 1)
            {
                return 1;
            }
            return _accommodationRates.Max(a => a.Id) + 1;
        }

        public bool HasUserRatedAccommodation(int userId, int reservationId)
        {
            List<AccommodationRate> rates = _serializer.FromCSV(FilePath);

            List<GuestReservation> reservations = guestReservationRepository.GetAll();

            var reservation = reservations.FirstOrDefault(r => r.GuestId == userId && r.ReservationId == reservationId);

            if (reservation != null)
            {
                return rates.Any(rate => rate.Reservation.Id == reservation.ReservationId);
            }
            else
            {
                return false;
            }
        }

        public void Update(AccommodationRate accommodationRate)
        {
            _accommodationRates = _serializer.FromCSV(FilePath);

            int index = _accommodationRates.FindIndex(r => r.Id == accommodationRate.Id);
            if (index != -1)
            {
                _accommodationRates[index] = accommodationRate;
                _serializer.ToCSV(FilePath, _accommodationRates);
            }
            else
            {
                throw new Exception("Accommodation rate with the specified ID not found.");
            }
        }

        public AccommodationRate GetById(int id)
        {
            _accommodationRates = _serializer.FromCSV(FilePath);

            var accommodationRate = _accommodationRates.FirstOrDefault(r => r.Id == id);

            if (accommodationRate == null)
            {   
                throw new Exception("Accommodation rate with the specified ID not found.");
            }
            else
            {
                return accommodationRate;
            }
        }



    }

}
