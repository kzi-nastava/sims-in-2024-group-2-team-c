using BookingApp.Model;
using BookingApp.WPF.ViewModel.GuestViewModel;
using System.Windows;
using System.Windows.Navigation;

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
            AccommodationOverview accommodationOverview = new AccommodationOverview(this);
            MainFrame.Navigate(accommodationOverview);
        }

        public NavigationService NavigationService { get; internal set; }

        public void ChangeHeaderText(string newText)
        {
            HeaderTextBlock.Text = newText;
        }
    }
}
