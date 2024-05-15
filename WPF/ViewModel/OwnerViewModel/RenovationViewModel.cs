using BookingApp.Model;
using BookingApp.Service.AccommodationServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingApp.WPF.ViewModel.OwnerViewModel
{
    public class RenovationViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private RenovationService renovationService;
        private ObservableCollection<Accommodation> accommodations;
        private Accommodation selectedAccommodation;


        public RenovationViewModel()
        {
            renovationService = new RenovationService(); 
            Accommodations = new ObservableCollection<Accommodation>(renovationService.GetAccommodations());
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
                selectedAccommodation = value;
                OnPropertyChanged("SelectedAccommodation");
            }
        }


        public void SaveRenovation(Accommodation accommodation,DateTime startdate, DateTime enddate, int duration)
        {
            Renovation newRenovation = new Renovation()
            {
                Accommodation = accommodation,
                StartDate = startdate,
                EndDate = enddate,
                Duration = duration
            };
            renovationService.Save(newRenovation);
        }



        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
