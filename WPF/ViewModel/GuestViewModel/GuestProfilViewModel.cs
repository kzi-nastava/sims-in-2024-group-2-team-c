using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Service.AccommodationServices;
using BookingApp.Service.GuestService;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.ViewModel.GuestViewModel
{
    public class GuestProfilViewModel : ViewModelBase
    {
        private readonly SuperGuestService _superGuestService;
        private readonly GuestNotificationService _guestNotificationService;
        private bool _superGuestStatus;
        private int _bonusPoints;
        private ObservableCollection<GuestNotification> _notifications;

        public bool SuperGuestStatus
        {
            get { return _superGuestStatus; }
            set
            {
                if (_superGuestStatus != value)
                {
                    _superGuestStatus = value;
                    OnPropertyChanged(nameof(SuperGuestStatus));
                }
            }
        }

        public int BonusPoints
        {
            get { return _bonusPoints; }
            set
            {
                if (_bonusPoints != value)
                {
                    _bonusPoints = value;
                    OnPropertyChanged(nameof(BonusPoints));
                }
            }
        }

        public ObservableCollection<GuestNotification> Notifications
        {
            get { return _notifications; }
            set
            {
                if (_notifications != value)
                {
                    _notifications = value;
                    OnPropertyChanged(nameof(Notifications));
                }
            }
        }

        public GuestProfilViewModel()
        {
            _superGuestService = new SuperGuestService();
            _guestNotificationService = new GuestNotificationService();
            LoadUserInfo();
            LoadNotifications();
        }

        private void LoadUserInfo()
        {
            Guest guest = _superGuestService.GetGuest(LoggedInUser.Id);

            SuperGuestStatus = guest.SuperGuestStatus;
            BonusPoints = guest.BonusPoints;
        }

        private void LoadNotifications()
        {
            Notifications = new ObservableCollection<GuestNotification>(_guestNotificationService.GetGuestNotifications(LoggedInUser.Id));
        }
    }
}
