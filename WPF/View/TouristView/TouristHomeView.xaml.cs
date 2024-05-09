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
    /// Interaction logic for TouristHomeView.xaml
    /// </summary>
    public partial class TouristHomeView : Page
    {
        public TouristHomeView()
        {
            InitializeComponent();
            DataContext = new TouristHomeViewModel();
        }


        private void SearchTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new SearchTourView());
            // NavigationService.Navigate(new Uri("YourPage.xaml", UriKind.Relative));
        }

        private void ViewTour_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            NavigateToNewPage(button);
        }


        private void NavigateToNewPage(Button button)
        {
            if (button?.DataContext is HomeTourDTO selectedTour)
            {

                SelectedTourViewModel selectedTourViewModel = new SelectedTourViewModel(selectedTour);
                SelectedTourView selectedTourView = new SelectedTourView();
                selectedTourView.DataContext = selectedTourViewModel;
                this.NavigationService.Navigate(selectedTourView);
            }
        }



    }
}
