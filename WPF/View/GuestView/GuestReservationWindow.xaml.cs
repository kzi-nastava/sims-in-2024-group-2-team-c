using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
namespace BookingApp.View
{
    /// <summary>
    /// Interaction logic for GuestReservationWindow.xaml
    /// </summary>
    public partial class GuestReservationWindow : Window
    {

        private GuestReservationRepository guestReservationRepository;
        private Accommodation selectedAccommodation;
        private List<GuestReservation> availableDates;


        // Privremena polja
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
            guestReservationRepository = new GuestReservationRepository();
            availableDates = new List<GuestReservation>();
            LoadAvailableDates(selectedAccommodationSent, startDate, endDate, stayDuration);
        }


        private void LoadAvailableDates(Accommodation selectedAccommodation, DateTime startDate, DateTime endDate, int stayDuration)
        {

            List<AvailableDateDisplay> availableDates = guestReservationRepository.FindAvailableReservations(selectedAccommodation, startDate, endDate, stayDuration);

            if (availableDates.Count > 0)
            {
                MessageBox.Show("Here are all available dates in this time period:");
                // Prikazivanje dostupnih datuma u ListBox-u
                AvailableDatesListBox.ItemsSource = availableDates;
                AvailableDatesListBox.Visibility = Visibility.Visible;
            }
            else
            {
                MessageBox.Show("Accommodation is not available for the selected dates.");

                // Pronalaženje alternativnih datuma
                List<AvailableDateDisplay> alternativeDates = guestReservationRepository.FindAvailableReservations(selectedAccommodation, startDate.AddDays(-10), endDate.AddDays(10), stayDuration);

                // Prikazivanje alternativnih datuma ako postoje
                ShowAvailableDates(alternativeDates);
            }

        }

        private void ShowAvailableDates(List<AvailableDateDisplay> dates)
        {
            // Provera da li ima dostupnih datuma
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

            // Dobijamo dugme koje je pokrenulo događaj
            Button button = sender as Button;
            if (button == null)
            {
                return;
            }

            // Dobijamo odgovarajući red koji sadrži kliknuto dugme
            FrameworkElement container = button.TemplatedParent as FrameworkElement;
            if (container == null)
            {
                return;
            }

            // Dobijamo DataContext iz reda, što je objekat AvailableDateDisplay
            AvailableDateDisplay selectedDate = container.DataContext as AvailableDateDisplay;
            if (selectedDate == null)
            {
                return;
            }

            // Dobavljanje CheckIn i CheckOut datuma
            DateTime checkInDate = selectedDate.CheckIn;
            DateTime checkOutDate = selectedDate.CheckOut;

            // Dobavljanje broja gostiju
            if (!int.TryParse(NumGuestsTextBox.Text, out int numOfGuests) || numOfGuests <= 0)
            {
                MessageBox.Show("Please enter a valid value for the number of guests.");
                return;
            }

            // Provera protiv maksimalnog broja gostiju
            if (numOfGuests > selectedAccommodation.MaxGuests)
            {
                MessageBox.Show($"Maximum number of guests allowed is {selectedAccommodation.MaxGuests}.");
                return;
            }

            // Rezervisanje odabranog datuma
            string resultMessage = guestReservationRepository.ReserveAccommodation(
                selectedAccommodation.Id,
                LoggedInUser.Id,
                startDate,
                endDate,
                stayDuration,
                checkInDate,
                checkOutDate,
                numOfGuests
            );

            // Prikaz rezultata korisniku
            MessageBox.Show(resultMessage);

            // Zatvaranje prozora nakon rezervacije
            this.Close();

        }



        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {

            // Display a confirmation dialog for canceling the reservation
            var result = MessageBox.Show("Are you sure you want to cancel the reservation?", "Cancel Reservation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                // Implementation of logic for canceling the reservation
                MessageBox.Show("Reservation canceled!");
                this.Close(); // Close the window after canceling
            }

        }
        

        private void BookAlternativeDate_Click(object sender, RoutedEventArgs e)
        {

            // Dobijamo dugme koje je pokrenulo događaj
            Button button = sender as Button;
            if (button == null)
            {
                return;
            }

            // Dobijamo odgovarajući red koji sadrži kliknuto dugme
            FrameworkElement container = button.TemplatedParent as FrameworkElement;
            if (container == null)
            {
                return;
            }

            // Dobijamo DataContext iz reda, što je objekat AvailableDateDisplay
            AvailableDateDisplay selectedDate = container.DataContext as AvailableDateDisplay;
            if (selectedDate == null)
            {
                return;
            }


            // Dobavljanje CheckIn i CheckOut datuma
            DateTime checkInDate = selectedDate.CheckIn;
            DateTime checkOutDate = selectedDate.CheckOut;

            // Dobavljanje broja gostiju
            if (!int.TryParse(NumGuestsTextBox.Text, out int numOfGuests) || numOfGuests <= 0)
            {
                MessageBox.Show("Molimo unesite validan broj gostiju.");
                return;
            }

            // Provera da li broj gostiju premašuje maksimalni broj gostiju za smeštaj
            if (numOfGuests > selectedAccommodation.MaxGuests)
            {
                MessageBox.Show($"Maksimalan broj gostiju je {selectedAccommodation.MaxGuests}.");
                return;
            }

            // Pozivanje funkcije za rezervaciju
            string resultMessage = guestReservationRepository.ReserveAccommodation(
                selectedAccommodation.Id,
                LoggedInUser.Id,
                startDate,
                endDate,
                stayDuration,
                checkInDate,
                checkOutDate,
                numOfGuests
            );

            // Prikazivanje rezultata korisniku
            MessageBox.Show(resultMessage);

            // Zatvaranje prozora nakon rezervacije
            this.Close();
        }


        private void CancelAlternativeDate_Click(object sender, RoutedEventArgs e)
        {

            // Display a confirmation dialog for canceling the reservation
            var result = MessageBox.Show("Are you sure you want to cancel the reservation?", "Cancel Reservation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                // Implementation of logic for canceling the reservation
                MessageBox.Show("Reservation canceled!");
                this.Close(); // Close the window after canceling
            }

        }



    }
      

}

