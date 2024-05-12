using BookingApp.DTO;
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
    /// Interaction logic for Guide_TourStatistic.xaml
    /// </summary>
    public partial class Guide_TourStatistic : Page
    {
        public Guide_TourStatistic()
        {
            InitializeComponent();
            var viewModel = new TourStatistic_ViewModel();
            viewModel.SelectedTour = (TourStatisticDTO)tourView.SelectedItem;
            DataContext = viewModel;
        }
        private void Show_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = new TourStatistic_ViewModel();
            viewModel.SelectedTour = (TourStatisticDTO)tourView.SelectedItem;
            var SelectedTour = viewModel.SelectedTour;
            if (SelectedTour != null)
            {
                MaxTouristsBlock.DataContext = SelectedTour;
                ReservedTouristsBlock.DataContext = SelectedTour;
                PresentTouristsBlock.DataContext = SelectedTour;

                LessBlock.DataContext = SelectedTour;
                BetweenBlock.DataContext = SelectedTour;
                MoreBlock.DataContext = SelectedTour;

                /*MessageBox.Show($"Max Tourists: {SelectedTour.MaxTourists}\n" +
                                $"Reserved Tourists: {SelectedTour.ReservedTourists}\n" +
                                $"Present Tourists: {SelectedTour.PresentTourists}",
                                "Tour Statistics", MessageBoxButton.OK, MessageBoxImage.Information);*/
            }
            else
            {
                MessageBox.Show("Please select a tour.", "Tour Statistics", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService?.GoBack();
        }
        private void TourView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var viewModel = new TourStatistic_ViewModel();
            viewModel.SelectedTour = (TourStatisticDTO)tourView.SelectedItem;
        }
        private void GenerateReport_Click(object sender, RoutedEventArgs e)
        {
            //
        }

        /*private void TourView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (tourView.SelectedItem != null)
            {
                viewModel.SelectedTour = tourView.SelectedItem;
            }
        }*/
    }
}
