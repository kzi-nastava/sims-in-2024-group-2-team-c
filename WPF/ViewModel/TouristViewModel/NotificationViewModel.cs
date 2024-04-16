using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Service.TourServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.ViewModel.TouristViewModel
{
    public class NotificationViewModel : ViewModelBase
    {
        private ObservableCollection<TouristNotification> _notifications;

        public ObservableCollection<TouristNotification> Notifications
        {
            get { return _notifications; }
            set { 
                _notifications = value; 
                OnPropertyChanged(nameof(Notifications));
            }
        }

        private readonly TouristNotificationService _notificationService;

        public NotificationViewModel() {
        
            _notificationService = new TouristNotificationService(new TouristNotificationRepository());
            LoadNotifications();
        
        }

        public void LoadNotifications()
        {

            Notifications = new ObservableCollection<TouristNotification>(_notificationService.GetAllNotificationsForUser(LoggedInUser.Id));

        }



    }
}
