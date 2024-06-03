using BookingApp.Commands;
using BookingApp.View;
using BookingApp.WPF.ViewModel.OwnerViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
using BookingApp.Resources.Language;

namespace BookingApp.WPF.View.OwnerView
{
    /// <summary>
    /// Interaction logic for OwnerHomePage.xaml
    /// </summary>
    public partial class OwnerHomePage : Page
    {

        public OwnerHomePage()
        {
            InitializeComponent();
            this.Loaded += BasePage_Loaded;
            App.StaticPropertyChanged += OnAppPropertyChanged;
        }

        private void NavigateToGuestRating_Click(object sender, RoutedEventArgs e)
        {
             this.NavigationService.Navigate(new Uri("WPF\\View\\OwnerView\\GuestRatingForm.xaml", UriKind.RelativeOrAbsolute));
        }

        private void NavigateToRegisterAccommodation_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("WPF\\View\\OwnerView\\RegisterAccommodationForm.xaml", UriKind.RelativeOrAbsolute));
        }

        private void NavigateToScheduleRenovation_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("WPF\\View\\OwnerView\\ScheduleRenovation.xaml", UriKind.RelativeOrAbsolute));
        }

            private void NavigateToStatistics_Click(object sender, RoutedEventArgs e)
            {
            this.NavigationService.Navigate(new Uri("WPF\\View\\OwnerView\\OwnerStatisticsForm.xaml", UriKind.RelativeOrAbsolute));
            //this.NavigationService.Navigate(new Uri("WPF\\View\\OwnerView\\AccommodationStatistics.xaml", UriKind.RelativeOrAbsolute));
            }

        private void ViewAccommodations_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("WPF\\View\\OwnerView\\AllAccommodationsPage.xaml", UriKind.RelativeOrAbsolute));
        }


         public event PropertyChangedEventHandler? PropertyChanged;
         protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
         {
             PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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

    }
}
