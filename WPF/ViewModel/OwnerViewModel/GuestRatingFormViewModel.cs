using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;

namespace BookingApp.WPF.ViewModel.OwnerViewModel 
{
    public class GuestRatingFormViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private Reservation selectedReservation;
        private ReservationRepository reservationRepository;
        private GuestRatingRepository guestRatingRepository;
        private GuestRepository guestRepository;
        private AccommodationRepository accommodationRepository;
        private ObservableCollection<Reservation> reservations;
        


        public GuestRatingFormViewModel()
        {
            reservationRepository = new ReservationRepository();
            reservationRepository.LoadReservationsFromCSV("../../../Resources/Data/reservations.csv");
            Reservations = new ObservableCollection<Reservation>(reservationRepository.GetAll());
            guestRatingRepository = new GuestRatingRepository();
            guestRepository = new GuestRepository();
            accommodationRepository = new AccommodationRepository();
           
        }

     
        public ObservableCollection<Reservation> Reservations
        {
            get { return reservations; }
            set
            {
                reservations = value;
                OnPropertyChanged("Reservations");
            }
        }

        public Reservation SelectedReservation
        {
            get { return selectedReservation; }
            set
            {
                selectedReservation = value;
                OnPropertyChanged("SelectedReservation");
            }
        }



        public void SaveGuestRating(int cleanliness, int ruleRespecting, string comment)
        {
            if (SelectedReservation == null)
            {
                MessageBox.Show("Please select a reservation.");
                return;
            }

            if (!ValidateCleanlinessAndRuleRespecting(cleanliness, ruleRespecting))
                return;


            //Guest guest = guestRepository.GetGuestById(SelectedReservation.Guest.Id);
            Guest guest = guestRepository.GetGuestByUsername(SelectedReservation.Guest.Username);



            // Validate cleanliness and rule respecting

            DateTime ratingDate = DateTime.Now;
            if (ratingDate < SelectedReservation.DepartureDate)
            {
                MessageBox.Show("Guest didn't leave the accommodation. You can not give a feedback!");
                return;
            }
            else if (ratingDate > SelectedReservation.DepartureDate.AddDays(5))
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

        }

        private bool ValidateCleanlinessAndRuleRespecting(int cleanliness, int ruleRespecting)
        {
            if (cleanliness < 1 || cleanliness > 5)
            {
                MessageBox.Show("Please, enter a number between 1 and 5 for Cleanliness");
                return false;
            }

            if (ruleRespecting < 1 || ruleRespecting > 5)
            {
                MessageBox.Show("Please, enter a number between 1 and 5 for Rule Respecting");
                return false;
            }

            return true;
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
