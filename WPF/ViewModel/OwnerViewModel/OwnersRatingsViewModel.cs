using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Service.AccommodationServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.ViewModel.OwnerViewModel
{
    internal class OwnersRatingsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private ObservableCollection<AccommodationRate> rates;
        private AccommodationRateService rateService;

        public OwnersRatingsViewModel()
        {
            rateService = new AccommodationRateService();
            rateService.LoadAccommodationRates();
            _accommodationRates = new ObservableCollection<AccommodationRate>(rateService.AccommodationRates);
        }

        public ObservableCollection<AccommodationRate> _accommodationRates
        {
            get { return rates; }
            set
            {
                rates = value;
                OnPropertyChanged("_accommodationRates");
            }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}