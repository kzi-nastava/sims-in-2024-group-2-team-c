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
    /// Interaction logic for NotificationView.xaml
    /// </summary>
    public partial class NotificationView : Page
    {
        public NotificationView()
        {
            InitializeComponent();
            DataContext = new NotificationViewModel();
        }

        

        private void Reccommendations_Click(object sender, RoutedEventArgs e)
        {
            Content1.Visibility = Visibility.Visible;
            Content2.Visibility = Visibility.Collapsed;
            Content3.Visibility = Visibility.Collapsed;
        }

        private void AcceptedRequests_Click(object sender, RoutedEventArgs e)
        {
            Content1.Visibility = Visibility.Collapsed;
            Content2.Visibility = Visibility.Visible;
            Content3.Visibility = Visibility.Collapsed;
        }

        private void TouristAdded_Click(object sender, RoutedEventArgs e)
        {
            Content1.Visibility = Visibility.Collapsed;
            Content2.Visibility = Visibility.Collapsed;
            Content3.Visibility = Visibility.Visible;
        }

    }
}
