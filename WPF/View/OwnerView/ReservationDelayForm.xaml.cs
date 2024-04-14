using BookingApp.Model;
using BookingApp.ViewModel;
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
    /// Interaction logic for ReservationDelayForm.xaml
    /// </summary>
    public partial class ReservationDelayForm : Page
    {
        private readonly ReservationDelayViewModel _viewModel;

        public ReservationDelayForm()
        {
            InitializeComponent();
            _viewModel = new ReservationDelayViewModel();
            DataContext = _viewModel;
        }

        

        private void ApproveButton_Click(object sender, RoutedEventArgs e)
        {
            
            _viewModel.ApproveReservationDelay();
        }

        private void RejectButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.RejectReservationDelay();
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }


        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ListView listView)
            {
                if (listView.SelectedItem != null)
                {
                    _viewModel.SelectedReservationDelay = (ReservationDelay)listView.SelectedItem;
                }
            }
        }


    }
}
