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

namespace BookingApp.Repository
{
    public class AccommodationRateRepository : IAccommodationRateRepository
    {

        private const string FilePath = "../../../Resources/Data/accommodationRate.csv";
        private readonly Serializer<AccommodationRate> _serializer;
        private List<AccommodationRate> _accommodationRates;
        private GuestReservationRepository guestReservationRepository;

        private ReservationRepository reservationRepository; //dodato umesto guestresrep



        public AccommodationRateRepository()
        {
            _serializer = new Serializer<AccommodationRate>();
            _accommodationRates = _serializer.FromCSV(FilePath);
            guestReservationRepository = new GuestReservationRepository(); 
            reservationRepository = new ReservationRepository();
        }


         public void Save(AccommodationRate accommodationRate)

        {
            accommodationRate.Id = NextId();
            _accommodationRates = _serializer.FromCSV(FilePath);
            _accommodationRates.Add(accommodationRate);
            _serializer.ToCSV(FilePath, _accommodationRates);
            //guestReservationRepository = new GuestReservationRepository();
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

        public bool HasUserRatedAccommodation(int userId, string name)
        {
            List<AccommodationRate> rates = _serializer.FromCSV(FilePath);

            List<GuestReservation> reservations = guestReservationRepository.GetAll();

            var reservation = reservations.FirstOrDefault(r => r.GuestId == userId && r.Accommodation.Name == name);

            if (reservation != null)
            {
                return rates.Any(rate => rate.Reservation.Id == reservation.ReservationId);

            }
            else
            {
                return false;
            }
        }

    }

}
