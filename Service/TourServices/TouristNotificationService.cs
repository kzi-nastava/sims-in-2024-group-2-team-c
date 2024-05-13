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
    public class TouristNotificationService
    {


        private readonly ITouristNotificationRepository _touristNotificationRepository;
        private readonly TouristService _touristService;
        private readonly TourService _tourService;
        private readonly TourRequestNotificationService _tourRequestNotificationService;

        public TouristNotificationService() {
            _touristService = new TouristService();
            _touristNotificationRepository = Injectorr.CreateInstance<ITouristNotificationRepository>();
            _tourService = new TourService();
            _tourRequestNotificationService = new TourRequestNotificationService();
        }

        


        public void Save(TouristNotification touristNotification)
        {
            _touristNotificationRepository.Save(touristNotification);
        }

        public void SendNotification(PeopleInfo info, KeyPoint selectedPoint)
        {
            Tour tour = _tourService.GetById(selectedPoint.TourId);
            TouristNotification notification = new TouristNotification 
            {
                Name = info.FirstName,
                TourInstanceId = selectedPoint.TourId,
                TourName = tour.Name,
                TouristId = 4
            };
            Save(notification);
        }

        public IEnumerable<TouristNotification> GetAllNotificationsForUser(int userId)
        {
            // Fetch all notifications from the repository
            var allNotifications = _touristNotificationRepository.GetAll();

            // Filter notifications based on userId and active TourInstance status
            var userNotifications = allNotifications.Where(notification =>
                notification.TouristId == userId && _touristService.GetActivity(userId));

            return userNotifications;
        }

        public List<TourRequestNotification> GetAllRequestNotifications(int userId)
        {
            return _tourRequestNotificationService.GetByUserId(userId);

        }

        public TouristRequestDTO GetAcceptedRequest(int requestId)
        {

            return _tourRequestNotificationService.GetRequest(requestId);


        }



    }
}
