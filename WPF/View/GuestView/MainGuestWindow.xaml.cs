using BookingApp.Model;
using BookingApp.Service;
using BookingApp.WPF.ViewModel.GuestViewModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;

namespace BookingApp.WPF.View.GuestView
{
    /// <summary>
    /// Interaction logic for MainGuestWindow.xaml
    /// </summary>
    public partial class MainGuestWindow : Window
    {

        public System.Windows.Navigation.NavigationService NavigationService { get; internal set; }

        public MainGuestWindow()
        {
            InitializeComponent();
            DataContext = new MainGuestWindowViewModel(MainFrame.NavigationService, this);
            AccommodationOverview accommodationOverview = new AccommodationOverview(this);
            MainFrame.Navigate(accommodationOverview);
        }

        public void ChangeHeaderText(string newText)
        {
            HeaderTextBlock.Text = newText;
        }

    }
}
