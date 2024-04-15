using BookingApp.Commands;
using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Service.TourServices;
using BookingApp.View;
using BookingApp.WPF.View.GuideView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModel.GuideViewModel
{
    internal class GuideHomePage_ViewModel : ViewModelBase
    {
        private readonly TourService _tourService;
        private readonly EndedToursService _endedToursService;
        private Tour _activeTour;
        public Tour ActiveTour
        {
            get { return _activeTour; }
            set
            {
                _activeTour = value;
                OnPropertyChanged(nameof(ActiveTour));
                OnPropertyChanged(nameof(IsNoActiveTourVisible));
                OnPropertyChanged(nameof(IsActiveTourVisible));
            }
        }
        private bool _isActiveTourVisible;
        public bool IsActiveTourVisible
        {
            get { return _isActiveTourVisible; }
            set
            {
                _isActiveTourVisible = value;
                OnPropertyChanged(nameof(IsActiveTourVisible));
            }
        }
        private TourStatisticDTO _statisticTour;
        public TourStatisticDTO StatisticTour
        {
            get { return _statisticTour; }
            set
            {
                _statisticTour = value;
                OnPropertyChanged(nameof(StatisticTour));
            }
        }
        private string _username;
        public string Username
        {
            get { return _username; }
            set
            {
                if (_username != value)
                {
                    _username = value;
                    OnPropertyChanged(nameof(Username));
                }
            }
        }
        public bool IsNoActiveTourVisible => ActiveTour == null;
       // public bool IsActiveTourVisible => ActiveTour != null;
        /*public ICommand SeeStatusCommand { get; } // Implementirajte svoju logiku za komandu "SeeStatus"
        public ICommand SeeMoreCommand { get; } // Implementirajte svoju logiku za komandu "SeeStatus"

        public ICommand ReviewsCommand { get; } // Implementirajte svoju logiku za komandu "Reviews"

        public ICommand TourRequestsCommand { get; } // Implementirajte svoju logiku za komandu "TourRequests"

        public ICommand RequestStatisticCommand { get; } // Implementirajte svoju logiku za komandu "RequestStatistic"

        public ICommand QuitJobCommand { get; } // Implementirajte svoju logiku za komandu "QuitJob"

        public ICommand LogOutCommand { get; } // Implementirajte svoju logiku za komandu "LogOut"

        public ICommand PlayTutorialCommand { get; } // Implementirajte svoju logiku za komandu "PlayTutorial"

        public ICommand TodaysTourCommand { get; } // Implementirajte svoju logiku za komandu "TodaysTour"

        public ICommand CreateNewTourCommand { get; } // Implementirajte svoju logiku za komandu "CreateNewTour"

        public ICommand AllToursViewCommand { get; } // Implementirajte svoju logiku za komandu "AllToursView"

        public ICommand FutureToursCommand { get; } // Implementirajte svoju logiku za komandu "FutureTours"

        public ICommand SeeTourStatisticCommand { get; } // Implementirajte svoju logiku za komandu "SeeTourStatistic"
        */
        public GuideHomePage_ViewModel()
        {
            // Inicijalizacija podataka ili logike po potrebi
            Username = LoggedInUser.Username;
            _tourService = new(new TourRepository());
            _endedToursService = new EndedToursService();
            ActiveTour = _tourService.GetByActivity();
            if (ActiveTour == null)
            {
                IsActiveTourVisible = false;
            }
            else
            {
                IsActiveTourVisible = true;
            }
            StatisticTour = _endedToursService.FindMostVisitedTour();

           /* TodaysTourCommand = new RelayCommand(parameter => TodaysTour_Click(parameter));
            SeeMoreCommand = new RelayCommand(parameter => SeeMore_Click(parameter));
            CreateNewTourCommand = new RelayCommand(parameter => CreateNewTourButton_Click(parameter));
            AllToursViewCommand = new RelayCommand(parameter => AllToursViewButton_Click(parameter));
            FutureToursCommand = new RelayCommand(parameter => FutureToursButton_Click(parameter));
            SeeTourStatisticCommand = new RelayCommand(parameter => SeeTourStatisticButton_Click(parameter));
            SeeStatusCommand = new RelayCommand(parameter => SeeStatus_Click(parameter));
            ReviewsCommand = new RelayCommand(parameter => Reviews_Click(parameter));
            TourRequestsCommand = new RelayCommand(parameter => TourRequests_Click(parameter));
            RequestStatisticCommand = new RelayCommand(parameter => RequestStatistic_Click(parameter));
            QuitJobCommand = new RelayCommand(parameter => QuitJob_Click(parameter));
            LogOutCommand = new RelayCommand(parameter => LogOut_Click(parameter));
            PlayTutorialCommand = new RelayCommand(parameter => PlayTutorial_Click(parameter));*/



        }
        /*private void TodaysTour_Click(object sender, RoutedEventArgs e)
        {
            // Implementacija logike za klik na dugme "TODAY'S TOUR"
        }

        private void SeeMore_Click(object sender, RoutedEventArgs e)
        {
            // Implementacija logike za klik na dugme "SEE MORE"
        }
        private void CreateNewTourButton_Click(object sender, RoutedEventArgs e)
        {
            TourForm tourForm = new TourForm();
            tourForm.Show();
            //MessageBox.Show("ALL TOURS VIEW button clicked.");
        }
        private void AllToursViewButton_Click(object sender, RoutedEventArgs e)
        {
            TourOverview tourOverview = new TourOverview();
            tourOverview.Show();
            //MessageBox.Show("ALL TOURS VIEW button clicked.");
        }

        private void FutureToursButton_Click(object sender, RoutedEventArgs e)
        {
            FutureToursOverview futureTourOverview = new FutureToursOverview();
            futureTourOverview.Show();
            //MessageBox.Show("FUTURE TOURS button clicked.");
        }

        private void SeeTourStatisticButton_Click(object sender, RoutedEventArgs e)
        {
            TourStatisticView tourStatisticView = new TourStatisticView();
            tourStatisticView.Show();
            //MessageBox.Show("SEE TOUR STATISTIC button clicked.");
        }
        private void SeeStatus_Click(object sender, RoutedEventArgs e)
        {
            //
        }
        private void Reviews_Click(object sender, RoutedEventArgs e)
        {
            ReviewsOverview reviewsOverview = new ReviewsOverview();
            reviewsOverview.Show();
        }
        private void TourRequests_Click(object sender, RoutedEventArgs e)
        {
            //
        }
        private void RequestStatistic_Click(object sender, RoutedEventArgs e)
        {
            //
        }
        private void QuitJob_Click(object sender, RoutedEventArgs e)
        {
            //
        }

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            //
        }
        private void PlayTutorial_Click(object sender, RoutedEventArgs e)
        {
            //
        }*/

    }
}
