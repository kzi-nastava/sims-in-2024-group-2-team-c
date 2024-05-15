using BookingApp.Model;
using BookingApp.Service.AccommodationServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingApp.WPF.ViewModel.OwnerViewModel
{
    public class RenovationAvailableDatesViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private RenovationService renovationService;

        public RenovationAvailableDatesViewModel(Accommodation selectedAccommodation, DateTime startDate, DateTime endDate, int duration)
        {
            renovationService = new RenovationService();
            SelectedAccommodation = selectedAccommodation;
            LoadAvailableDates(selectedAccommodation, startDate, endDate, duration);
        }

       


        private List<AvailableDateDisplay> availableDates;
        public List<AvailableDateDisplay> AvailableDates
        {
            get { return availableDates; }
            set
            {
                availableDates = value;
                OnPropertyChanged("AvailableDates");
            }
        }

        private AvailableDateDisplay selectedDate;
        public AvailableDateDisplay SelectedDate
        {
            get { return selectedDate; }
            set
            {
                selectedDate = value;
                OnPropertyChanged("SelectedDate");
            }
        }

        private Accommodation selectedAccommodation;
        public Accommodation SelectedAccommodation
        {
            get { return selectedAccommodation; }
            set
            {
                selectedAccommodation = value;
                OnPropertyChanged("SelectedAccommodation");
            }
        }

        private string description;
        public string Description
        {
            get { return description; }
            set
            {
                description = value;
                OnPropertyChanged("Description");
            }
        }

    

        public void LoadAvailableDates(Accommodation selectedAccommodation, DateTime startDate, DateTime endDate, int duration)
        {
            AvailableDates = renovationService.FindAvailableReservations(selectedAccommodation, startDate, endDate, duration);
           
        }
        // Renovations = renovationService.GetAllRenovations();
        /* public void LoadAvailable()
         {
             Renovations = renovationService.GetAllRenovations();
         }*/


        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Save()
        {
            if (SelectedDate != null)
            {
                RenovationAvailableDate renovation = new RenovationAvailableDate
                {
                    Accommodation = SelectedAccommodation,
                    StartDate = SelectedDate.CheckIn,
                    EndDate = SelectedDate.CheckOut,
                    Description = Description 
                };

                renovationService.SaveRenovation(renovation);
                MessageBox.Show("Renovation saved successfully!");
            }
            else
            {
                MessageBox.Show("Please select a date for renovation first.");
            }
        }

       


    }
}
