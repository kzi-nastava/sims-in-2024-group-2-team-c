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

        public TourRequestNotificationService() {
            tourRequestNotificationRepository = Injectorr.CreateInstance<ITourRequestNotificationRepository>();
            tourRequestService = new TourRequestService();
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

        

        public TouristRequestDTO GetRequest(int requestId)
        {

            List<TouristRequestDTO> requests = tourRequestService.GetTouristRequests();

            return requests.FirstOrDefault(request => request.TourRequestId == requestId);

        }



    }
}
