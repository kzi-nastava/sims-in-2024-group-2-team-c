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
            var activeTourInstances = tourInstances.Where(instance =>  reservedTourInstanceIds.Contains(instance.Id) && !tourReviews.Any(review => review.TourInstanceId == instance.Id)).ToList();

            List<FollowingTourDTO> tourDTOs = new List<FollowingTourDTO>();

            AddActiveDTOsToList(activeTourInstances, tourDTOs);

            
            return tourDTOs;
        }





        private void AddActiveDTOsToList(List<TourInstance> activeTourInstances, List<FollowingTourDTO> activeTourDTOs)
        {
            foreach (var instance in activeTourInstances)
            {
                
                var tour = _tourService.GetById(instance.IdTour);

                FollowingTourDTO dto = new FollowingTourDTO
                {
                    Name = tour.Name,
                    Active = instance.Started, 
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


            TourInstance tourInstance = _tourInstanceService.GetById(activeTour.TourInstanceId);

            foreach (var instance in keyPoints)
            {
                
                string activity = CheckActivity(instance, tourInstance.Ended);

                ActiveTourKeyPointDTO keyPoint = new ActiveTourKeyPointDTO(instance.Name, instance.Description, activity);

                keyPointDTOs.Add(keyPoint);


            }


            return keyPointDTOs;
        }



        public List<ActiveTourKeyPointDTO> GetKeyPointsByTour(HomeTourDTO activeTour)
        {

            List<KeyPoint> keyPoints = _keyPointService.GetKeyPointsByTourId(activeTour.TourId);
            List<ActiveTourKeyPointDTO> keyPointDTOs = new List<ActiveTourKeyPointDTO>();

            

            foreach (var instance in keyPoints)
            {

               

                ActiveTourKeyPointDTO keyPoint = new ActiveTourKeyPointDTO(instance.Name, instance.Description, instance.Active.ToString());

                keyPointDTOs.Add(keyPoint);


            }


            return keyPointDTOs;
        }




        private static string CheckActivity(KeyPoint instance, bool ended)
        {
            string activity;
            

            if (!ended)
            {
                if (instance.Active)
                {
                    activity = "ACTIVE";
                }
                else
                {
                    activity = "NOT ACTIVE";
                }
            }
            else
            {
                activity = "PASSED";
            }




            return activity;
        }


        public  string GetDescription(int id)
        {

            Tour tour = _tourService.GetById(id);
            return tour.Description;


        }


        public DateTime GetDateOfInstance(int id)
        {
            TourInstance tourInstance = _tourInstanceService.GetById(id);
            return tourInstance.Date;

        }


    }
}
