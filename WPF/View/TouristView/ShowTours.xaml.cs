using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
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

namespace BookingApp.View
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {

        private TourRepository _tourRepository;
        private LocationRepository _locationRepository;
        private TourInstanceRepository _tourInstanceRepository;


        public Window1()
        {
            InitializeComponent();
            _tourRepository = new TourRepository();
            _locationRepository = new LocationRepository();
            _tourInstanceRepository = new TourInstanceRepository();

            tourListView.IsEnabled = true;
            UpdateTourListView(ShowAllTours());
        }

        private void UpdateTourListView(List<Tour> tours)
        {
            tourListView.ItemsSource = tours;
          
        }

        private void LoadLocations(List<Tour> tours) {

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


        private List<Tour> ShowAllTours()
        {
            List<Tour> tours =  _tourRepository.GetAll();
          
            LoadLocations(tours); 

            return tours;


        }

        private List<Tour> FilterTours(string location, string duration, string language, string numOfPeople)
        {
            List<Tour> tours = _tourRepository.GetAll();


            // Apply filters based on search criteria
            tours = FilterByLocation(location, tours);
            tours = FilterByDuration(duration, tours);
            tours = FilterByLanguage(language, tours);

            LoadLocations(tours);

            return tours;
        }

        private static List<Tour> FilterByLanguage(string language, List<Tour> tours)
        {
            if (!string.IsNullOrEmpty(language))
            {
                tours = tours.Where(t => t.Language.ToLower().Contains(language.ToLower())).ToList();
            }

            return tours;
        }

        private static List<Tour> FilterByDuration(string duration, List<Tour> tours)
        {
            if (!string.IsNullOrEmpty(duration))
            {
                int durationValue;
                if (int.TryParse(duration, out durationValue))
                {
                    tours = tours.Where(t => t.Duration == durationValue).ToList();
                }
            }

            return tours;
        }

        private List<Tour> FilterByLocation(string location, List<Tour> tours)
        {
            if (!string.IsNullOrEmpty(location))
            {
                int locationId = _locationRepository.GetIdByCityorCoutry(location);
                tours = _tourRepository.GetToursByLocationId(locationId);

            }

            return tours;
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            // Get search criteria from text boxes
            string location = LocationText.Text.Trim();
            string duration = DurationText.Text.Trim();
            string language = LanguageText.Text.Trim();
            string numOfPeople = NumOfPeopleText.Text.Trim();

            // Filter tours based on search criteria
            List<Tour> filteredTours = FilterTours(location, duration, language, numOfPeople);

            LocationText.Text = "";
            DurationText.Text = "";
            LanguageText.Text = "";
            NumOfPeopleText.Text = "";

            // Update tour list view with filtered tours
            UpdateTourListView(filteredTours);
        }

        private void TourListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            if (tourListView.SelectedItem != null)
            {
                Tour selectedTour = (Tour)tourListView.SelectedItem;
                List<TourInstance>  tourInstances = _tourInstanceRepository.GetTourInstancesByTourId(selectedTour.Id);
                
                instancesListView.ItemsSource = tourInstances;
                
            }
        }


        private bool isFull(TourInstance instance) {
            int remainingSpots = instance.MaxTourists - instance.ReservedTourists;
            if (remainingSpots == 0) {
                return false;
            }
            return true;

        }
        
        private void BookButton_Click(object sender, RoutedEventArgs e) {

            Button button = sender as Button;
            if (button != null)
            {
                // Get the corresponding tour instance object
                TourInstance instance = button.DataContext as TourInstance;
                Tour tour = _tourRepository.GetById(instance.IdTour);

                if (instance != null && isFull(instance))
                {
                    
                    // Open a new window to book the tour instance
                    ReservationView bookWindow = new ReservationView(instance, tour);
                    bookWindow.ShowDialog();
                }
                else {

                    List<Tour> tours = _tourRepository.GetToursByLocationId(tour.LocationId);
                    int indexToRemove = tours.FindIndex(tour => tour.Id == tour.Id);

                    // If the index is found (not -1), remove the tour from the list
                    if (indexToRemove != -1)
                    {
                        tours.RemoveAt(indexToRemove);
                    }

                    AlternativeToursView alternativeToursView = new AlternativeToursView(tours);
                    alternativeToursView.ShowDialog();
                
                }
            }

        }
            
        

       


    }
}
