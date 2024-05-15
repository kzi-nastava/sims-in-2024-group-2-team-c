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
    /// Interaction logic for RenovationsList.xaml
    /// </summary>
    public partial class RenovationsList : Page
    {
        private RenovationListViewModel viewModel;
        public RenovationsList()
        {
            InitializeComponent();
            viewModel = new RenovationListViewModel();
            DataContext = viewModel;
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is DataGrid dataGrid)
            {
                if (dataGrid.SelectedItem != null)
                {
                    viewModel.SelectedRenovation = (RenovationAvailableDate)dataGrid.SelectedItem;
                }
            }
        }


        public void CancelRenovationButton_Click(object sender, RoutedEventArgs e)
        {
            viewModel.CancelRenovation();
           
        }
    }
}
