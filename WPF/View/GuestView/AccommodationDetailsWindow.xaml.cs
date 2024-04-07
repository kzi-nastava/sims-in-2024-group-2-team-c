using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Windows;
namespace BookingApp.View
{
    /// <summary>
    /// Interaction logic for AccommodationDetailsWindow.xaml
    /// </summary>
    public partial class AccommodationDetailsWindow : Window
    {
        private GuestReservationRepository guestReservationRepository;
        private Accommodation selectedAccommodation;

        public AccommodationDetailsWindow()
        {
            InitializeComponent();
        }

        public AccommodationDetailsWindow(Accommodation selectedAccommodationSent)
        {
            InitializeComponent();
            selectedAccommodation = selectedAccommodationSent;
        }

        private void CheckButton_Click(object sender, RoutedEventArgs e)
        {
            // Check if all required data is entered
            if (StartDatePicker.SelectedDate == null || EndDatePicker.SelectedDate == null ||
                string.IsNullOrWhiteSpace(StayDurationTextBox.Text))
            {
                MessageBox.Show("Please enter all required information for the reservation.");
                return;
            }

            // Gather reservation parameters
            DateTime startDate = StartDatePicker.SelectedDate.Value;
            DateTime endDate = EndDatePicker.SelectedDate.Value;

            // Check if the selected dates are in the future
            if (startDate < DateTime.Today || endDate < DateTime.Today)
            {
                MessageBox.Show("Please choose future dates for the reservation.");
                return;
            }

            if (!int.TryParse(StayDurationTextBox.Text, out int stayDuration) || stayDuration <= 0)
            {
                MessageBox.Show("Please enter a valid value for the duration of stay.");
                return;
            }

            // Check against the minimum booking duration set by the owner
            if (stayDuration < selectedAccommodation.MinBookingDays)
            {
                MessageBox.Show($"Minimum booking duration is {selectedAccommodation.MinBookingDays} days.");
                return;
            }

            GuestReservationWindow guestReservationWindow = new GuestReservationWindow(selectedAccommodation, startDate, endDate, stayDuration);
            guestReservationWindow.ShowDialog(); // Otvara prozor i čeka da se zatvori pre nego što se vrati na ovaj prozor

        }

        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            
             this.Close(); // Go back to the first window
            
        }



    }
}
