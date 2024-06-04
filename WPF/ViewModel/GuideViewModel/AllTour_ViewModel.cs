using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Service.TourServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.ViewModel.GuideViewModel
{
    public class AllTour_ViewModel : ViewModelBase
    {
        //private readonly TourService _tourService;
        //private readonly TourInstanceService _tourInstanceService;
        //private readonly LocationService _locationService;
        private readonly TourViewService _tourViewService;
        private ObservableCollection<TourViewDTO> _tours;
        public ObservableCollection<TourViewDTO> Tours
        {
            get { return _tours; }
            set { _tours = value; OnPropertyChanged(nameof(Tours)); }
        }

        private TourViewDTO _selectedTour;
        public TourViewDTO SelectedTour
        {
            get => _selectedTour;
            set { _selectedTour = value; OnPropertyChanged(nameof(SelectedTour)); }
        }
        public AllTour_ViewModel() 
        {
            //_tourService = new TourService();
            //_tourInstanceService = new TourInstanceService();
            //_locationService = new LocationService();
            _tourViewService = new TourViewService();
            Tours = new ObservableCollection<TourViewDTO>(_tourViewService.GetDTOs());

        }
    }
}
