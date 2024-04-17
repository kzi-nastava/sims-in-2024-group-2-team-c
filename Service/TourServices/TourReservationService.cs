using BookingApp.Injector;
using BookingApp.Interfaces;
using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Service.TourServices
{
    public class TourReservationService
    {

        public readonly ITourReservationRepository tourReservationRepository;

        public TourReservationService()
        {
            tourReservationRepository = Injectorr.CreateInstance<ITourReservationRepository>();
        }


        public List<TourReservation> GetByMainTouristId(int mainTouristId)
        {
            return tourReservationRepository.GetByMainTouristId(mainTouristId);
        }

        public void Save(TourReservation tourReservation)
        {
            tourReservationRepository.Save(tourReservation);
        }


    }
}
