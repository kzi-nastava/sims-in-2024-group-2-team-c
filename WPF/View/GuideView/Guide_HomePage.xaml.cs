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
        private void Follow_Click(object sender, RoutedEventArgs e)
        {
           Guide_FollowTour followTour = new Guide_FollowTour();
            this.NavigationService.Navigate(followTour);
        }

        private void SeeMore_Click(object sender, RoutedEventArgs e)
        {
            TourOverview tourOverviewWindow = new TourOverview();
            //tourOverviewWindow.Show();
            //Close();
            this.NavigationService.Navigate(tourOverviewWindow);
        }
        private void CreateNewTourButton_Click(object sender, RoutedEventArgs e)
        {
            //TourForm tourForm = new TourForm();
            //tourForm.Show();
            Guide_CreateTour createTourPage = new Guide_CreateTour();
            this.NavigationService.Navigate(createTourPage);
        }
        private void AllToursViewButton_Click(object sender, RoutedEventArgs e)
        {
            //TourOverview tourOverview = new TourOverview();
            //tourOverview.Show();
            //MessageBox.Show("ALL TOURS VIEW button clicked.");
            Guide_AllTourView allTourView = new Guide_AllTourView();
            this.NavigationService.Navigate(allTourView);
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
            Guide_Status guideStatus = new Guide_Status();
            this.NavigationService.Navigate(guideStatus);
        }
        private void Reviews_Click(object sender, RoutedEventArgs e)
        {
           // ReviewsOverview reviewsOverview = new ReviewsOverview();
           // reviewsOverview.Show();
            Guide_TourReviews reviewView = new Guide_TourReviews();
            this.NavigationService.Navigate(reviewView);
        }
        
        private void TourRequests_Click(object sender, RoutedEventArgs e)
        {
            Guide_TourRequests requestsView = new Guide_TourRequests();
            this.NavigationService.Navigate(requestsView);
        }
        private void RequestStatistic_Click(object sender, RoutedEventArgs e)
        {
            Guide_TourRequestStatistic requestStatistics = new Guide_TourRequestStatistic();
            this.NavigationService.Navigate(requestStatistics);
        }
        private void QuitJob_Click(object sender, RoutedEventArgs e)
        {
            Guide_QuitJob guide_QuitJob = new Guide_QuitJob();
            this.NavigationService.Navigate(guide_QuitJob);
        }

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            Window parentWindow = Window.GetWindow(this);
            parentWindow.Close();
            SignInForm signInForm = new SignInForm();
            signInForm.Show();
        }
        private void PlayTutorial_Click(object sender, RoutedEventArgs e)
        {
            string name = $"tutorial1";
            Guide_TutorialView tutorialView = new Guide_TutorialView(name);
            this.NavigationService.Navigate(tutorialView);
        }
    }
}
