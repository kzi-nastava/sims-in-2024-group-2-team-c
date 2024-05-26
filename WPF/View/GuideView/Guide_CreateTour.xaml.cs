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
    /// Interaction logic for Guide_CreateTour.xaml
    /// </summary>
    public partial class Guide_CreateTour : Page
    {
        private MainWindow_ViewModel mainView;
        private readonly CreateTour_ViewModel viewModel;
        public Guide_CreateTour()
        {
            InitializeComponent();
            viewModel = new CreateTour_ViewModel();
            DataContext = viewModel;
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }
        private void ViewToursButton_Click(object sender, RoutedEventArgs e)
        {
            //NavigationService?.GoBack();
        }
        private void AddKeyPoints_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as CreateTour_ViewModel;
            Guide_AddKeyPoints nextWindow = new Guide_AddKeyPoints();
            AddKeyPoints_ViewModel nextViewModel = new AddKeyPoints_ViewModel()
            {
                KeyPoints = viewModel.GetKeyPoints()
            };
            nextWindow.DataContext = nextViewModel;
            this.NavigationService?.Navigate(nextWindow);
        }
        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            var viewModel = DataContext as CreateTour_ViewModel;
            DateTime selectedDate = DatePicker.SelectedDate ?? DateTime.MinValue;
            List<DateTime> dates = viewModel.Dates;
            dates.Add(selectedDate);
            viewModel.Dates = dates;
            MessageBox.Show("Selected date: " + selectedDate.ToString("dd.MM.yyyy. HH:mm:ss"));
        }

    }
}
