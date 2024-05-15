using BookingApp.Model;
using BookingApp.WPF.ViewModel.OwnerViewModel;
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



namespace BookingApp.WPF.View.OwnerView
{
    /// <summary>
    /// Interaction logic for RenovationAvailableDates.xaml
    /// </summary>
    public partial class RenovationAvailableDates : Page
    {
        private RenovationAvailableDatesViewModel viewModel;
        public RenovationAvailableDates(Accommodation selectedAccommodation, DateTime startDate, DateTime endDate, int duration)
        {
            InitializeComponent();

            viewModel = new RenovationAvailableDatesViewModel(selectedAccommodation, startDate, endDate, duration);
            DataContext = viewModel;
        }

      

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Save();
            this.NavigationService.Navigate(new Uri("WPF\\View\\OwnerView\\RenovationsList.xaml", UriKind.RelativeOrAbsolute));
        }
    }
}
