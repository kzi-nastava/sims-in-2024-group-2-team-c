using BookingApp.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Interfaces
{
    public interface ITourReservationRepository
    {


        public List<TourReservation> GetAll();

        public TourReservation Save(TourReservation reservation);

        public int NextId();

        public void Delete(TourReservation reservation);
        public TourReservation Update(TourReservation reservation);
        public List<TourReservation> GetByMainTouristId(int mainTouristId);



    }
}
