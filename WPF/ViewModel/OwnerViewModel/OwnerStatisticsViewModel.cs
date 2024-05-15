using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Service.AccommodationServices;
using BookingApp.Service.OwnerService;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.ViewModel.OwnerViewModel
{
    public class OwnerStatisticsViewModel : INotifyPropertyChanged
    {
        private OwnerStatisticsService _ownerStatisticsService;
        private ObservableCollection<Accommodation> accommodations;
        private Accommodation selectedAccommodation;
        private OwnerStatistics ownerStatistic;
        private RenovationService renovationService; //mozda vadi iz accommodation repository
        AccommodationRepository accommodationRepository;
        //dodajem ova tri ovako
        private Dictionary<int, int> _monthlyReservations;
        private Dictionary<int, int> _monthlyCancellations;
        private Dictionary<int, int> _monthlyDelays;
        public Dictionary<int, int> MonthlyReservations
        {
            get { return _monthlyReservations; }
            set
            {
                _monthlyReservations = value;
                OnPropertyChanged(nameof(MonthlyReservations));
            }
        }

        public Dictionary<int, int> MonthlyCancellations
        {
            get { return _monthlyCancellations; }
            set
            {
                _monthlyCancellations = value;
                OnPropertyChanged(nameof(MonthlyCancellations));
            }
        }

        public Dictionary<int, int> MonthlyDelays
        {
            get { return _monthlyDelays; }
            set
            {
                _monthlyDelays = value;
                OnPropertyChanged(nameof(MonthlyDelays));
            }
        }

        public void GetMonthlyStatistics(int accommodationId, int year)
        {
            var accommodation = accommodationRepository.GetAccommodationById(accommodationId); // Pretpostavimo da imate ovu metodu

            MonthlyReservations = _ownerStatisticsService.NumberOfReservationsByMonth(accommodation, year);
            MonthlyCancellations = _ownerStatisticsService.NumberOfCancellationsByMonth(accommodation, year);
            MonthlyDelays = _ownerStatisticsService.NumberOfDelaysByMonth(accommodation, year);
        }

        public ObservableCollection<OwnerStatistics> OwnerStatistics { get; set; }

        public OwnerStatisticsViewModel() 
        {
            _ownerStatisticsService = new OwnerStatisticsService();
            renovationService = new RenovationService();
            accommodationRepository = new AccommodationRepository();
            Accommodations = new ObservableCollection<Accommodation>(renovationService.GetAccommodations());
            RefreshStatistics(); // Ovo osvežava statistiku kada se instancira ViewModel
        }

        public ObservableCollection<Accommodation> Accommodations
        {
            get { return accommodations; }
            set
            {
                accommodations = value;
                OnPropertyChanged("Accommodations");
            }
        }

        public Accommodation SelectedAccommodation
        {
            get { return selectedAccommodation; }
            set
            {
                if (value != null)
                {
                    selectedAccommodation = value;
                    OwnerStatistic = OwnerStatistics.Where(s => s.Accommodation.Name == selectedAccommodation.Name).First();
                    OnPropertyChanged("SelectedAccommodation");
                }
            }
        }

        public OwnerStatistics OwnerStatistic
        {
            get { return ownerStatistic; }
            set
            {
                if (value != null)
                {
                    ownerStatistic = value;
                    OnPropertyChanged("OwnerStatistic");
                }
            }
        }



        public void RefreshStatistics()
        {
            OwnerStatistics = new ObservableCollection<OwnerStatistics>();
            foreach (var accommodation in Accommodations)
            {
                var ownerStatistic = new OwnerStatistics
                {
                    Accommodation = accommodation,
                    ReservationsByYear = _ownerStatisticsService.NumberOfReservationsByYear(accommodation),
                    CancellationsByYear = _ownerStatisticsService.NumberOfCancellationsByYear(accommodation),
                    DelaysByYear = _ownerStatisticsService.NumberOfDelaysByYear(accommodation)
                };
                OwnerStatistics.Add(ownerStatistic);
            }
            OnPropertyChanged(nameof(OwnerStatistics));
        }



        // Implementacija interfejsa INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
