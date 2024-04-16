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

        //ITourInstanceRepository tourInstanceRepository = Injector.CreateInstance<ITourInstanceRepository>();
        private readonly ITourRepository _tourRepository;
        private readonly ITourInstanceRepository _tourInstanceRepository;
        private readonly TourReservationRepository _tourReservationRepository;
        private KeyPointService _keyPointService;
        private readonly TourReviewService _tourReviewService;


        /* public FollowTourService(ITourRepository tourRepository, ITourInstanceRepository tourInstanceRepository)
         {
             _tourRepository = tourRepository;
             _tourInstanceRepository = tourInstanceRepository;
            _tourReservationRepository = new TourReservationRepository();
             _keyPointService = new KeyPointService();
             _tourReviewService = new TourReviewService(new TourReviewsRepository());
         }*/

        public FollowTourService()
        {
            _tourRepository = Injectorr.CreateInstance<ITourRepository>();
            _tourInstanceRepository = Injectorr.CreateInstance<ITourInstanceRepository>();
            _tourReservationRepository = new TourReservationRepository();
            _keyPointService = new KeyPointService();
            _tourReviewService = new TourReviewService();
        }


        public List<FollowingTourDTO> GetActiveTourInstances()
        {
            // Fetch all tour instances
            var tourInstances = _tourInstanceRepository.GetAll();

            var reservations = _tourReservationRepository.GetByMainTouristId(LoggedInUser.Id);
            var reservedTourInstanceIds = reservations.Select(reservation => reservation.TourInstanceId).ToHashSet();

            var tourReviews = _tourReviewService.GetAll();

            // Filter only active instances (started but not ended)
            var activeTourInstances = tourInstances.Where(instance => instance.Started && reservedTourInstanceIds.Contains(instance.Id) && !tourReviews.Any(review => review.TourInstanceId == instance.Id)).ToList();

            // Create a list of FollowingTourDTOs based on the active tour instances
            List<FollowingTourDTO> activeTourDTOs = new List<FollowingTourDTO>();

            foreach (var instance in activeTourInstances)
            {
                // Fetch the tour associated with the instance
                var tour = _tourRepository.GetById(instance.IdTour);

                // Create a DTO object and add it to the list
                FollowingTourDTO dto = new FollowingTourDTO
                { 
                    Name = tour.Name,
                    Active = true, // Since we're only including active instances
                    TourId = tour.Id,
                    TourInstanceId = instance.Id
                };

                activeTourDTOs.Add(dto);
            }

            // Return the list of active tour DTOs
            return activeTourDTOs;
        }



        public List<ActiveTourKeyPointDTO> GetKeyPoints(FollowingTourDTO activeTour)
        {

            List<KeyPoint> keyPoints = _keyPointService.GetKeyPointsByTourId(activeTour.TourId);
            List<ActiveTourKeyPointDTO> keyPointDTOs = new List<ActiveTourKeyPointDTO>();

            foreach (var instance in keyPoints)
            {
                // Fetch the tour associated with the instance
                string activity;
                if (instance.Active)
                {
                     activity = "ACTIVE";
                }
                else
                {
                    activity = "NOT ACTIVE";
                }


                ActiveTourKeyPointDTO keyPoint = new ActiveTourKeyPointDTO(instance.Name,instance.Description, activity);

                // Create a DTO object and add it to the list
                keyPointDTOs.Add(keyPoint);

                
            }


            return keyPointDTOs;
        }




    }
}
