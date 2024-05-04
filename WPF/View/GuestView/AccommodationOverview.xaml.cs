using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.WPF.View.GuestView;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
//using BookingApp.View.GuestReservationWindow;

namespace BookingApp.WPF.View.GuestView
{
    /// <summary>
    /// Interaction logic for AccommodationOverview.xaml
    /// </summary>
    public partial class AccommodationOverview : Page
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
                accommodations = accommodations.Where(a => a.Location.Id == locationId).ToList();
            }

            if (!string.IsNullOrEmpty(name))
            {
                accommodations = accommodations.Where(a => a.Name.ToLower().Contains(name)).ToList();
            }

            if (!string.IsNullOrEmpty(type))
            {
                accommodations = accommodations.Where(a => a.Type.ToLower() == type).ToList();
            }

            if (!string.IsNullOrEmpty(numOfGuestsStr) && int.TryParse(numOfGuestsStr, out int numOfGuests))
            {
                accommodations = accommodations.Where(a => a.MaxGuests >= numOfGuests).ToList();
            }

            if (!string.IsNullOrEmpty(bookingDaysStr) && int.TryParse(bookingDaysStr, out int bookingDays))
            {
                accommodations = accommodations.Where(a => a.MinBookingDays <= bookingDays).ToList();
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
                //AccommodationDetailsWindow accommodationDetailsWindow = new AccommodationDetailsWindow(selectedAccommodation);
                //accommodationDetailsWindow.Show();
                NavigationService?.Navigate(new AccommodationDetails(selectedAccommodation));
            }
        }

        private void ReservationDetailsButton_Click(object sender, RoutedEventArgs e)
        {
            // Otvaranje prozora za detalje rezervacije
            GuestReservationDetails guestReservationDetails = new GuestReservationDetails();
            guestReservationDetails.Show();
        }

        private void AnywhereAnytimeButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            // Navigate to Anywhere Anytime page
            //NavigationService?.Navigate(new AnywhereAnytimePage());
        }

        private void ForumsButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            // Navigate to Forums page
            //NavigationService?.Navigate(new ForumsPage());
        }


    }
}



