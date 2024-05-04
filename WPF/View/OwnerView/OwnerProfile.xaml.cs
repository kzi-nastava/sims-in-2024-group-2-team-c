using BookingApp.Repository;
using BookingApp.Service.OwnerService;
using BookingApp.View;
using BookingApp.WPF.ViewModel.OwnerViewModel;
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

namespace BookingApp.WPF.View.OwnerView
{
    /// <summary>
    /// Interaction logic for OwnerProfile.xaml
    /// </summary>
    public partial class OwnerProfile : Page
    {
        private OwnerProfileViewModel _viewModel;
        
        public OwnerProfile()
        {
            InitializeComponent();
            _viewModel = new OwnerProfileViewModel();
            DataContext = _viewModel;
        }
        private void NotificationsAndRequests_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("WPF\\View\\OwnerView\\ReservationDelayForm.xaml", UriKind.RelativeOrAbsolute));
        }

        private void LogOut(object sender, RoutedEventArgs e)
        {
            _viewModel.LogOut();
            Application.Current.Shutdown();
            // Otvori SignInForm prozor
            //var signInFormWindow = new SignInForm();
            //signInFormWindow.Show();
     
            // Zatvori aplikaciju samo
        }

        
    }
}
