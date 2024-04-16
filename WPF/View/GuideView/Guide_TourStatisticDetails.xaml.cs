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
    /// Interaction logic for Guide_TourStatisticDetails.xaml
    /// </summary>
    public partial class Guide_TourStatisticDetails : Page
    {
        private readonly GuideTourStatisticDetails_ViewModel viewModel;

        public Guide_TourStatisticDetails()
        {
            InitializeComponent();
            viewModel = new GuideTourStatisticDetails_ViewModel();
            DataContext = viewModel;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        private void HomePageButton_Click(object sender, RoutedEventArgs e)
        {
            GuideHomePage_ViewModel homePage = new GuideHomePage_ViewModel();
            this.NavigationService.Navigate(homePage);
        }
    }
}
