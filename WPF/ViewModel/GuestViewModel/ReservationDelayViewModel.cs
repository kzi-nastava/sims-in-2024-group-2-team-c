using BookingApp.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using BookingApp.Service;
using BookingApp.Model;
using System.Collections.ObjectModel;
using BookingApp.Service.AccommodationServices;

namespace BookingApp.WPF.ViewModel.GuestViewModel
{
    public class ReservationDelayViewModel : ViewModelBase
    {
        private GuestReservationDTO _selectedReservation;

        private readonly ReservationDelayService _reservationDelayService;

        public ICommand SendRequestCommand { get; }

        public GuestReservationDTO SelectedReservation
        {
            get { return _selectedReservation; }
            set
            {
                _selectedReservation = value;
                OnPropertyChanged(nameof(SelectedReservation));
            }
        }

        private DateTime _newArrivalDate;
        public DateTime NewArrivalDate
        {
            get { return _newArrivalDate; }
            set
            {
                _newArrivalDate = value;
                OnPropertyChanged(nameof(NewArrivalDate));
            }
        }

        private DateTime _newDepartureDate;
        public DateTime NewDepartureDate
        {
            get { return _newDepartureDate; }
            set
            {
                _newDepartureDate = value;
                OnPropertyChanged(nameof(NewDepartureDate));
            }
        }

        private ObservableCollection<ReservationDelayDTO> _reservationDelays;

        public ObservableCollection<ReservationDelayDTO> ReservationDelayRequests
        {
            get { return _reservationDelays; }
            set
            {
                _reservationDelays = value;
                OnPropertyChanged(nameof(ReservationDelay));
            }
        }

        public ReservationDelayViewModel(GuestReservationDTO selectedReservation)
        {
            _reservationDelayService = new ReservationDelayService();
            SelectedReservation = selectedReservation;
            SendRequestCommand = new ViewModelCommand<object>(SendRequest);

            NewArrivalDate = DateTime.Today.AddDays(1); // Postavi na trenutni datum
            NewDepartureDate = DateTime.Today.AddDays(2); // Postavi na sutrašnji datum
            ReservationDelayRequests = new ObservableCollection<ReservationDelayDTO>();
            LoadReservationDelayRequests();
        }

        private void LoadReservationDelayRequests()
        {
            try
            {
                var requests = _reservationDelayService.GetAllReservationDelays();
                ReservationDelayRequests.Clear();
                foreach (var request in requests)
                {
                    ReservationDelayRequests.Add(request);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading requests: {ex.Message}");
            }
            
        }

        public Guest getGuestByReservation(GuestReservationDTO _selectedReservation)
        {
            return _reservationDelayService.getGuestByReservation(_selectedReservation);
        }

        public Accommodation getAccommodationByReservation(GuestReservationDTO _selectedReservation)
        {
            return _reservationDelayService.getAccommodationByReservation(_selectedReservation);
        }


        private void SendRequest(object parameter)
        {
            if (NewArrivalDate == default || NewDepartureDate == default)
            {
                MessageBox.Show("Please choose new arrival and departure dates.");
                return;
            }

            if (NewArrivalDate >= NewDepartureDate)
            {
                MessageBox.Show("Departure date must be after arrival date.");
                return;
            }

            var reservationDelayDTO = new ReservationDelayDTO
            {
                Guest = new Guest() { Username = LoggedInUser.Username },
                Accommodation = new Accommodation() { Name = _selectedReservation.Name , Type = _selectedReservation.Type},
                NewCheckInDate = NewArrivalDate,
                NewCheckOutDate = NewDepartureDate,
                Status = ReservationDelayStatus.Pending
            };

            try
            {
                _reservationDelayService.SaveReservationDelay(reservationDelayDTO);
                MessageBox.Show("Request sent successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error sending request: {ex.Message}");
            }
        }
    }
}
