
using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Service;
using BookingApp.Service.ReservationService;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BookingApp.ViewModel
{
    public class ReservationDelayViewModel : INotifyPropertyChanged
    {
        private readonly ReservationDelayRepository _reservationDelayRepository;
        private readonly ReservationDelayService _reservationDelayService;
        private  ReservationRepository _reservationreposiotry;
        private ReservationService _reservationservice;

        public ObservableCollection<ReservationDelay> ReservationDelays { get; set; }
        public ReservationDelay SelectedReservationDelay { get;  set; }
        

        public ReservationDelayViewModel()
        {
            _reservationDelayRepository = new ReservationDelayRepository();
            ReservationDelays = new ObservableCollection<ReservationDelay>(_reservationDelayRepository.GetAll());
            _reservationDelayService = new ReservationDelayService();
            _reservationservice = new ReservationService();
            _reservationreposiotry = new ReservationRepository();

        }

        public void LoadReservationDelays()
        {
            ReservationDelays.Clear();
            List<ReservationDelay> delays = _reservationDelayRepository.GetAll();
            foreach (var delay in delays)
            {
                ReservationDelays.Add(delay);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        


    public void ApproveReservationDelay()
        {
            if (SelectedReservationDelay != null)
            {

                SelectedReservationDelay.Status = ReservationDelayStatus.Approved;
                _reservationDelayRepository.Update(SelectedReservationDelay);

                // Poziv servisa da ažurira status
                
                _reservationDelayService.UpdateReservationDelayStatus(SelectedReservationDelay.ReservationDelayId, ReservationDelayStatus.Approved);

                

                LoadReservationDelays(); // Osvežavanje liste
                //menjanje datuma rezervacije

                Reservation oldReservation = _reservationservice.FindOldReservation(SelectedReservationDelay.Guest, SelectedReservationDelay.Accommodation);

                if (oldReservation != null)
                {
                    // Ažuriraj datum stare rezervacije na datum nove rezervacije
                    oldReservation.ArrivalDate = SelectedReservationDelay.NewCheckInDate;
                    oldReservation.DepartureDate = SelectedReservationDelay.NewCheckOutDate;
                    _reservationreposiotry.Update(oldReservation);

                    LoadReservationDelays(); // Osvežavanje liste
                }

            }
        }

        public void RejectReservationDelay()
        {
            if (SelectedReservationDelay != null)
            {
                SelectedReservationDelay.Status = ReservationDelayStatus.Rejected;
                _reservationDelayRepository.Update(SelectedReservationDelay);

                // Poziv servisa da ažurira status
                _reservationDelayService.UpdateReservationDelayStatus(SelectedReservationDelay.ReservationDelayId, ReservationDelayStatus.Rejected);

                LoadReservationDelays(); // Osvežavanje liste
            }

        }
       

    }
}


