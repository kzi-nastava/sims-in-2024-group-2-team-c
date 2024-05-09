using BookingApp.DTO;
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
using System.Windows.Shapes;

namespace BookingApp.WPF.View.GuestView
{
    /// <summary>
    /// Interaction logic for RateAccommodationWindow.xaml
    /// </summary>
    public partial class RateAccommodationWindow : Page
    {

        private GuestReservationDTO _selectedReservation;
        public RateAccommodationWindow(GuestReservationDTO selectedReservation)
        {
            InitializeComponent();
            _selectedReservation = selectedReservation;
            DataContext = new RateAccommodationViewModel(selectedReservation);
        }

        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }
    }
}
