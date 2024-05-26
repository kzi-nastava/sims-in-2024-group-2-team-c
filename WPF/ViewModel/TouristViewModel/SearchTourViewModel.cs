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
    public class SearchTourViewModel :ViewModelBase
    {
        private ObservableCollection<Location> _locations;
        public ObservableCollection<Location> Locations
        {
            get { return _locations; }
            set
            {
                _locations = value;
                OnPropertyChanged(nameof(Locations));
            }
        }

        private Location _selectedLocation;
        public Location SelectedLocation
        {
            get { return _selectedLocation; }
            set
            {
                _selectedLocation = value;
                OnPropertyChanged(nameof(SelectedLocation));
                
            }
        }

        private ObservableCollection<Language> _language;
        public ObservableCollection<Language> Languages
        {
            get { return _language; }
            set
            {
                _language = value;
                OnPropertyChanged(nameof(Languages));
            }
        }

        private Language _selectedLanguage;
        public Language SelectedLanguage
        {
            get { return _selectedLanguage; }
            set
            {
                _selectedLanguage = value;
                OnPropertyChanged(nameof(SelectedLanguage));
               
            }
        }


        private readonly LocationService _locationService;
        private readonly LanguageService _languageService;

        public SearchTourViewModel() {
            _locationService = new LocationService();
            _languageService = new LanguageService();

            LoadLocations();
            LoadLanguages();
        }

        public void LoadLocations()
        {
            Locations = new ObservableCollection<Location>(_locationService.GetAll());
        }


        public void LoadLanguages()
        {
            Languages = new ObservableCollection<Language>(_languageService.GetAll());
        }


    }
}
