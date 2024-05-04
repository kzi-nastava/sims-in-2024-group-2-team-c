using BookingApp.Model;
using BookingApp.View;
using BookingApp.WPF.ViewModel.TouristViewModel;
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

namespace BookingApp.WPF.View.TouristView
{
    /// <summary>
    /// Interaction logic for TouristUserView.xaml
    /// </summary>
    public partial class TouristUserView : Page
    {
        public TouristUserView()
        {
            InitializeComponent();
            DataContext = new TouristUserViewModel();
        }

        private void View_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new TouristVouchersView());
        }

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            Window parentWindow = Window.GetWindow(this);
            parentWindow.Close();

            // Open the SignInForm window
            SignInForm signInForm = new SignInForm();
            signInForm.Show();

        }

        private void TourRequest_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new TourRequestCreationView());
        }

        private void Statistics_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new RequestStatisticsView());
        }

        private void ComplexTour_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ComplexTourRequestView());
        }

        private void ViewComplexTour_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ShowAllComplexToursView());
        }
    }
}
