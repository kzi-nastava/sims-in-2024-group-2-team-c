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
    /// Interaction logic for SuggestionsForAccommodations.xaml
    /// </summary>
    public partial class SuggestionsForAccommodations : Page
    {
        private OwnerStatisticsViewModel view_model;
        public SuggestionsForAccommodations()
        {
            InitializeComponent();
            view_model = new OwnerStatisticsViewModel();
            DataContext = view_model;
        }

        private void AddNewAccommodation_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("WPF\\View\\OwnerView\\RegisterAccommodationForm.xaml", UriKind.RelativeOrAbsolute));
        }
    }
}
