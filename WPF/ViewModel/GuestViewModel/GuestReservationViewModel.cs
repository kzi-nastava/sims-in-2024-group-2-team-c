using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Service.AccommodationServices;
using BookingApp.WPF.View.GuestView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModel.GuestViewModel
{
    public class GuestReservationViewModel : ViewModelBase
    {

        private readonly GuestReservationService _guestReservationService;
        private readonly AccommodationRateService _accommodationRateService;

        public ICommand RateReservationCommand { get; }
        public ICommand CancelReservationCommand => new ViewModelCommand<object>(CancelReservation);
        public ICommand RateAccommodationCommand { get; }

        public ICommand RescheduleReservationCommand { get; }

        private ObservableCollection<GuestReservationDTO> _guestReservations;

        public ObservableCollection<GuestReservationDTO> GuestReservations
        {
            get { return _guestReservations; }
            set
            {
                _guestReservations = value;
                OnPropertyChanged(nameof(GuestReservation));
            }
        }

        public GuestReservationViewModel()
        {
            _guestReservationService = new(new GuestReservationRepository());
            _accommodationRateService = new(new AccommodationRateRepository());
            GuestReservations = new ObservableCollection<GuestReservationDTO>();
            RateReservationCommand = new ViewModelCommand<object>(RateReservation);
            RescheduleReservationCommand = new ViewModelCommand<object>(RescheduleReservation);
            LoadGuestReservations();
        }

        private void LoadGuestReservations()
        {
            try
            {
                var guestReservations = _guestReservationService.GetAllGuestReservations(LoggedInUser.Id);
                GuestReservations.Clear();
                foreach (var reservation in guestReservations)
                {
                    GuestReservations.Add(reservation);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading reservations: {ex.Message}");
            }
        }

        private void CancelReservation(object selectedReservationObj)
        {
            try
            {
                if (!(selectedReservationObj is GuestReservationDTO selectedReservation))
                {
                    MessageBox.Show("Please select a reservation to cancel.");
                    return;
                }
                int reservationId = selectedReservation.Id;
                string result = _guestReservationService.CancelReservation(reservationId);
                MessageBox.Show(result);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error cancelling reservation: {ex.Message}");
            }
        }

        private bool GetIsReservedStatus(int reservationId)
        {
            try
            {
                return _guestReservationService.GetIsReservedStatus(reservationId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting reservation status: {ex.Message}");
                return false; // Ako se desi greška, vraćamo false
            }
        }

        private bool CanRateReservation(String Name)
        {
            try
            {
                return !_accommodationRateService.HasUserRatedAccommodation(LoggedInUser.Id, Name);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error checking if user can rate accommodation: {ex.Message}");
                return false;
            }
        }



        private void RateReservation(object parameter)
        {
            if (parameter != null)
            {
                var selectedReservation = parameter as GuestReservationDTO;
                bool isReserved = GetIsReservedStatus(selectedReservation.Id);

                if (!isReserved)
                {
                    MessageBox.Show("You can rate only the accommodations you have visited.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                if (!CanRateReservation(selectedReservation.Name))
                {
                    MessageBox.Show("You have already rated this accommodation.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                var checkOutDate = selectedReservation.CheckOut;
                var currentDate = DateTime.Now;

                if (currentDate > checkOutDate)
                {
                    if (currentDate.Subtract(checkOutDate).Days <= 5)
                    {
                        RateAccommodationWindow rateAccommodationWindow = new RateAccommodationWindow(selectedReservation);
                        rateAccommodationWindow.Show();
                    }
                    else
                    {
                        MessageBox.Show("You can rate the accommodation and owner no later than 5 days after your stay.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else if (checkOutDate > currentDate)
                {
                    MessageBox.Show("You can rate the accommodation only after your stay.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Please select a reservation to rate.");
            }
        }

        private void RescheduleReservation(object parameter)
        {
            if (parameter != null)
            {
                var selectedReservation = parameter as GuestReservationDTO;

                // Provera da li je odabrana rezervacija prošla ili trenutno traje
                if (selectedReservation.CheckIn < DateTime.Today || selectedReservation.CheckOut <= DateTime.Today)
                {
                    MessageBox.Show("You cannot reschedule past or ongoing reservations.");
                }
                else
                {
                    // Otvori prozor za reschedule rezervacije
                    ReservationDelayWindow rescheduleWindow = new ReservationDelayWindow(selectedReservation);
                    rescheduleWindow.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("Please select a reservation to reschedule.");
            }
        }



    }


}

