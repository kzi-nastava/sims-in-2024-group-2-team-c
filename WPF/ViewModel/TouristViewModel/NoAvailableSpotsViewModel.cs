using BookingApp.Model;
using BookingApp.Service.TourServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.ViewModel.TouristViewModel
{
    public class NoAvailableSpotsViewModel : ViewModelBase
    {

        public ViewModelCommandd SeeMoreCommand { get; }
        private readonly MainViewModel _mainViewModel;


        private string location;
        public string Location
        {
            get { return location; }
            set { location = value;
                OnPropertyChanged(nameof(Location));
            }
        }

        private int tourId;
        public int TourId
        {
            get { return tourId; }
            set
            {
                tourId = value;
                OnPropertyChanged(nameof(TourId));
            }
        }

        private readonly LocationService locationService;


        public NoAvailableSpotsViewModel(string location, int tourId) {
            _mainViewModel = LoggedInUser.mainViewModel;
            SeeMoreCommand = new ViewModelCommandd(ExecuteSeeMore);
            Location = location;
            TourId = tourId;
            locationService = new LocationService();
        }

        private void ExecuteSeeMore(object obj)
        {

            int locationId = locationService.GetIdByCountry(Location);


            _mainViewModel.ExecuteAlternativeTours(locationId,tourId);
        }

    }
}
