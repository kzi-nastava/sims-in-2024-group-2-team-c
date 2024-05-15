using BookingApp.DTO;
using BookingApp.Injector;
using BookingApp.Interfaces;
using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Service.TourServices
{
    public class TourRequestNotificationService
    {

        private readonly ITourRequestNotificationRepository tourRequestNotificationRepository;
        private readonly TourRequestService tourRequestService;
        private readonly LocationService locationService;
        private readonly TourInstanceService tourInstanceService;

        public TourRequestNotificationService() {
            tourRequestNotificationRepository = Injectorr.CreateInstance<ITourRequestNotificationRepository>();
            tourRequestService = new TourRequestService();
            locationService = new LocationService();
            tourInstanceService = new TourInstanceService();
        }

        public void Save(TourRequestNotification notification)
        {
            tourRequestNotificationRepository.Save(notification);
        }

        public List<TourRequestNotification> GetAll() { return tourRequestNotificationRepository.GetAll(); }

        public List<TourRequestNotification> GetByUserId(int userId)
        {
            List<TourRequestNotification> notifications = GetAll();

            var userNotifications = notifications.Where(notification =>
                notification.TouristId == userId );

            return userNotifications.ToList();
        }
        public void SendNotification(TourRequestNotification notification)
        {
            Save(notification);
        }
        public int NextId() { return tourRequestNotificationRepository.NextId(); }



        public TouristRequestDTO GetRequest(int requestId)
        {

            List<TouristRequestDTO> requests = tourRequestService.GetTouristRequests();

            return requests.FirstOrDefault(request => request.TourRequestId == requestId);

        }

        public SelectedTourRequestDTO GetTourRequest(TouristRequestDTO touristRequest)
        {


            TourRequest request = tourRequestService.GetById(touristRequest.TourRequestId);
            Location location = locationService.Get(request.LocationId);
            string CityAndCountry = location.City + ", " + location.Country;
            string activity;

            if (request.Status == TourRequestStatus.Accepted)
            {
                activity = "ACCEPTED";
                DateTime chosenDate = GetTourDate(request);

                SelectedTourRequestDTO acceptedRequest = new SelectedTourRequestDTO(touristRequest.Number, request.StartDate, request.EndDate, request.Description, CityAndCountry, request.Language, activity, chosenDate);
                return acceptedRequest;
            }
            else if (request.Status == TourRequestStatus.Invalid)
            {
                activity = "INVALID";
            }
            else
            {
                activity = "ON HOLD";
            }

            SelectedTourRequestDTO selectedRequest = new SelectedTourRequestDTO(touristRequest.Number, request.StartDate, request.EndDate, request.Description, CityAndCountry, request.Language, activity, DateTime.Now);
            return selectedRequest;
        }

        private DateTime GetTourDate(TourRequest request)
        {
            List<TourRequestNotification> tourRequestNotifications = GetAll();
            TourRequestNotification notification = tourRequestNotifications.FirstOrDefault(notify => notify.RequestId == request.Id);
            if(notification == null)
            {
                DateTime date = DateTime.Now;
                return date;
             }
            TourInstance instance = tourInstanceService.GetRequestInstance(notification.TourId);
            DateTime chosenDate = instance.Date;
            return chosenDate;
        }
    }
}
