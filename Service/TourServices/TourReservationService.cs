using BookingApp.DTO;
using BookingApp.Injector;
using BookingApp.Interfaces;
using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Service.TourServices
{
    public class TourReservationService
    {

        public readonly ITourReservationRepository tourReservationRepository;
        public readonly TourService tourService;
        public readonly PeopleInfoService peopleInfoService;

        public TourReservationService()
        {
            tourReservationRepository = Injectorr.CreateInstance<ITourReservationRepository>();
            peopleInfoService = new PeopleInfoService();
            tourService = new TourService();
        }


        public List<TourReservation> GetByMainTouristId(int mainTouristId)
        {
            return tourReservationRepository.GetByMainTouristId(mainTouristId);
        }

        public void Save(TourReservation tourReservation)
        {
            tourReservationRepository.Save(tourReservation);
        }


        public TourDTO makeTourDTO(int id)
        {

            Tour tour = tourService.GetById(id);

            TourDTO tourDTO  = new TourDTO(tour.Name,tour.Language,tour.Duration);

            return tourDTO;

        }

        public void SaveTouristReservation(int instanceId, int? touristNumber, int mainTouristId, List<PeopleInfo> peopleInfo)
        {
            List<int> peopleIds =   peopleInfoService.SavePeopleInfoList(peopleInfo);
            TourReservation reservation = new TourReservation(instanceId, (int)touristNumber, mainTouristId, peopleIds);
            Save(reservation);
        }


    }
}
