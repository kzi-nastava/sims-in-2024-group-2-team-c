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
    public class RenovationListViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private ObservableCollection<RenovationAvailableDate> renovations;
        private RenovationService service;

    
        public RenovationListViewModel()
        {
            service = new RenovationService();
            //service.GetAllRenovations(); //?
            Renovations = new ObservableCollection<RenovationAvailableDate>(service.GetAllRenovations());
        }

        public ObservableCollection<RenovationAvailableDate> Renovations
        {
            get { return renovations; }
            set
            {
                renovations = value;
                OnPropertyChanged("Renovations");
            }
        }

        private RenovationAvailableDate selectedRenovation; //ovo sam promenila iz REnovation
        public RenovationAvailableDate SelectedRenovation
        {
            get { return selectedRenovation; }
            set
            {
                selectedRenovation = value;
                OnPropertyChanged("SelectedRenovation");
            }
        }

        public void CancelRenovation()
        {
            if (SelectedRenovation != null)
            {
                // Provera da li ima više od 5 dana do početka renovacije
                TimeSpan timeUntilStart = SelectedRenovation.StartDate - DateTime.Now;
                if (timeUntilStart.Days > 5)
                {
                    service.CancelRenovation(SelectedRenovation);
                    MessageBox.Show("Renovation cancelled successfully!");
                    
                }
                else
                {
                    MessageBox.Show("Renovation cannot be cancelled because it is less than 5 days until its start date.");
                }
            }
            else
            {
                MessageBox.Show("Please select a renovation to cancel.");
            }
        }





        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
