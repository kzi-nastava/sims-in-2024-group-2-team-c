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
    /// Interaction logic for Guide_AllTourView.xaml
    /// </summary>
    public partial class Guide_AllTourView : Page
    {
        public Guide_AllTourView()
        {
            InitializeComponent();
            DataContext = new AllTour_ViewModel();
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }
        private void HomePage_Click(object sender, RoutedEventArgs e)
        {
            Guide_HomePage home = new Guide_HomePage();
            this.NavigationService?.Navigate(home);
        }
        /*private void Details_Click(object sender, RoutedEventArgs e)
        {
            Guide_TourDetails details = new Guide_TourDetails();
            var MainViewModel = DataContext as AllTour_ViewModel;
            MainViewModel.AcceptRequest();
            TourDetails_ViewModel viewModel = new TourDetails_ViewModel
            {
                Tour = MainViewModel.SelectedTour
            };
            Guide_AcceptingTourRequest acceptTourPage = new Guide_AcceptingTourRequest();
            acceptTourPage.DataContext = viewModel;
            this.NavigationService.Navigate(acceptTourPage);
        }*/
    }
}
