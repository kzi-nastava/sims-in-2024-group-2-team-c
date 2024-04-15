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
    /// Interaction logic for FollowTourView.xaml
    /// </summary>
    public partial class FollowTourView : Page
    {

        
        public FollowTourView()
        {
            InitializeComponent();
            DataContext = new FollowTourViewModel();
        }

        /*private void View_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("WPF/View/TouristView/FollowKeyPointsView.xaml", UriKind.RelativeOrAbsolute));
        }*/


        private void View_Click(object sender, RoutedEventArgs e)
        {
            
            Button button = sender as Button;

            if (button?.DataContext is FollowingTourDTO selectedTour)
            {
                
                FollowKeyPointsViewModel followKeyPointsViewModel = new FollowKeyPointsViewModel(selectedTour);

                
                FollowKeyPointsView followKeyPointsView = new FollowKeyPointsView();

               
                followKeyPointsView.DataContext = followKeyPointsViewModel;

                
                this.NavigationService.Navigate(followKeyPointsView);
            }
        }

    }
}
