using BookingApp.DTO;
using BookingApp.WPF.ViewModel.TouristViewModel;
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

namespace BookingApp.WPF.View.TouristView
{
    /// <summary>
    /// Interaction logic for RateTourView.xaml
    /// </summary>
    public partial class RateTourView : Page
    {
        public RateTourView(FollowingTourDTO selectedTour)
        {
            InitializeComponent();
            DataContext = new RateTourViewModel(selectedTour);
        }


        

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        private void Rate_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new FollowTourView());
        }
    }
}
