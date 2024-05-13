using BookingApp.DTO;
using BookingApp.Model;
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
    /// Interaction logic for Guide_TourRequests.xaml
    /// </summary>
    public partial class Guide_TourRequests : Page
    {
        private readonly TourRequests_ViewModel viewModel;
        private MainWindow_ViewModel mainView;
        public Guide_TourRequests()
        {
            InitializeComponent();
            viewModel = new TourRequests_ViewModel();
            DataContext = viewModel;
        }
        private void AcceptButton_Click(object sender, RoutedEventArgs e)
        {
            viewModel.AcceptTourRequestCommand.Execute(requestsView.SelectedItem);

        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }
        private void TourRequestView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (requestsView.SelectedItem != null)
            {
                viewModel.SelectedTourRequest = (TourRequestDTO)requestsView.SelectedItem;
            }
        }
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            //viewModel.SearchTourRequestCommand;
        }
        

    }
}
