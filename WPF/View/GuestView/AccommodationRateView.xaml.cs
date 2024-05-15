using BookingApp.DTO;
using BookingApp.Service;
using BookingApp.WPF.ViewModel.GuestViewModel;
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

namespace BookingApp.WPF.View.GuestView
{
    /// <summary>
    /// Interaction logic for AccommodationRateView.xaml
    /// </summary>
    public partial class AccommodationRateView : Page
    {
        private readonly System.Windows.Navigation.NavigationService _navigationService;
        private GuestReservationDTO _selectedReservation;
        public AccommodationRateView(GuestReservationDTO selectedReservation, System.Windows.Navigation.NavigationService navigationService)
        {
            InitializeComponent();
            _selectedReservation = selectedReservation;
            DataContext = new AccommodationRateViewModel(selectedReservation, navigationService);
            _navigationService = navigationService;

        }

        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }

    }
}
