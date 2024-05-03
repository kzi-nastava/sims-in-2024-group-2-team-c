﻿using BookingApp.Model;
using BookingApp.WPF.View.TouristView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Navigation;

namespace BookingApp.WPF.ViewModel.TouristViewModel
{
    public class MainViewModel : ViewModelBase
    {

        private ViewModelBase _currentChildView;
        public ViewModelBase CurrentChildView
        {
            get
            {
                return _currentChildView;
            }

            set
            {
                _currentChildView = value;
                OnPropertyChanged(nameof(CurrentChildView));
            }
        }


        private string _currentHomeIconSource;

        public string CurrentHomeIconSource
        {
            get { return _currentHomeIconSource; }
            set
            {
                _currentHomeIconSource = value;
                OnPropertyChanged(nameof(CurrentHomeIconSource));
            }
        }

        private string _currentMarkerIconSource;

        public string CurrentMarkerIconSource
        {
            get { return _currentMarkerIconSource; }
            set
            {
                _currentMarkerIconSource = value;
                OnPropertyChanged(nameof(CurrentMarkerIconSource));
            }
        }


        private string _currentUserIconSource;

        public string CurrentUserIconSource
        {
            get { return _currentUserIconSource; }
            set
            {
                _currentUserIconSource = value;
                OnPropertyChanged(nameof(CurrentUserIconSource));
            }
        }


        private string _currentNotificationIconSource;

        public string CurrentNotificationIconSource
        {
            get { return _currentNotificationIconSource; }
            set
            {
                _currentNotificationIconSource = value;
                OnPropertyChanged(nameof(CurrentNotificationIconSource));
            }
        }


        private string _currentRequestIconSource;

        public string CurrentRequestIconSource
        {
            get { return _currentRequestIconSource; }
            set
            {
                _currentRequestIconSource = value;
                OnPropertyChanged(nameof(CurrentRequestIconSource));
            }
        }


        public ViewModelCommandd ShowToursCommand { get; }

        public ViewModelCommandd FollowTourCommand { get; }
        public ViewModelCommandd ShowKeyPointsCommand { get; }

        public ViewModelCommandd UserCommand { get; }

        public ViewModelCommandd NotificationCommand { get; }

        public ViewModelCommandd RequestsCommand { get; }

        public MainViewModel()
        {

            LoggedInUser.mainViewModel = this;
            FollowTourCommand = new ViewModelCommandd(ExecuteFollowTourCommand);
            //ShowKeyPointsCommand = new ViewModelCommand(ExecuteShowKeyPointsCommand);
            UserCommand = new ViewModelCommandd(ExecuteUserCommand);
            NotificationCommand = new ViewModelCommandd(ExecuteNotificationCommand);
            ShowToursCommand = new ViewModelCommandd(ExecuteShowTourCommand);
            RequestsCommand = new ViewModelCommandd(ExecuteRequestCommand);

            CurrentMarkerIconSource = "/Resources/Images/marker.png";
            CurrentHomeIconSource = "/Resources/Images/home.png";
            CurrentUserIconSource = "/Resources/Images/tourist.png";
            CurrentNotificationIconSource = "/Resources/Images/bell.png";
            CurrentRequestIconSource = "/Resources/Images/tour-request.png";




        }

        public void ExecuteNotificationCommand(object obj)
        {
            /* if (obj is MainTouristView mainTouristView)
             {
                 mainTouristView.MainFrame.Navigate(new FollowTourView());
             }*/
            CurrentMarkerIconSource = "/Resources/Images/marker.png";
            CurrentHomeIconSource = "/Resources/Images/home.png";
            CurrentUserIconSource = "/Resources/Images/tourist.png";
            CurrentNotificationIconSource = "/Resources/Images/on bell.png";
            CurrentRequestIconSource = "/Resources/Images/tour-request.png";
            CurrentChildView = new NotificationViewModel();

        }

        public void ExecuteFollowTourCommand(object obj)
        {
            CurrentMarkerIconSource = "/Resources/Images/on marker.png";
            CurrentHomeIconSource = "/Resources/Images/home.png";
            CurrentUserIconSource = "/Resources/Images/tourist.png";
            CurrentNotificationIconSource = "/Resources/Images/bell.png";
            CurrentRequestIconSource = "/Resources/Images/tour-request.png";
            CurrentChildView = new FollowTourViewModel();



        }


        public void ExecuteShowTourCommand(object obj)
        {
            CurrentMarkerIconSource = "/Resources/Images/marker.png";
            CurrentHomeIconSource = "/Resources/Images/on home.png";
            CurrentUserIconSource = "/Resources/Images/tourist.png";
            CurrentNotificationIconSource = "/Resources/Images/bell.png";
            CurrentChildView = new TouristHomeViewModel();



        }

        public void ExecuteUserCommand(object obj)
        {
            CurrentMarkerIconSource = "/Resources/Images/marker.png";
            CurrentHomeIconSource = "/Resources/Images/home.png";
            CurrentUserIconSource = "/Resources/Images/on tourist.png";
            CurrentNotificationIconSource = "/Resources/Images/bell.png";
            CurrentRequestIconSource = "/Resources/Images/tour-request.png";
            CurrentChildView = new TouristUserViewModel();

        }

        public void ExecuteRequestCommand(object obj)
        {
            CurrentMarkerIconSource = "/Resources/Images/marker.png";
            CurrentHomeIconSource = "/Resources/Images/home.png";
            CurrentUserIconSource = "/Resources/Images/tourist.png";
            CurrentNotificationIconSource = "/Resources/Images/bell.png";
            CurrentRequestIconSource = "/Resources/Images/on-tour-request.png";
            CurrentChildView = new TourRequestViewModel();
      
        }

        /*public void ExecuteShowKeyPointsCommand(object obj)
        {
            /*if (obj is MainTouristView mainTouristView)
            {
                mainTouristView.MainFrame.Navigate(new FollowKeyPointsView());
            }
            CurrentChildView = new FollowKeyPointsViewModel();
        }
    */

    }
}
