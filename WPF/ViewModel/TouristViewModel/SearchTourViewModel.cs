using BookingApp.Model;
using BookingApp.Service.TourServices;
using BookingApp.WPF.View.TouristView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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

        private int? _duration;
        public int? Duration
        {
            get { return _duration; }
            set
            {
                _duration = value;
                OnPropertyChanged(nameof(Duration));
            }
        }

        private readonly LocationService _locationService;
        private readonly LanguageService _languageService;

        
        public ViewModelCommandd SearchCommand { get; }
        private readonly MainViewModel _mainViewModel;

        public SearchTourViewModel() {
            _locationService = new LocationService();
            _languageService = new LanguageService();
            _mainViewModel = LoggedInUser.mainViewModel;
            LoadLocations();
            LoadLanguages();
            SearchCommand = new ViewModelCommandd(ExecuteSearchCommand);
        }

        public void LoadLocations()
        {
            Locations = new ObservableCollection<Location>(_locationService.GetAll());
        }


        public void LoadLanguages()
        {
            Languages = new ObservableCollection<Language>(_languageService.GetAll());
        }

        public void ExecuteSearchCommand(object obj)
        {

            if (SelectedLocation != null || SelectedLanguage != null || Duration.HasValue) {

                string SearchedLanguage = SelectedLanguage != null ? SelectedLanguage.Name : string.Empty;
                string SearchedCity = SelectedLocation != null ? SelectedLocation.City : string.Empty;
                Duration = Duration ?? 0;

                _mainViewModel.ExecuteSearchCommand(SearchedCity, SearchedLanguage.ToString(), Duration);
            }
            else
            {
                _mainViewModel.ExecuteShowTourCommand(obj);

            }

            
        }


    }
}
