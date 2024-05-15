using BookingApp.Injector;
using BookingApp.Interfaces;
using BookingApp.Model;
using BookingApp.Service.TourServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Service.GuestService
{
    public class GuestNotificationService
    {

        private readonly IGuestNotificationRepository _guestNotificationRepository;

        public GuestNotificationService()
        {
            _guestNotificationRepository = Injectorr.CreateInstance<IGuestNotificationRepository>();
        }
        public IEnumerable<GuestNotification> GetGuestNotifications(int id)
        {
            return _guestNotificationRepository.GetAllByGuestId(id);
        }
    }
}
