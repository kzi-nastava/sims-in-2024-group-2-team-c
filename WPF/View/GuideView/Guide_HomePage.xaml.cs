using BookingApp.View;
using BookingApp.WPF.View.TouristView;
using BookingApp.WPF.ViewModel.GuideViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookingApp.WPF.View.GuideView
{
    /// <summary>
    /// Interaction logic for Guide_HomePage.xaml
    /// </summary>
    public partial class Guide_HomePage : Page
    {
        public Guide_HomePage()
        {
            InitializeComponent();
            DataContext = new GuideHomePage_ViewModel();
        }
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
            //FutureToursOverview futureTourOverview = new FutureToursOverview();
            //futureTourOverview.Show();
            Guide_CancelTour cancelTourPage = new Guide_CancelTour();
            this.NavigationService.Navigate(cancelTourPage);
            //mainFrame.Navigate(cancelTourPage);
            //MessageBox.Show("FUTURE TOURS button clicked.");
        }

        private void SeeTourStatisticButton_Click(object sender, RoutedEventArgs e)
        {
            //TourStatisticView tourStatisticView = new TourStatisticView();
            Guide_TourStatistic tourStatisticPage = new Guide_TourStatistic();
            this.NavigationService.Navigate(tourStatisticPage);
            // tourStatisticView.Show();
            //MessageBox.Show("SEE TOUR STATISTIC button clicked.");
        }
        private void SeeTourStatisticDetailsButton_Click(object sender, RoutedEventArgs e)
        {
            Guide_TourStatisticDetails tourStatisticDetails = new Guide_TourStatisticDetails();
            this.NavigationService.Navigate(tourStatisticDetails);
        }
        private void SeeStatus_Click(object sender, RoutedEventArgs e)
        {
            //
        }
        private void Reviews_Click(object sender, RoutedEventArgs e)
        {
            ReviewsOverview reviewsOverview = new ReviewsOverview();
            reviewsOverview.Show();
            //Guide_TourReviews reviewView = new Guide_TourReviews();
            //this.NavigationService.Navigate(reviewView);
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
