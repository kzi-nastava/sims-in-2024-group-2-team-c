using BookingApp.WPF.ViewModel.GuideViewModel;
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

namespace BookingApp.WPF.View.GuideView
{
    /// <summary>
    /// Interaction logic for Guide_CreateTourByRequest.xaml
    /// </summary>
    public partial class Guide_CreateTourByRequest : Page
    {
       // private readonly CreateTourByRequest_ViewModel viewModel;
       public Guide_CreateTourByRequest()
        {
            InitializeComponent();
            DataContext = new CreateTourByRequest_ViewModel();

        }
        private void AddKeyPoints_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as CreateTourByRequest_ViewModel;
            Guide_AddKeyPoints nextWindow = new Guide_AddKeyPoints();
            AddKeyPoints_ViewModel nextViewModel = new AddKeyPoints_ViewModel()
            {
                KeyPoints = viewModel.GetKeyPoints()
            };
            nextWindow.DataContext = nextViewModel;
            this.NavigationService?.Navigate(nextWindow);
            //this.NavigationService?.GoBack();
        }
        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            var viewModel = DataContext as CreateTourByRequest_ViewModel;
            DateTime selectedDate = DatePicker.SelectedDate ?? DateTime.MinValue;
            List<DateTime> dates = viewModel.Dates;
            dates.Add(selectedDate);
            viewModel.Dates = dates;
            MessageBox.Show("Selected date: " + selectedDate.ToString("dd.MM.yyyy. HH:mm:ss"));
        }
        /*private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            var viewModel = DataContext as CreateTourByRequest_ViewModel;
            DateTime selectedDate = DatePicker.SelectedDate ?? DateTime.MinValue;
            List<DateTime> dates = viewModel.Dates;
            dates.Add(selectedDate);
            viewModel.Dates = dates;
            MessageBox.Show("Selected date: " + selectedDate.ToString("dd.MM.yyyy. HH:mm:ss"));
        }

        private void AddDateButton_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as CreateTourByRequest_ViewModel;
           // viewModel.Dates.Add(DatePicker.SelectedDate ?? DateTime.MinValue);
            DatePicker.SelectedDate = null;
            viewModel.OnPropertyChanged(nameof(viewModel.Dates));

            //logiku za ažuriranje prikaza ili obradu dodatih datuma
        }*/

        /* private void SaveTour_Click(object sender, RoutedEventArgs e)
         {
             var viewModel = DataContext as CreateTourByRequest_ViewModel;
             viewModel.SaveTourCommand.Execute(null);
         }*/
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService?.GoBack();
        }
        private void ViewToursButton_Click(object sender, RoutedEventArgs e)
        {
            //this.NavigationService?.GoBack();
        }
    }
}
