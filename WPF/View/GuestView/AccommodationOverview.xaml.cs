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
        private MainGuestWindow mainGuestWindow;



        public AccommodationOverview(MainGuestWindow mainWindow)
        {
            InitializeComponent();
            accommodationRepository = new AccommodationRepository();
            locationRepository = new LocationRepository();
            this.mainGuestWindow = mainWindow;

            UpdateAccommodationListView(PopulateAccommodationListView());
        }


        public void UpdateAccommodationListView(List<Accommodation> accommodations)
        {
            AccommodationListView.ItemsSource = accommodations;

        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string name = NameText.Text.Trim().ToLower();
            string location = LocationText.Text.Trim().ToLower();
            string type = TypeText.Text.Trim().ToLower();
            string numOfGuestsStr = NumberOfGuestsText.Text.Trim().ToLower();
            string bookingDaysStr = BookingDaysText.Text.Trim().ToLower();

            List<Accommodation> filteredAccommodations = FilterAccommodations(name, location, type, numOfGuestsStr, bookingDaysStr);

            ClearSearchFields();

            UpdateAccommodationListView(filteredAccommodations);
        }

        private void ClearSearchFields()
        {
            NameText.Text = "";
            LocationText.Text = "";
            TypeText.Text = "";
            NumberOfGuestsText.Text = "";
            BookingDaysText.Text = "";
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
                accommodations = accommodations.Where(a => a.Name.ToLower().StartsWith(name)).ToList();
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
            List<Accommodation> accommodations = accommodationRepository.GetAll();

            LoadLocations(accommodations);

            return accommodations;
        }

        private void LoadLocations(List<Accommodation> accommodations)
        {
                foreach (var accommodation in accommodations)
                {
                    Location location = locationRepository.GetById(accommodation.Location.Id);

                    if (location != null)
                    {
                        string LocationDetails = $"{location.City}, {location.Country}";
                        accommodation.LocationDetails = LocationDetails;
                    }
                    else
                    {
                        accommodation.LocationDetails = "Location not found";
                    }
                }
        }

        private void AccommodationListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Accommodation selectedAccommodation = (Accommodation)AccommodationListView.SelectedItem;
            if (selectedAccommodation != null)
            {
                if (mainGuestWindow != null)
                {
                    mainGuestWindow.ChangeHeaderText("Here are all accommodation details");
                    NavigationService?.Navigate(new AccommodationDetails(selectedAccommodation));
                }
            }
        }

        private void ReservationDetailsButton_Click(object sender, RoutedEventArgs e)
        {
            if (mainGuestWindow != null)
            {
                mainGuestWindow.ChangeHeaderText("Reservation details");
                NavigationService?.Navigate(new GuestReservationDetails(mainGuestWindow, NavigationService));
            }
        }

        private void AnywhereAnytimeButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            //NavigationService?.Navigate(new AnywhereAnytimePage());
        }

        private void ForumsButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            mainGuestWindow.ChangeHeaderText("Forums");
            NavigationService?.Navigate(new ForumView(mainGuestWindow, NavigationService));
        }


    }
}



