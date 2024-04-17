using BookingApp.Repository;
using BookingApp.Service.OwnerService;
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
            OwnerRepository ownerRepository = new OwnerRepository(); 
            SuperOwnerService superOwnerService = new SuperOwnerService(ownerRepository);
            _viewModel = new OwnerProfileViewModel( ownerRepository);

           
            DataContext = _viewModel;
        }
    //page to page
        private void NotificationsAndRequests_Click(object sender, RoutedEventArgs e)
        {
            // Svaka stranica ima referencu na NavigationService Frame kontrole unutar koje se nalazi, pa se i sa stranice moze vrsiti navigacija na neku drugu stranicu
            this.NavigationService.Navigate(new Uri("WPF\\View\\OwnerView\\ReservationDelayForm.xaml", UriKind.RelativeOrAbsolute));
        }
    }
}
