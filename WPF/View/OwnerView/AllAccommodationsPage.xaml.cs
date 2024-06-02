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
    /// Interaction logic for AllAccommodationsPage.xaml
    /// </summary>
    public partial class AllAccommodationsPage : Page
    {
        private AllAccommodationsViewModel viewModel;
        public AllAccommodationsPage()
        {
            InitializeComponent();
            viewModel=new AllAccommodationsViewModel();
            DataContext = viewModel;
        }

        private void MoreInformation_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var accommodation = button.DataContext as Accommodation;
            var viewModel = DataContext as AllAccommodationsViewModel;
            viewModel.NavigateToDetails(accommodation);
        }
    }
}
