
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

namespace BookingApp.WPF.ViewModel.OwnerViewModel
{
    public class ReservationDelayViewModel : INotifyPropertyChanged
    {
        private readonly ReservationDelayService _reservationDelayService;
        private ReservationRepository _reservationreposiotry;
        private ReservationService _reservationservice;

        public ObservableCollection<ReservationDelay> ReservationDelays { get; set; }
        public ReservationDelay SelectedReservationDelay { get; set; }


        public ReservationDelayViewModel()
        {
            //_reservationDelayRepository = new ReservationDelayRepository();
            _reservationDelayService = new ReservationDelayService();
            ReservationDelays = new ObservableCollection<ReservationDelay>(_reservationDelayService.GetAll());           
            _reservationservice = new ReservationService();
            _reservationreposiotry = new ReservationRepository();

        }

        public void LoadReservationDelays()
        {
            ReservationDelays.Clear();
            List<ReservationDelay> delays = _reservationDelayService.GetAll();
            foreach (var delay in delays)
            {
                ReservationDelays.Add(delay);
            }

        }

        private bool _availability;
        public bool Availability
        {
            get { return _availability; }
            set
            {
                if (_availability != value)
                {
                    _availability = value;
                    OnPropertyChanged(nameof(Availability));
                }
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
                _reservationDelayService.Update(SelectedReservationDelay);

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
                _reservationDelayService.Update(SelectedReservationDelay);

                // Poziv servisa da ažurira status
                _reservationDelayService.UpdateReservationDelayStatus(SelectedReservationDelay.ReservationDelayId, ReservationDelayStatus.Rejected);

                LoadReservationDelays(); // Osvežavanje liste
            }

        }

        public void SaveExplanationToCSV(string explanation)
        {
            if (SelectedReservationDelay != null)
            {

                SelectedReservationDelay.Explanation = explanation;


                string filePath = "../../../Resources/Data/reservationdelay.csv";
                string[] lines = File.ReadAllLines(filePath);

                List<string> updatedLines = new List<string>();
                foreach (string line in lines)
                {
                    string[] parts = line.Split('|');
                    if (parts.Length >= 1 && parts[0] == SelectedReservationDelay.ReservationDelayId.ToString())
                    {
                        parts[6] = explanation;
                        updatedLines.Add(string.Join("|", parts));
                    }
                    else
                    {
                        updatedLines.Add(line);
                    }
                }

                File.WriteAllLines(filePath, updatedLines);
            }
        }
        
    
}
}


