using BookingApp.Commands;
using BookingApp.Model;
using BookingApp.View;
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
using System.Windows.Shapes;

namespace BookingApp.WPF.View.OwnerView
{
    /// <summary>
    /// Interaction logic for OwnerWindow.xaml
    /// </summary>
    public partial class OwnerWindow : Window
    {
        
        public OwnerWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new OwnerHomePage());
        }
        private void Home_Click(object sender, RoutedEventArgs e)
        {
            //SetPageLanguageBasedOnWindowLanguage();
            NavigateToPage("OwnerHomePage");
        }

        private void Ratings_Click(object sender, RoutedEventArgs e)
        {
            NavigateToPage("GuestRatingForm");
        }

        private void Renovations_Click(object sender, RoutedEventArgs e)
        {
            NavigateToPage("RenovationsList");
        }

        
        private void Profile_Click(object sender, RoutedEventArgs e)
        {
            NavigateToPage("OwnerProfile");
        }

        private void OwnersRatings_Click(object sender, RoutedEventArgs e)
        {
            NavigateToPage("OwnersRatings");
        }

        private void Accommodations_Click(object sender, RoutedEventArgs e)
        {
            NavigateToPage("AllAccommodationsPage");
        }

        private void Statistics_Click(object sender, RoutedEventArgs e)
        {
            NavigateToPage("OwnerStatisticsForm");
        }

        private void NavigateToPage(string pageName)
        {
            string pageUri = "WPF\\View\\OwnerView\\" + pageName + ".xaml";
            MainFrame.Navigate(new Uri(pageUri, UriKind.Relative));
        }

     
        //-------PROMENA JEZIKA I TEME------
        private void ChangeLanguageToSerbian_Click(object sender, RoutedEventArgs e)
         {
             App.ChangeLanguage("\\Resources\\ResourcesLanSerbian.xaml");
         }

         private void ChangeLanguageToEnglish_Click(object sender, RoutedEventArgs e)
         {
             App.ChangeLanguage("\\Resources\\ResourcesLan.xaml");
         }
        

        private void ChangeThemeToLight_Click(object sender, RoutedEventArgs e)
        {
            App.ChangeTheme("\\Resources\\Themes\\LightTheme.xaml");
        }

        private void ChangeThemeToDark_Click(object sender, RoutedEventArgs e)
        {
            App.ChangeTheme("\\Resources\\Themes\\DarkTheme.xaml");
        }

    }
}
