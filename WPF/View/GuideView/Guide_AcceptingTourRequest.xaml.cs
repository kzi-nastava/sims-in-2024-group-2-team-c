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
    /// Interaction logic for Guide_AcceptingTourRequest.xaml
    /// </summary>
    public partial class Guide_AcceptingTourRequest : Window
    {
        public Guide_AcceptingTourRequest()
        {
            InitializeComponent();
            DataContext = new AcceptingTourRequest_ViewModel();
        }
        private void Send_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as AcceptingTourRequest_ViewModel;
            viewModel.SendNotificationCommand.Execute(null);

        }
    }
}
