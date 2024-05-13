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
    /// Interaction logic for Guide_TourRequestStatistic.xaml
    /// </summary>
    public partial class Guide_TourRequestStatistic : Page
    {
        public Guide_TourRequestStatistic()
        {
            InitializeComponent();
            TourRequestStatistic_ViewModel viewModel = new TourRequestStatistic_ViewModel();
            DataContext = viewModel;
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService?.GoBack();
        }
        private void Search_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as TourRequestStatistic_ViewModel;
            viewModel.SearchCommand.Execute(null);
        }
        private void CreateTourLocation_Click(object sender, RoutedEventArgs e)
        {
            //this.NavigationService?.Navigate();
        }
        private void CreateTourLanguage_Click(object sender, RoutedEventArgs e)
        {
            //this.NavigationService?.Navigate();
        }
    }
}
