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
using System.Windows.Shapes;
using BookingApp.Model;
using BookingApp.Repository;

namespace BookingApp.View
{
    /// <summary>
    /// Interaction logic for AlternativeToursView.xaml
    /// </summary>
    public partial class AlternativeToursView : Window
    {

        private TourInstanceRepository _tourInstanceRepository;
        private LocationRepository _locationRepository;
        private TourRepository _tourRepository;
        public AlternativeToursView(List<Tour> tours)
        {

            InitializeComponent();
            _tourInstanceRepository = new TourInstanceRepository();
            _locationRepository = new LocationRepository();
            _tourRepository = new TourRepository();
            LoadLocations(tours);
            AlternativesListView.ItemsSource = tours;
        }

        private void LoadLocations(List<Tour> tours)
        {

            foreach (Tour tour in tours)
            {
                Location locationObject = _locationRepository.Get(tour.LocationId);
                if (locationObject != null)
                {
                    tour.ViewLocation = $"{locationObject.City}, {locationObject.Country}";
                }
                else
                {
                    tour.ViewLocation = "Location not found";
                }
            }


        }

        private void Alternatives_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {


            if (AlternativesListView.SelectedItem != null)
            {
                Tour selectedTour = (Tour)AlternativesListView.SelectedItem;
                List<TourInstance> tourInstances = _tourInstanceRepository.GetTourInstancesByTourId(selectedTour.Id);
                AlternativeinstancesView.ItemsSource = tourInstances;
            }
        }

    

        private void BookButton_Click(object sender, RoutedEventArgs e)
        {

            Button button = sender as Button;
            if (button != null)
            {
                // Get the corresponding tour instance object
                TourInstance instance = button.DataContext as TourInstance;
                Tour tour = _tourRepository.GetById(instance.IdTour);

                if (instance != null )
                {
                    
                    // Open a new window to book the tour instance
                    ReservationView bookWindow = new ReservationView(instance, tour);
                    bookWindow.ShowDialog();
                }
                
            }

        }

        public void CloseButton_Click(object sender, RoutedEventArgs e) { 
            Close(); 
        }

        private void FilterInstancesButton_Click(object sender, RoutedEventArgs e)
        {
            if (AlternativesListView.SelectedItem != null && !string.IsNullOrEmpty(NumberOfPeopleTextBox.Text))
            {
                Tour selectedTour = (Tour)AlternativesListView.SelectedItem;
                int numberOfPeople = FilterByAvailableSpots(selectedTour);
            }
            else
            {
                // Handle no selection or empty input
                MessageBox.Show("Please select a tour and enter the number of people.", "Selection Required", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private int FilterByAvailableSpots(Tour selectedTour)
        {
            int numberOfPeople;
            if (int.TryParse(NumberOfPeopleTextBox.Text, out numberOfPeople))
            {
                List<TourInstance> tourInstances = _tourInstanceRepository.GetInstancesByTourIdAndAvailableSlots(selectedTour.Id, numberOfPeople);
                AlternativeinstancesView.ItemsSource = tourInstances;
            }
            else
            {
                // Handle invalid input
                MessageBox.Show("Please enter a valid number of people.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            return numberOfPeople;
        }
    }
}
