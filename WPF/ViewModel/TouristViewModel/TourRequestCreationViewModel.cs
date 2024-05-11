using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Service.TourServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModel.TouristViewModel
{
    public class TourRequestCreationViewModel : ViewModelBase
    {

        private readonly LocationService _locationService;
        private readonly LanguageService _languageService;
        private readonly TourRequestService tourRequestService;


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

        private string _description;
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
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


        private DateTime _startDate = new DateTime(2024, 1, 1);
        public DateTime StartDate
        {
            get { return _startDate; }
            set
            {
                _startDate = value;
                OnPropertyChanged(nameof(StartDate));
            }
        }

        private DateTime _endDate = new DateTime(2024, 1, 1);
        public DateTime EndDate
        {
            get { return _endDate; }
            set
            {
                _endDate = value;
                OnPropertyChanged(nameof(EndDate));
            }
        }


        private ObservableCollection<PeopleInfo> _people;
        public ObservableCollection<PeopleInfo> People
        {
            get { return _people; }
            set
            {
                _people = value;
                OnPropertyChanged(nameof(People));
            }
        }


        private List<PeopleInfo> _peopleList;
        public List<PeopleInfo> PeopleList
        {
            get { return _peopleList; }
            set
            {
                _peopleList = value;
                OnPropertyChanged(nameof(PeopleList));
            }
        }


        public ViewModelCommandd SearchCommand { get; private set; }
        public ViewModelCommandd CreateRequestCommand { get; private set; }



        public TourRequestCreationViewModel() {
            _locationService = new LocationService();
            _languageService = new LanguageService();
            tourRequestService = new TourRequestService();

            People = new ObservableCollection<PeopleInfo>();
            PeopleList = new List<PeopleInfo>();

            SearchCommand = new ViewModelCommandd(Search);
            CreateRequestCommand = new ViewModelCommandd(CreateRequest);

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


        private void Search(object parameter)
        {
            if (int.TryParse(parameter?.ToString(), out int numberOfPeople))
            {
                People.Clear(); // Clear existing people
                for (int i = 0; i < numberOfPeople; i++)
                {
                    // Add a new person with empty details
                    People.Add(new PeopleInfo());
                }
            }
        }

        // Create request command handler
        private void CreateRequest(object parameter)
        {
            // Add all entered people to the PeopleList
            foreach (var person in People)
            {
                PeopleList.Add(person);
            }

            tourRequestService.CreateTourRequest(SelectedLocation,Description,SelectedLanguage,StartDate,EndDate,PeopleList);

            // Clear the People collection after adding them to the list
            People.Clear();
        }

    }
}
