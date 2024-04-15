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
        public ICommand RateReservationCommand { get; }
        public ICommand CancelReservationCommand => new ViewModelCommand<object>(CancelReservation);
        public ICommand RateAccommodationCommand { get; }

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

        private readonly GuestReservationService _guestReservationService;

        public GuestReservationViewModel()
        {
            _guestReservationService = new(new GuestReservationRepository());
            GuestReservations = new ObservableCollection<GuestReservationDTO>();
            RateReservationCommand = new ViewModelCommand<object>(RateReservation);
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

        private void RateReservation(object parameter)
        {
            if (parameter != null)
            {
                RateAccommodationWindow rateAccommodationWindow = new RateAccommodationWindow(parameter as GuestReservationDTO);
                rateAccommodationWindow.Show();
            }
            else
            {
                MessageBox.Show("Please select a reservation to rate.");
            }
        }

    }


}

