using BookingApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Injector;
using BookingApp.DTO;
using BookingApp.Repository;
using BookingApp.Model;

namespace BookingApp.Service.TourServices
{
    public class FollowTourService
    {


        private readonly TourReservationService _tourReservationService;
        private readonly TourService _tourService;
        private readonly TourInstanceService _tourInstanceService;
        private readonly KeyPointService _keyPointService;
        private readonly TourReviewService _tourReviewService;


        public FollowTourService()
        {
            _tourReservationService = new TourReservationService();
            _tourService = new TourService();
            _tourInstanceService = new TourInstanceService();
            _keyPointService = new KeyPointService();
            _tourReviewService = new TourReviewService();

        }




        public List<FollowingTourDTO> GetActiveTourInstances()
        {

            var tourInstances = _tourInstanceService.GetAll();
            var reservations = _tourReservationService.GetByMainTouristId(LoggedInUser.Id);
            var reservedTourInstanceIds = reservations.Select(reservation => reservation.TourInstanceId).ToHashSet();
            var tourReviews = _tourReviewService.GetAll();

            // Filter active instances 
            var activeTourInstances = tourInstances.Where(instance => instance.Started && reservedTourInstanceIds.Contains(instance.Id) && !tourReviews.Any(review => review.TourInstanceId == instance.Id)).ToList();

            List<FollowingTourDTO> activeTourDTOs = new List<FollowingTourDTO>();

            AddActiveDTOsToList(activeTourInstances, activeTourDTOs);

            
            return activeTourDTOs;
        }





        private void AddActiveDTOsToList(List<TourInstance> activeTourInstances, List<FollowingTourDTO> activeTourDTOs)
        {
            foreach (var instance in activeTourInstances)
            {
                
                var tour = _tourService.GetById(instance.IdTour);

                FollowingTourDTO dto = new FollowingTourDTO
                {
                    Name = tour.Name,
                    Active = true, 
                    TourId = tour.Id,
                    TourInstanceId = instance.Id
                };

                activeTourDTOs.Add(dto);
            }
        }




        public List<ActiveTourKeyPointDTO> GetKeyPoints(FollowingTourDTO activeTour)
        {

            List<KeyPoint> keyPoints = _keyPointService.GetKeyPointsByTourId(activeTour.TourId);
            List<ActiveTourKeyPointDTO> keyPointDTOs = new List<ActiveTourKeyPointDTO>();

            foreach (var instance in keyPoints)
            {
                
                string activity = CheckActivity(instance);

                ActiveTourKeyPointDTO keyPoint = new ActiveTourKeyPointDTO(instance.Name, instance.Description, activity);

                keyPointDTOs.Add(keyPoint);


            }


            return keyPointDTOs;
        }




        private static string CheckActivity(KeyPoint instance)
        {
            string activity;
            if (instance.Active)
            {
                activity = "ACTIVE";
            }
            else
            {
                activity = "NOT ACTIVE";
            }

            return activity;
        }


        public  string GetDescription(int id)
        {

            Tour tour = _tourService.GetById(id);
            return tour.Description;


        }


    }
}
