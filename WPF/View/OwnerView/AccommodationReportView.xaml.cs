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
    /// Interaction logic for AccommodationReportView.xaml
    /// </summary>
    public partial class AccommodationReportView : UserControl
    {
        private AccommodationReportViewModel viewModel;
        public AccommodationReportView()
        {
            InitializeComponent();
            viewModel = new AccommodationReportViewModel();
            DataContext = viewModel;
        }

        private void GenerateReport_Click(object sender, RoutedEventArgs e)
        {
            
            viewModel.GenerateReport();
        }
    }
}
