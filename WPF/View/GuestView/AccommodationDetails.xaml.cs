using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Windows;
using System.Windows.Controls;
namespace BookingApp.WPF.View.GuestView
{
    /// <summary>
    /// Interaction logic for AccommodationDetailsWindow.xaml
    /// </summary>
    public partial class AccommodationDetails : Page
    {
        private Accommodation selectedAccommodation;

        public AccommodationDetails()
        {
            InitializeComponent();
        }

        public AccommodationDetails(Accommodation selectedAccommodationSent)
        {
            InitializeComponent();
            selectedAccommodation = selectedAccommodationSent;
        }

        private void CheckButton_Click(object sender, RoutedEventArgs e)
        {
            if (StartDatePicker.SelectedDate == null || EndDatePicker.SelectedDate == null ||
                string.IsNullOrWhiteSpace(StayDurationTextBox.Text))
            {
                MessageBox.Show("Please enter all required information for the reservation.");
                return;
            }

            DateTime startDate = StartDatePicker.SelectedDate.Value;
            DateTime endDate = EndDatePicker.SelectedDate.Value;

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

            if (stayDuration < selectedAccommodation.MinBookingDays)
            {
                MessageBox.Show($"Minimum booking duration is {selectedAccommodation.MinBookingDays} days.");
                return;
            }

            GuestReservationWindow guestReservationWindow = new GuestReservationWindow(selectedAccommodation, startDate, endDate, stayDuration);
            guestReservationWindow.Show();

        }

        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();

        }



    }
}
