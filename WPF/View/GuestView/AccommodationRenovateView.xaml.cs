using BookingApp.DTO;
using BookingApp.Model;
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
    /// Interaction logic for AccommodationRenovateView.xaml
    /// </summary>
    public partial class AccommodationRenovateView : Page
    {
        public AccommodationRenovateView(AccommodationRate ratedAccommodation)
        {
            InitializeComponent();
            DataContext = new AccommodationRenovateViewModel(ratedAccommodation);
        }

        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }
    }
}
