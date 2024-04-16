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

        public TouristNotificationService(ITouristNotificationRepository touristNotificationRepository)
        {
            _touristNotificationRepository = touristNotificationRepository;
            _touristService = new TouristService(new TouristRepository());
        }


        public void Save(TouristNotification touristNotification)
        {
            _touristNotificationRepository.Save(touristNotification);
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



    }
}
