using BookingApp.Model;
using BookingApp.View;
using BookingApp.WPF.ViewModel.GuideViewModel;
using BookingApp.WPF.ViewModel.OwnerViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
            this.Loaded += BasePage_Loaded;
            App.StaticPropertyChanged += OnAppPropertyChanged;
            this.KeyDown += OwnerWindow_KeyDown;

        }

        private void BasePage_Loaded(object sender, RoutedEventArgs e)
        {
            SetLanguage();
        }

        private void OnAppPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(App.CurrentLanguage))
            {
                SetLanguage();
            }
        }
        private void SetLanguage()
        {
            string currentLanguage = App.CurrentLanguage;
            var newResource = new ResourceDictionary
            {
                Source = new Uri(currentLanguage, UriKind.Relative)
            };

            this.Resources.MergedDictionaries.Clear();
            this.Resources.MergedDictionaries.Add(newResource);
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Postavljanje selektovane rezervacije
            viewModel.SelectedReservation = (Reservation)guestListView.SelectedItem;
        }

        private void SaveGuestRating(object sender, RoutedEventArgs e)
        {
            
            if (string.IsNullOrEmpty(txtCleanliness.Text) || string.IsNullOrEmpty(txtRuleRespecting.Text))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }
            int cleanliness, ruleRespecting;
            if (!int.TryParse(txtCleanliness.Text, out cleanliness) || !int.TryParse(txtRuleRespecting.Text, out ruleRespecting))
            {
                MessageBox.Show("Invalid input. Please enter valid numbers.");
                return;
            }
            viewModel.SaveGuestRating(cleanliness, ruleRespecting, txtComment.Text.Trim().ToLower());
        }


        private void OwnersRatings_Click(object sender, RoutedEventArgs e)
        {
            // Svaka stranica ima referencu na NavigationService Frame kontrole unutar koje se nalazi, pa se i sa stranice moze vrsiti navigacija na neku drugu stranicu
            this.NavigationService.Navigate(new Uri("WPF\\View\\OwnerView\\OwnersRatings.xaml", UriKind.RelativeOrAbsolute));
            
        }

        private void OwnerWindow_KeyDown(object sender, KeyEventArgs e)
        {
            // Proverite koji taster je pritisnut
            switch (e.Key)
            {
                case Key.RightCtrl:
                    SaveGuestRating(null, null);
                    break;
                default:
                    break;
            }
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.Focus();
        }
    }
}