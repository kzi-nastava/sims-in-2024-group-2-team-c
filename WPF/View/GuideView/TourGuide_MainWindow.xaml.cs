using BookingApp.WPF.ViewModel.GuideViewModel;
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

namespace BookingApp.WPF.View.GuideView
{
    /// <summary>
    /// Interaction logic for TourGuide_MainWindow.xaml
    /// </summary>
    public partial class TourGuide_MainWindow : Window
    {
        public TourGuide_MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindow_ViewModel();
        }
        /*private void NavigateToHomePage()
        {
            MainFrame.Source = new Uri("WPF/View/GuideView/HomePage.xaml", UriKind.Relative);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            NavigateToHomePage();
        }
        private void NavigateToPage(string pageName)
        {
            String pageUri = "Pages/" + pageName + ".xaml"; // ovo je nacin sa putanjama, a moze da se instancira i nova stranica prilikom navigacije, pa da ne moraju da se koriste putanje, ali ima neke razlike u ponasanju stranica prilikom navigacije (procitati na linku)
            MainFrame.Navigate(new Uri(pageUri, UriKind.RelativeOrAbsolute)); // ovo je skraceni zapis za MainFrame.NavigationService.Navigate(...);
        }

        // Alternativni nacin za navigaciju - Prosledjuje se objekat stranice prilikom navigacije
        private void NavigateToPage(Page page)
        {
            MainFrame.Navigate(page);
        }*/
    }
}
