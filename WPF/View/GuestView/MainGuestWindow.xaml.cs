using BookingApp.Model;
using BookingApp.WPF.ViewModel.GuestViewModel;
using System.Windows;

namespace BookingApp.WPF.View.GuestView
{
    /// <summary>
    /// Interaction logic for MainGuestWindow.xaml
    /// </summary>
    public partial class MainGuestWindow : Window
    {
        public MainGuestWindow()
        {
            InitializeComponent();

            DataContext = new MainGuestWindowViewModel();

            //MainFrame.Navigate(new Uri("AccommodationOverview.xaml", UriKind.Relative));
            MainFrame.Navigate(new AccommodationOverview());

        }
    }
}
