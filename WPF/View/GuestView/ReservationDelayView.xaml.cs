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
    /// Interaction logic for ReservationDelayView.xaml
    /// </summary>
    public partial class ReservationDelayView : Page
    {
        public ReservationDelayView(GuestReservationDTO selectedReservation)
        {
            InitializeComponent();
            DataContext = new ReservationDelayViewModel(selectedReservation);
        }

        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }
    }
}
