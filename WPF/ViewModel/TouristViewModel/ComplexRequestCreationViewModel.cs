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
    public  class ComplexRequestCreationViewModel : ViewModelBase
    {

        private readonly LocationService _locationService;
        private readonly LanguageService _languageService;
        private readonly ComplexTourRequestService _complexTourRequestService;


        private readonly MainViewModel _mainViewModel;


        private int? _peopleNumber;
        public int? PeopleNumber
        {

            get { return _peopleNumber; }
            set
            {
                _peopleNumber = value;
                OnPropertyChanged(nameof(PeopleNumber));
            }

        }


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
                UpdateRequestCreatable();
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
                UpdateRequestCreatable();
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
                UpdateRequestCreatable();
            }
        }


        private DateTime _startDate = DateTime.Now.AddMonths(2);
        public DateTime StartDate
        {
            get { return _startDate; }
            set
            {
                _startDate = value;
                OnPropertyChanged(nameof(StartDate));
                UpdateRequestCreatable();
            }
        }

        private DateTime _endDate = DateTime.Now.AddMonths(2);
        public DateTime EndDate
        {
            get { return _endDate; }
            set
            {
                _endDate = value;
                OnPropertyChanged(nameof(EndDate));
                UpdateRequestCreatable();
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


        private bool _isRequestCreatable;
        public bool IsRequestCreatable
        {
            get { return _isRequestCreatable; }
            set
            {
                _isRequestCreatable = value;
                OnPropertyChanged(nameof(IsRequestCreatable));
            }
        }

        private PartViewModel _part;
        public PartViewModel Part
        {
            get { return _part; }
            set
            {
                _part = value;
                OnPropertyChanged(nameof(Part));
            }
        }



        private readonly ComplexTourRequestViewModel _complexTourRequestViewModel;
        public ViewModelCommandd SearchCommand { get; private set; }
        public ViewModelCommandd CreateRequestCommand { get; private set; }
        public ComplexRequestCreationViewModel(PartViewModel part, ComplexTourRequestViewModel complexTourRequestViewModel) {
            _mainViewModel = LoggedInUser.mainViewModel;
            Part = part;
            _complexTourRequestViewModel = complexTourRequestViewModel;
            _locationService = new LocationService();
            _languageService = new LanguageService();
            _complexTourRequestService = new ComplexTourRequestService();

            People = new ObservableCollection<PeopleInfo>();
            PeopleList = new List<PeopleInfo>();

            SearchCommand = new ViewModelCommandd(Search);
            CreateRequestCommand = new ViewModelCommandd(CreateRequest);

            LoadLocations();
            LoadLanguages();

            IsRequestCreatable = false;
        }



        private void UpdateRequestCreatable()
        {

            bool basicCriteriaMet = !string.IsNullOrEmpty(Description) &&
                                     SelectedLocation != null &&
                                     SelectedLanguage != null &&
                                     StartDate != DateTime.MinValue &&
                                     EndDate != DateTime.MinValue;


            bool datesValid = StartDate < EndDate && StartDate > DateTime.Today && EndDate > DateTime.Today;


            bool peoplePresent = People.Count > 0;


            IsRequestCreatable = basicCriteriaMet && datesValid && peoplePresent;
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
                UpdateRequestCreatable();
            }
        }




        private void CreateRequest(object parameter)
        {

            foreach (var person in People)
            {
                person.Age = 20;
                PeopleList.Add(person);
            }

           int id =  _complexTourRequestService.CreateTourRequest(SelectedLocation, Description, SelectedLanguage, StartDate, EndDate, PeopleList);

            Part.RequestId = id;
            Part.Status = "Filled out";
            Part.IsEnabled = false;

            _mainViewModel.ExecuteGoBAckToComplexRequests(_complexTourRequestViewModel);

            People.Clear();

            

        }



    }
}
