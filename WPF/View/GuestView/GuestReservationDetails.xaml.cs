using BookingApp.WPF.ViewModel.GuestViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookingApp.WPF.View.GuestView
{
    /// <summary>
    /// Interaction logic for GuestReservationDetails.xaml
    /// </summary>
    public partial class GuestReservationDetails : Page
    {

        private readonly MainGuestWindow _mainGuestWindow;
        private readonly NavigationService _navigationService;

        public GuestReservationDetails(MainGuestWindow mainGuestWindow, NavigationService navigationService)
        {
            InitializeComponent();
            DataContext = new GuestReservationDetailsViewModel(mainGuestWindow, navigationService);
            _mainGuestWindow = mainGuestWindow;
            _navigationService = navigationService;
        }

        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }

    }

}
