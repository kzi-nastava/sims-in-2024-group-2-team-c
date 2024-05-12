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

        public TourRequestNotificationService() {
            tourRequestNotificationRepository = Injectorr.CreateInstance<ITourRequestNotificationRepository>();
        }



        public void Save(TourRequestNotification notification)
        {
            tourRequestNotificationRepository.Save(notification);
        }



    }
}
