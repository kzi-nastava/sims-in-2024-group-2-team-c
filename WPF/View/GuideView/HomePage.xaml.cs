using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Service.TourServices;
using BookingApp.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BookingApp.WPF.View.GuideView
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Window
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
                OnPropertyChanged();
            }
        }
        private TourStatisticDTO _statisticTour;
        public TourStatisticDTO StatisticTour
        {
            get { return _statisticTour; }
            set
            {
                _statisticTour = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public HomePage()
        {
            InitializeComponent();
            _tourService = new(new TourRepository());
            _endedToursService = new EndedToursService();
            ActiveTour = _tourService.GetByActivity();
            if (ActiveTour == null)
            {
                NoActiveTourBlock.Visibility = Visibility.Visible;
                NoActiveTourButton.Visibility = Visibility.Visible;
                ActiveTourBlock.Visibility = Visibility.Hidden;
                ActiveTourBlock2.Visibility = Visibility.Hidden;
                ActiveTourButton.Visibility = Visibility.Hidden;
            }
            else
            {
                ActiveTourBlock.Visibility = Visibility.Visible;
                ActiveTourBlock2.Visibility = Visibility.Visible;
                ActiveTourButton.Visibility = Visibility.Visible;
                NoActiveTourBlock.Visibility = Visibility.Hidden;
                NoActiveTourButton.Visibility = Visibility.Hidden;
            }
            StatisticTour = _endedToursService.FindMostVisitedTour();

            DataContext = this;
        }
        /*private void LoadLocation(Location location)
        {
            Location = location;
            LocationTextBlock.Text = $"{location.City}, {location.Country}";
        }*/ 
        private void TodaysTour_Click(object sender, RoutedEventArgs e)
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
        }

    }
}
