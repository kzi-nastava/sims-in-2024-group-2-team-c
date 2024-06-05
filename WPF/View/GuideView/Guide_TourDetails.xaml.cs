using BookingApp.Model;
using BookingApp.Service.TourServices;
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
    /// Interaction logic for Guide_TourDetails.xaml
    /// </summary>
    public partial class Guide_TourDetails : Page
    {
        private readonly KeyPointService _keyPointService;
        public Guide_TourDetails()
        {
            InitializeComponent();
            DataContext = new TourDetails_ViewModel();
            _keyPointService = new KeyPointService();
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }
        private void HomePageButton_Click(object sender, RoutedEventArgs e)
        {
            Guide_HomePage home = new Guide_HomePage();
            this.NavigationService?.Navigate(home);
        }
        private void ViewKeyPointsButton_Click(object sender, RoutedEventArgs e)
        {
            var MainViewModel = DataContext as FollowTour_ViewModel;
            List<KeyPoint> keyPoints = _keyPointService.GetKeypointsByIds(MainViewModel.Tour.KeyPointIds);
            ActiveKeyPoint_ViewModel viewModel = new ActiveKeyPoint_ViewModel
            {
                toursKeyPoints = keyPoints
            };
            Guide_ActivateKeyPoints newPage = new Guide_ActivateKeyPoints();
            newPage.DataContext = viewModel;

            this.NavigationService.Navigate(newPage);
            //Guide_ActivateKeyPoints keyPointsView = new Guide_ActivateKeyPoints();
            //this.NavigationService?.Navigate(keyPointsView);
        }
    }
}
