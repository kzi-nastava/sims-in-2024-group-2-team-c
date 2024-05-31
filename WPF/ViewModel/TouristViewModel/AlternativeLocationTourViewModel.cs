using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Service.TourServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingApp.WPF.ViewModel.TouristViewModel
{
    public class AlternativeLocationTourViewModel : ViewModelBase
    {



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

        private readonly SearchTourService searchTourService;
        private readonly LocationService locationService;
        

        public AlternativeLocationTourViewModel(int locationId,int tourId) {

            searchTourService = new SearchTourService();
            locationService = new LocationService();
            Location location = locationService.GetById(locationId);


            List<HomeTourDTO> AllToursLocation  = searchTourService.GetFilteredTours(location.City, string.Empty, 0);

            

            Tours = new ObservableCollection<HomeTourDTO>(AllToursLocation.Where(tour => tour.TourId != tourId).ToList());
        }   
    }
}
