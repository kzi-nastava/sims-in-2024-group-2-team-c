using BookingApp.DTO;
using BookingApp.Service.TourServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.ViewModel.TouristViewModel
{
    public class TouristHomeViewModel : ViewModelBase
    {

        private readonly TourService _tourService;
         

        private ObservableCollection<HomeTourDTO> _tours;

        public ObservableCollection<HomeTourDTO> Tours
        { 
            get { return _tours; }
            set
            {
                _tours = value;
                OnPropertyChanged(nameof(Tours));
            }

        }


        public TouristHomeViewModel() {

            _tourService = new TourService();
            Tours = new ObservableCollection<HomeTourDTO>(_tourService.GetAllTourDTOs());
        }
    }
}
