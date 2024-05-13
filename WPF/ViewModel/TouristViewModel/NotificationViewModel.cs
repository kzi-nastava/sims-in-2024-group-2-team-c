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
using System.Windows;

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



        private ObservableCollection<TourRequestNotification> _requestNotifications;

        public ObservableCollection<TourRequestNotification> RequestNotifications
        {
            get { return _requestNotifications; }
            set
            {
                _requestNotifications = value;
                OnPropertyChanged(nameof(RequestNotifications));
            }
        }


        private Visibility _content1Visibility = Visibility.Collapsed;
        private Visibility _content2Visibility = Visibility.Collapsed;
        private Visibility _content3Visibility = Visibility.Collapsed;

        public Visibility Content1Visibility
        {
            get => _content1Visibility;
            set
            {
                if (_content1Visibility != value)
                {
                    _content1Visibility = value;
                    OnPropertyChanged(nameof(Content1Visibility));
                }
            }
        }

        public Visibility Content2Visibility
        {
            get => _content2Visibility;
            set
            {
                if (_content2Visibility != value)
                {
                    _content2Visibility = value;
                    OnPropertyChanged(nameof(Content2Visibility));
                }
            }
        }

        public Visibility Content3Visibility
        {
            get => _content3Visibility;
            set
            {
                if (_content3Visibility != value)
                {
                    _content3Visibility = value;
                    OnPropertyChanged(nameof(Content3Visibility));
                }
            }
        }


        private readonly TouristNotificationService _notificationService;
        private readonly MainViewModel _mainViewModel;

        public ViewModelCommandd ViewCommand { get; }


        public NotificationViewModel() {
            _mainViewModel = LoggedInUser.mainViewModel;
            ViewCommand = new ViewModelCommandd(ExecuteViewCommand);
            Content1Visibility = Visibility.Visible;
            _notificationService = new TouristNotificationService();
            LoadNotifications();
            LoadRequestNotifications();
        
        }


        private void ExecuteViewCommand(object parameter)
        {
            if (parameter is int requestId)
            {
               TouristRequestDTO request = _notificationService.GetAcceptedRequest(requestId);
                if (request != null)
                {
                    _mainViewModel.ExecuteSingleTourView(request);
                }

            }
        }



        public void LoadRequestNotifications()
        {

            RequestNotifications = new ObservableCollection<TourRequestNotification>(_notificationService.GetAllRequestNotifications(LoggedInUser.Id));

        }

        public void LoadNotifications()
        {
            Notifications = new ObservableCollection<TouristNotification>(_notificationService.GetAllNotificationsForUser(LoggedInUser.Id));

        }


        public void ShowRecommendations()
        {
            Content1Visibility = Visibility.Visible;
            Content2Visibility = Visibility.Collapsed;
            Content3Visibility = Visibility.Collapsed;
        }

        public void ShowAcceptedRequests()
        {
            Content1Visibility = Visibility.Collapsed;
            Content2Visibility = Visibility.Visible;
            Content3Visibility = Visibility.Collapsed;
        }

        public void ShowTouristsAdded()
        {
            Content1Visibility = Visibility.Collapsed;
            Content2Visibility = Visibility.Collapsed;
            Content3Visibility = Visibility.Visible;
        }



    }
}
