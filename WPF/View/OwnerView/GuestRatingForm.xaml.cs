using BookingApp.Model;
using BookingApp.View;
using BookingApp.WPF.ViewModel.GuideViewModel;
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
using System.Xml.Linq;

namespace BookingApp.WPF.View.OwnerView
{
    /// <summary>
    /// Interaction logic for GuestRatingFormm.xaml
    /// </summary>
    public partial class GuestRatingForm : Page
    {

        private GuestRatingFormViewModel viewModel;
  
        public GuestRatingForm()
        {
            InitializeComponent();
            viewModel = new GuestRatingFormViewModel();
            DataContext = viewModel;
            
        }

        

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Postavljanje selektovane rezervacije
            viewModel.SelectedReservation = (Reservation)guestListView.SelectedItem;
        }

        private void SaveGuestRating(object sender, RoutedEventArgs e)
        {
            // Pozivanje metode za čuvanje ocene gosta
            viewModel.SaveGuestRating(
                int.Parse(txtCleanliness.Text),
                int.Parse(txtRuleRespecting.Text),
                txtComment.Text.Trim().ToLower()
            );
   
        }

        /*private void OwnersRatings_Click(object sender, RoutedEventArgs e)
        {
            // Svaka stranica ima referencu na NavigationService Frame kontrole unutar koje se nalazi, pa se i sa stranice moze vrsiti navigacija na neku drugu stranicu
            this.NavigationService.Navigate(new Uri("\\WPF\\View\\OwnerView\\OwnersRatings.xaml", UriKind.RelativeOrAbsolute));
            
        }
        */

      

    }
}