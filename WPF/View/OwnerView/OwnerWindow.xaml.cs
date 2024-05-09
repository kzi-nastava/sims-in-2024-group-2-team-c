using BookingApp.View;
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
            OwnerHomePage ownerHomePage = new OwnerHomePage();
            this.Content = ownerHomePage;
        }

        private void Ratings_Click(object sender, RoutedEventArgs e)
        {
            GuestRatingForm guestRatingForm = new GuestRatingForm();
            this.Content = guestRatingForm;
        }

        //sve ostale uradi ovako, samo pazi da se ne preklapa tekst gore!
        private void Profile_Click(object sender, RoutedEventArgs e)
        {
            NavigateToPage("OwnerProfile");
        }

        private void OwnersRatings_Click(object sender, RoutedEventArgs e)
        {
            NavigateToPage("OwnersRatings");
        }


        private void NavigateToPage(string pageName)
        {
            String pageUri = "WPF\\View\\OwnerView\\" + pageName + ".xaml"; // ovo je nacin sa putanjama, a moze da se instancira i nova stranica prilikom navigacije, pa da ne moraju da se koriste putanje, ali ima neke razlike u ponasanju stranica prilikom navigacije (procitati na linku)
            MainFrame.Navigate(new Uri(pageUri, UriKind.RelativeOrAbsolute)); // ovo je skraceni zapis za MainFrame.NavigationService.Navigate(...);
        }



        /* stari nacin
         private void Profile_Click(object sender, RoutedEventArgs e)
         {
             OwnerProfile ownerProfile = new OwnerProfile();
             this.Content = ownerProfile;
      
         }*/

        

    }
}
