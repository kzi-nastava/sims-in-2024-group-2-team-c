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
    /// Interaction logic for FollowKeyPointsView.xaml
    /// </summary>
    public partial class FollowKeyPointsView : Page
    {
        public FollowKeyPointsView()
        {
            InitializeComponent();
        }

      /*  private void Return_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }*/

        private void Rate_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = (FollowKeyPointsViewModel)DataContext;

            var selectedTour = viewModel.SelectedTour;
            RateTourView rateTourView = new RateTourView(selectedTour);
            this.NavigationService.Navigate(rateTourView);
        }
    }
}
