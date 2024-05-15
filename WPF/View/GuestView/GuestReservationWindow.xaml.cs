using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Service.AccommodationServices;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
namespace BookingApp.WPF.View.GuestView
{
    /// <summary>
    /// Interaction logic for GuestReservationWindow.xaml
    /// </summary>
    public partial class GuestReservationWindow : Window
    {
        private GuestReservationService guestReservationService;
        private Accommodation selectedAccommodation;
        private List<GuestReservation> availableDates;

        private DateTime startDate;
        private DateTime endDate;
        private int stayDuration;

        public GuestReservationWindow()
        {
            InitializeComponent();
        }

        public GuestReservationWindow(Accommodation selectedAccommodationSent, DateTime startDate, DateTime endDate, int stayDuration)
        {
            InitializeComponent();
            this.selectedAccommodation = selectedAccommodationSent;
            this.startDate = startDate;
            this.endDate = endDate;
            this.stayDuration = stayDuration;
            guestReservationService = new GuestReservationService();
            availableDates = new List<GuestReservation>();
            LoadAvailableDates(selectedAccommodationSent, startDate, endDate, stayDuration);
        }


        private void LoadAvailableDates(Accommodation selectedAccommodation, DateTime startDate, DateTime endDate, int stayDuration)
        {

            List<AvailableDateDisplay> availableDates = guestReservationService.FindAvailableReservations(selectedAccommodation, startDate, endDate, stayDuration);

            if (availableDates.Count > 0)
            {
                MessageBox.Show("Here are all available dates in this time period:");
                AvailableDatesListBox.ItemsSource = availableDates;
                AvailableDatesListBox.Visibility = Visibility.Visible;
            }
            else
            {
                MessageBox.Show("Accommodation is not available for the selected dates.");
                List<AvailableDateDisplay> alternativeDates = guestReservationService.FindAvailableReservations(selectedAccommodation, startDate.AddDays(-10), endDate.AddDays(10), stayDuration);
                ShowAvailableDates(alternativeDates);
            }

        }

        private void ShowAvailableDates(List<AvailableDateDisplay> dates)
        {
            if (dates.Count > 0)
            {
                MessageBox.Show("Here are some alternative dates you can consider:");
                AlternativniDatumiListBox.ItemsSource = dates;
                AlternativniDatumiListBox.Visibility = Visibility.Visible;
            }
            else
            {
                MessageBox.Show("There are no alternative dates at the moment.");
                AlternativniDatumiListBox.ItemsSource = null;
                AlternativniDatumiListBox.Visibility = Visibility.Collapsed;
            }
        }


        private void BookButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button == null)
            {
                return;
            }

            FrameworkElement container = button.TemplatedParent as FrameworkElement;
            if (container == null)
            {
                return;
            }

            AvailableDateDisplay selectedDate = container.DataContext as AvailableDateDisplay;
            if (selectedDate == null)
            {
                return;
            }

            DateTime checkInDate = selectedDate.CheckIn;
            DateTime checkOutDate = selectedDate.CheckOut;

            if (!int.TryParse(NumGuestsTextBox.Text, out int numOfGuests) || numOfGuests <= 0)
            {
                MessageBox.Show("Please enter a valid value for the number of guests.");
                return;
            }

            if (numOfGuests > selectedAccommodation.MaxGuests)
            {
                MessageBox.Show($"Maximum number of guests allowed is {selectedAccommodation.MaxGuests}.");
                return;
            }

            string resultMessage = guestReservationService.ReserveAccommodation(
                selectedAccommodation.Id,
                LoggedInUser.Id,
                startDate,
                endDate,
                stayDuration,
                checkInDate,
                checkOutDate,
                numOfGuests
            );

            MessageBox.Show(resultMessage);
            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to cancel the reservation?", "Cancel Reservation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                MessageBox.Show("Reservation canceled!");
                this.Close();
            }

        }


        private void BookAlternativeDate_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button == null)
            {
                return;
            }

            FrameworkElement container = button.TemplatedParent as FrameworkElement;
            if (container == null)
            {
                return;
            }

            AvailableDateDisplay selectedDate = container.DataContext as AvailableDateDisplay;
            if (selectedDate == null)
            {
                return;
            }

            DateTime checkInDate = selectedDate.CheckIn;
            DateTime checkOutDate = selectedDate.CheckOut;

            if (!int.TryParse(NumGuestsTextBox.Text, out int numOfGuests) || numOfGuests <= 0)
            {
                MessageBox.Show("Molimo unesite validan broj gostiju.");
                return;
            }

            if (numOfGuests > selectedAccommodation.MaxGuests)
            {
                MessageBox.Show($"Maksimalan broj gostiju je {selectedAccommodation.MaxGuests}.");
                return;
            }

            string resultMessage = guestReservationService.ReserveAccommodation(
                selectedAccommodation.Id,
                LoggedInUser.Id,
                startDate,
                endDate,
                stayDuration,
                checkInDate,
                checkOutDate,
                numOfGuests
            );

            MessageBox.Show(resultMessage);
            this.Close();
        }


        private void CancelAlternativeDate_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to cancel the reservation?", "Cancel Reservation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                MessageBox.Show("Reservation canceled!");
                this.Close();
            }

        }

    }

}