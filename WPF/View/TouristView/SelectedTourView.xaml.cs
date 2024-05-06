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
    /// Interaction logic for SelectedTourView.xaml
    /// </summary>
    public partial class SelectedTourView : Page
    {
        public SelectedTourView()
        {
            InitializeComponent();
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }


        private void BookButton_Click(object sender, RoutedEventArgs e)
        {
            /*if (TourInstancesListView.SelectedItem != null)
            {
                var selectedTour = (YourTourItemType)TourInstancesListView.SelectedItem;
                var bookTourViewModel = new BookTourViewModel(selectedTour);
                var bookTourView = new BookTourView();
                bookTourView.DataContext = bookTourViewModel;
                // Navigate to the BookTourView
                // This depends on your navigation mechanism, such as Frame.Navigate() in UWP or NavigationService.Navigate() in WPF
            }
            else
            {
                // Handle the case where no item is selected
            }*/

            this.NavigationService.Navigate(new BookTourView());
        }



    }
}
