using BookingApp.Repository;
using BookingApp.Service.OwnerService;
using BookingApp.View;
using BookingApp;
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

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to log out?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                
                SignInForm signInForm = new SignInForm();
                signInForm.Show();
                
                Window parentWindow = Window.GetWindow(this);
                if (parentWindow != null)
                {
                    parentWindow.Close();
                }
            }
        }

        //------PROMENA JEZIKA I TEME ------

        /* private void ChangeLanguageToSerbian_Click(object sender, RoutedEventArgs e)
         {
             ChangeLanguage("/Resources/ResourcesLanSerbian.xaml");
         }

         private void ChangeLanguageToEnglish_Click(object sender, RoutedEventArgs e)
         {
             ChangeLanguage("/Resources/ResourcesLan.xaml");
         }

         private void ChangeLanguage(string resourcePath)
         {
             try
             {
                 // Uklonite trenutne resurse
                 Application.Current.Resources.MergedDictionaries.Clear();

                 // Učitajte novi resursni fajl
                 ResourceDictionary newResource = new ResourceDictionary();
                 newResource.Source = new Uri(resourcePath, UriKind.Relative);

                 // Dodajte novi resursni fajl u MergedDictionaries
                 Application.Current.Resources.MergedDictionaries.Add(newResource);

                 // Osvežite trenutnu stranicu
                 this.Resources.MergedDictionaries.Clear();
                 this.Resources.MergedDictionaries.Add(newResource);

                 MessageBox.Show("Language changed successfully.");
             }
             catch (Exception ex)
             {
                 MessageBox.Show($"Error changing language: {ex.Message}");
             }
         } */

        private void ChangeLanguageToSerbian_Click(object sender, RoutedEventArgs e)
        {
            App.ChangeLanguage("/Resources/ResourcesLanSerbian.xaml");
        }

        private void ChangeLanguageToEnglish_Click(object sender, RoutedEventArgs e)
        {
            App.ChangeLanguage("/Resources/ResourcesLan.xaml");
        }

        private void ChangeThemeToLight_Click(object sender, RoutedEventArgs e)
        {
            App.ChangeTheme("/Resources/Themes/LightTheme.xaml");
        }

        private void ChangeThemeToDark_Click(object sender, RoutedEventArgs e)
        {
            App.ChangeTheme("/Resources/Themes/DarkTheme.xaml");
        }



    }
}
