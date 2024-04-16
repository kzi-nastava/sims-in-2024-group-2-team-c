using BookingApp.DTO;
using BookingApp.Service.TourServices;
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
    /// Interaction logic for Guide_CancelTour.xaml
    /// </summary>
    public partial class Guide_CancelTour : Page
    {
        public Guide_CancelTour()
        {
            InitializeComponent();
            DataContext = new CancelTour_ViewModel();
        }
        private void FutureToursView_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (tourView.SelectedItem != null)
            {
                var viewModel = DataContext as CancelTour_ViewModel;
                viewModel.SelectedTour = (FutureTourDTO)tourView.SelectedItem;
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }

        private void CancelTourButton_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as CancelTour_ViewModel;
            viewModel.CancelTourCommand.Execute(null);
        }
    }
}
