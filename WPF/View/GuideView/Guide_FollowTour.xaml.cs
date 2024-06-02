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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookingApp.WPF.View.GuideView
{
    /// <summary>
    /// Interaction logic for Guide_FollowTour.xaml
    /// </summary>
    public partial class Guide_FollowTour : Page
    {
        public Guide_FollowTour()
        {
            InitializeComponent();
            DataContext = new FollowTour_ViewModel();
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }
        private void HomePageButton_Click(object sender, RoutedEventArgs e)
        {
            Guide_HomePage home = new Guide_HomePage();
            this.NavigationService?.Navigate(home);
        }
        private void ViewKeyPointsButton_Click(object sender, RoutedEventArgs e)
        {
            // Implementacija
        }
    }
}
