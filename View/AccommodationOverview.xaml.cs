using BookingApp.Model;
using BookingApp.Repository;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
//using BookingApp.View.GuestReservationWindow;

namespace BookingApp.View
{
    /// <summary>
    /// Interaction logic for AccommodationOverview.xaml
    /// </summary>
    public partial class AccommodationOverview : Window
    {

        private AccommodationRepository accommodationRepository;
        private LocationRepository locationRepository;


        private List<Accommodation> GetAllAccommodations()
        {
            return accommodationRepository.GetAll();
        }


        public AccommodationOverview()
        {
            InitializeComponent();
            accommodationRepository = new AccommodationRepository();
            locationRepository = new LocationRepository();

            UpdateAccommodationListView(PopulateAccommodationListView());
        }


        public void UpdateAccommodationListView(List<Accommodation> accommodations)
        {
            AccommodationListView.ItemsSource = accommodations;

        }
        

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            // Get search criteria from text boxes
            string name = NameText.Text.Trim().ToLower();
            string location = LocationText.Text.Trim().ToLower();
            string type = TypeText.Text.Trim().ToLower();
            string numOfGuestsStr = NumberOfGuestsText.Text.Trim().ToLower();
            string bookingDaysStr = BookingDaysText.Text.Trim().ToLower();

            // Filter tours based on search criteria
            List<Accommodation> filteredAccommodations = FilterAccommodations(name, location, type, numOfGuestsStr, bookingDaysStr);

            NameText.Text = "";
            LocationText.Text = "";
            TypeText.Text = "";
            NumberOfGuestsText.Text = "";
            BookingDaysText.Text = "";

            // Update accommodation list view with filtered accommodations
            UpdateAccommodationListView(filteredAccommodations);
        }

        private List<Accommodation> FilterAccommodations(string name, string location, string type, string numOfGuestsStr, string bookingDaysStr)
        {
            List<Accommodation> accommodations = accommodationRepository.GetAll();

            if (!string.IsNullOrEmpty(location))
            {

                int locationId = locationRepository.GetIdByCityorCountry(location);
                accommodations = accommodationRepository.GetToursByLocationId(locationId);

            }

            if (!string.IsNullOrEmpty(name))
            {

                accommodations = accommodationRepository.GetAccommodationsByName(name);

            }

            if (!string.IsNullOrEmpty(type))
            {

                accommodations = accommodationRepository.GetAccommodationsByType(type);

            }


            if (!string.IsNullOrEmpty(numOfGuestsStr))
            {

                accommodations = accommodationRepository.GetAccommodationsByNumOfGuests(numOfGuestsStr);

            }

            if (!string.IsNullOrEmpty(bookingDaysStr))
            {

                accommodations = accommodationRepository.GetAccommodationsByBookingDays(bookingDaysStr);

            }


            LoadLocations(accommodations);

            return accommodations;
        }



        private List<Accommodation> PopulateAccommodationListView()
        {
           
            // Dobavljanje svih smještaja
            List<Accommodation> accommodations = accommodationRepository.GetAll();

            LoadLocations(accommodations);

            // Postavljanje podataka smještaja u AccommodationListView
            return accommodations;
        }

        private void LoadLocations(List<Accommodation> accommodations)
        {
                foreach (var accommodation in accommodations)
                {
                    // Dobavljanje detalja lokacije koristeći ID lokacije
                    Location location = locationRepository.GetById(accommodation.Location.Id);

                    if (location != null)
                    {
                        // Postavljanje svojstva LocationDetails svakog smještaja za prikaz u AccommodationListView-u
                        string LocationDetails = $"{location.City}, {location.Country}";
                        accommodation.LocationDetails = LocationDetails;
                    }
                    else
                    {
                        accommodation.LocationDetails = "Location not found";
                    }
                }
        }

        /*
        private void AccommodationListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Accommodation selectedAccommodation = (Accommodation)AccommodationListView.SelectedItem;
            if (selectedAccommodation != null)
            {
                // Prikaz informacija o selektovanom smeštaju
                IdTextBlock.Text = selectedAccommodation.Id.ToString();
                NameTextBlock.Text = selectedAccommodation.Name;
                LocationTextBlock.Text = selectedAccommodation.LocationDetails;
                TypeTextBlock.Text = selectedAccommodation.Type;
                MaxGuestsTextBlock.Text = selectedAccommodation.MaxGuests.ToString();
                MinBookingDaysTextBlock.Text = selectedAccommodation.MinBookingDays.ToString();
                MinCancellationDaysTextBlock.Text = selectedAccommodation.CancellationDays.ToString();
            }
        }
        */


        private void AccommodationListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Accommodation selectedAccommodation = (Accommodation)AccommodationListView.SelectedItem;
            if (selectedAccommodation != null)
            {
                AccommodationDetailsWindow accommodationDetailsWindow = new AccommodationDetailsWindow(selectedAccommodation);
                accommodationDetailsWindow.Show();
            }
        }


    }
}



