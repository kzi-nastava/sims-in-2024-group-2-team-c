using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
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
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;


namespace BookingApp.View
{
    /// <summary>
    /// Interaction logic for GuestRatingForm.xaml
    /// </summary>
    public partial class GuestRatingForm : Window
    {

        private ReservationRepository reservationRepository; 
        private GuestRatingRepository guestRatingRepository;
        private GuestRepository guestRepository; 
        private AccommodationRepository accommodationRepository;
        private Owner owner;
        private Reservation selectedReservation;


        public GuestRatingForm()
        {
            InitializeComponent();
            reservationRepository = new ReservationRepository();
            reservationRepository.LoadReservationsFromCSV("../../../Resources/Data/reservations.csv");
            guestListView.ItemsSource = reservationRepository.Reservations;
            guestRatingRepository = new GuestRatingRepository();
            guestRepository = new GuestRepository();
            accommodationRepository = new AccommodationRepository();
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (guestListView.SelectedItem != null)
            {
                selectedReservation = (Reservation)guestListView.SelectedItem;
            }
        }



        private void SaveGuestRating(object sender, RoutedEventArgs e)
        {
            if (selectedReservation == null)
            {
                MessageBox.Show("Please select a reservation.");
                return;
            }

            if (!ValidateCleanlinessAndRuleRespecting())
                return;

            Guest guest = guestRepository.GetGuestById(selectedReservation.Guest.Id);
            int cleanliness = int.Parse(txtCleanliness.Text);
            if (cleanliness < 1 || cleanliness > 5)
            {
                MessageBox.Show("Please, enter a number between 1 and 5 for Cleanliness");
                return;
            }
            int ruleRespecting = int.Parse(txtRuleRespecting.Text);
            if (ruleRespecting < 1 || ruleRespecting > 5)
            {
                MessageBox.Show("Please, enter a number between 1 and 5 for Rule Respecting");
                return;
            }
            string comment = txtComment.Text.Trim().ToLower();
            DateTime ratingDate = DateTime.Now;
            if (ratingDate < selectedReservation.DepartureDate)
            {
                MessageBox.Show("Guest didn't leave the accommodation. You can not give a feedback!");
                return;
            }
            else if (ratingDate > selectedReservation.DepartureDate.AddDays(5))
            {
                MessageBox.Show("More than 5 days passed. You can not rate this guest!");
                return;
            }



            GuestRating newGuestRating = new GuestRating()
            {
                Guest = guest,
                Cleanliness = cleanliness,
                RuleRespecting = ruleRespecting,
                Comment = comment,
                RatingDate = ratingDate,

            };




            guestRatingRepository.Save(newGuestRating);


            MessageBox.Show("Guest rating saved successfully.");

            Close();
        }
        private bool ValidateCleanlinessAndRuleRespecting()
        {
            if (string.IsNullOrEmpty(txtCleanliness.Text))
            {
                MessageBox.Show("Please enter a value for Cleanliness.");
                return false;
            }

            if (string.IsNullOrEmpty(txtRuleRespecting.Text))
            {
                MessageBox.Show("Please enter a value for Rule Respecting.");
                return false;
            }

            int cleanliness, ruleRespecting;

            if (!int.TryParse(txtCleanliness.Text, out cleanliness) || cleanliness < 1 || cleanliness > 5)
            {
                MessageBox.Show("Please enter a valid number between 1 and 5 for Cleanliness.");
                return false;
            }

            if (!int.TryParse(txtRuleRespecting.Text, out ruleRespecting) || ruleRespecting < 1 || ruleRespecting > 5)
            {
                MessageBox.Show("Please enter a valid number between 1 and 5 for Rule Respecting.");
                return false;
            }

            return true;
        }

    }
}