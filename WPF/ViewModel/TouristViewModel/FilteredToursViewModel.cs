using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Service.TourServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.ViewModel.TouristViewModel
{
    public class FilteredToursViewModel : ViewModelBase
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

        private string _filters;
        public string Filters
        {
            get { return _filters; }
            set { _filters = value; OnPropertyChanged(nameof(Filters));}
        }

        private readonly SearchTourService searchTourService;


        public FilteredToursViewModel(string Location,string Language,int? Duration) {

            searchTourService = new SearchTourService();
            Tours = new ObservableCollection<HomeTourDTO>(searchTourService.GetFilteredTours(Location,Language,Duration));
            LoadFilters(Location,Language,Duration);
        }

        private void LoadFilters(string location,string language,int? duration) {

            string result = "";
            if(!string.IsNullOrEmpty(location))
            {
                result +=" " + location;
            }
            if (!string.IsNullOrEmpty(language))
            {
                result+=" " + language;
            }
            if(duration != 0)
            {
                result += " " + "Duration:" + " " + duration.ToString() + " h";
            }
            Filters = result;

        }


    }
}
