using BookingApp.Interfaces;
using BookingApp.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BookingApp.Repository
{
    public class TourReservationRepository : ITourReservationRepository
    {

        private const string FilePath = "../../../Resources/Data/tourreservations.csv";

        private readonly Serializer<TourReservation> _serializer;

        private List<TourReservation> _reservations;

        public TourReservationRepository()
        {
            _serializer = new Serializer<TourReservation>();
            _reservations = _serializer.FromCSV(FilePath);
        }

        public List<TourReservation> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public TourReservation Save(TourReservation reservation)
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
            return _reservations.Max(c => c.Id) + 1;
        }

        public void Delete(TourReservation reservation)
        {
            _reservations = _serializer.FromCSV(FilePath);
            TourReservation founded = _reservations.Find(c => c.Id == reservation.Id);
            _reservations.Remove(founded);
            _serializer.ToCSV(FilePath, _reservations);
        }

        public TourReservation Update(TourReservation reservation)
        {
            _reservations = _serializer.FromCSV(FilePath);
            TourReservation current = _reservations.Find(c => c.Id == reservation.Id);
            int index = _reservations.IndexOf(current);
            _reservations.Remove(current);
            _reservations.Insert(index, reservation);       // keep ascending order of ids in file 
            _serializer.ToCSV(FilePath, _reservations);
            return reservation;
        }

        public List<TourReservation> GetByMainTouristId(int mainTouristId)
        {
            return _reservations.Where(r => r.MainTouristId == mainTouristId).ToList();
        }



    }
}
