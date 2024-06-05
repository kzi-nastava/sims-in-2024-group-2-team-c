using BookingApp.Model;
using BookingApp.Service.TourServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModel.GuideViewModel
{
    public class AcceptTour_ViewModel : ViewModelBase
    {
        private readonly ComplexTourRequestService _complexTourRequestService;
        //private ObservableCollection<DateTime> _availableSlots;
        private List<DateTime> _availableSlots;
        public List<DateTime> AvailableSlots
        {
            get { return _availableSlots; }
            set
            {
                if (_availableSlots != value)
                {
                    _availableSlots = value;
                    OnPropertyChanged(nameof(AvailableSlots));
                }
            }

        }
        private DateTime _selectedSlot;
        public DateTime SelectedSlot
        {
            get { return _selectedSlot; }
            set
            {
                if (_selectedSlot != value)
                {
                    _selectedSlot = value;
                    OnPropertyChanged(nameof(SelectedSlot));
                }
            }

        }
        private TourRequest _request;
        public TourRequest Request
        {
            get { return _request; }
            set
            {
                if (_request != value)
                {
                    _request = value;
                    OnPropertyChanged(nameof(Request));
                }
            }

        }
        public ICommand AcceptCommand { get; }
        public AcceptTour_ViewModel(List<DateTime> availableSlots, TourRequest request) 
        {
            AcceptCommand = new ViewModelCommandd(Accept);
            //AvailableSlots = new ObservableCollection<DateTime>(availableSlots);
            AvailableSlots = availableSlots;
            _complexTourRequestService = new ComplexTourRequestService();
            Request = request;
        }

        private void Accept(object obj)
        {
            _complexTourRequestService.AcceptTourPart(Request.Id, LoggedInUser.Id, _selectedSlot);
            MessageBox.Show("Zahtev je prihvacen");
        }
    }
}
