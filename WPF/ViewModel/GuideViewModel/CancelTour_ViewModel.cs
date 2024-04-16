using BookingApp.DTO;
using BookingApp.Service.TourServices;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModel.GuideViewModel
{
    public class CancelTour_ViewModel : ViewModelBase
    {
        private readonly FutureToursService _futureToursService;
        private ObservableCollection<FutureTourDTO> _futureTours;
        public ObservableCollection<FutureTourDTO> FutureTours
        {
            get { return _futureTours; }
            set { _futureTours = value; OnPropertyChanged(nameof(FutureTours)); }
        }

        private FutureTourDTO _selectedTour;
        public FutureTourDTO SelectedTour
        {
            get => _selectedTour;
            set { _selectedTour = value; OnPropertyChanged(nameof(SelectedTour)); }
        }

        public ICommand CancelTourCommand { get; private set; }
        public CancelTour_ViewModel()
        {
            _futureToursService = new FutureToursService();
            FutureTours = new ObservableCollection<FutureTourDTO>(_futureToursService.GetFutureTourDTOs());
            CancelTourCommand = new Commands.RelayCommand(CancelTour);
        }

        private void CancelTour()
        {
            if (SelectedTour != null)
            {
                _futureToursService.DeliverVoucherToTourists(SelectedTour.Id);
                _futureToursService.CancelTour(SelectedTour.Id);
                FutureTours.Remove(SelectedTour);
            }
            else
            {
                MessageBox.Show("Please select a tour to cancel.");
            }
        }


    }
}
