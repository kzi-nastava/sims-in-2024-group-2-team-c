using BookingApp.Commands;
using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Service.TourServices;
using BookingApp.WPF.View.GuideView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModel.GuideViewModel
{
    public class TourRequestStatistic_ViewModel : ViewModelBase
    {
        //private readonly LocationService _locationService;
        private readonly TourRequestService _tourRequestService;


        private ObservableCollection<int> _years;
        public ObservableCollection<int> Years
        {
            get => _years;
            set
            {
                _years = value;
                OnPropertyChanged(nameof(Years));
            }
        }
        private ObservableCollection<string> _locations;
        public ObservableCollection<string> Locations
        {
            get => _locations;
            set
            {
                _locations = value;
                OnPropertyChanged(nameof(Locations));
            }
        }
        private ObservableCollection<string> _languages;
        public ObservableCollection<string> Languages
        {
            get => _languages;
            set
            {
                _languages = value;
                OnPropertyChanged(nameof(Languages));
            }
        }
        private int _selectedYear;
        public int SelectedYear
        {
            get => _selectedYear;
            set
            {
                _selectedYear = value;
                OnPropertyChanged(nameof(SelectedYear));
            }
        }
        private string _selectedLocation;
        public string SelectedLocation
        {
            get => _selectedLocation;
            set
            {
                _selectedLocation = value;
                OnPropertyChanged(nameof(SelectedLocation));
            }
        }
        private string _selectedLanguage;
        public string SelectedLanguage
        {
            get => _selectedLanguage;
            set
            {
                _selectedLanguage = value;
                OnPropertyChanged(nameof(SelectedLanguage));
            }
        }
        private string _mostPopularLocation;
        public string MostPopularLocation
        {
            get => _mostPopularLocation;
            set
            {
                _mostPopularLocation = value;
                OnPropertyChanged(nameof(MostPopularLocation));
            }
        }
        private string _mostPopularLanguage;
        public string MostPopularLanguage
        {
            get => _mostPopularLanguage;
            set
            {
                _mostPopularLanguage = value;
                OnPropertyChanged(nameof(MostPopularLanguage));
            }
        }
        private int _numberOfRequestsByLanguage;
        public int NumberOfRequestsByLanguage
        {
            get => _numberOfRequestsByLanguage;
            set
            {
                _numberOfRequestsByLanguage = value;
                OnPropertyChanged(nameof(NumberOfRequestsByLanguage));
            }
        }
        private int _numberOfRequestsByLocation;
        public int NumberOfRequestsByLocation
        {
            get => _numberOfRequestsByLocation;
            set
            {
                _numberOfRequestsByLocation = value;
                OnPropertyChanged(nameof(NumberOfRequestsByLocation));
            }

        }
        private ObservableCollection<StatisticTourRequestDTO> _tourRequests;
        public ObservableCollection<StatisticTourRequestDTO> TourRequests
        {
            get { return _tourRequests; }
            set { _tourRequests = value; OnPropertyChanged(nameof(TourRequests)); }
        }
        public ICommand SearchCommand { get; set; }
       // public ICommand CreateTourLanguageCommand { get; set; }
//        public ICommand CreateTourLocationCommand { get; set; }

        //public ICommand BackCommand { get; set; }

        public TourRequestStatistic_ViewModel()
        {
            _tourRequestService = new TourRequestService();
            //_locationService = new LocationService();
            Years = new ObservableCollection<int>(Enumerable.Range(2020, DateTime.Now.Year - 2020 + 1));
            Years.Add(0);
            Locations = new ObservableCollection<string>(_tourRequestService.GetAllLocations());
            Languages = new ObservableCollection<string>(_tourRequestService.GetAllLanguages());
            ShowMostPopularLocation();
            ShowMostPopularLanguage();
            SearchCommand = new ViewModelCommandd(ExecuteSearchCommand);
            //CreateTourLanguageCommand = new ViewModelCommandd(ExecuteCreateCommandByLanguage);
           // CreateTourLocationCommand = new ViewModelCommandd(ExecuteCreateCommandByLocation);
            UpdateTourRequests();
            //BackCommand = 
            //SelectedYear = DateTime.Now.Year;
        }

        /*private void ExecuteCreateCommandByLanguage(object obj)
        {
            throw new NotImplementedException();
        }

        private void ExecuteCreateCommandByLocation(object obj)
        {
            string mostPopularItem = obj as string;

            if (!string.IsNullOrEmpty(mostPopularItem))
            {
                // Kreiramo novi ViewModel za CreateTourByRequest i prosleđujemo mostPopularItem
                CreateTourByRequest_ViewModel viewModel = new CreateTourByRequest_ViewModel
                {
                    Location = mostPopularItem  // Postavljamo lokaciju ili jezik u zavisnosti od parametra
                };

                // Navigacija na CreateTourByRequest Page
                Guide_CreateTourByRequest createTourPage = new Guide_CreateTourByRequest();
                createTourPage.DataContext = viewModel; // Postavljamo novi ViewModel kao DataContext za stranicu
                this.NavigationService.Navigate(createTourPage);
            }
        }*/

        private void ShowMostPopularLocation()
        {
            (string mostPopularLocation, int maxCountLocation) = _tourRequestService.GetMostPopularLocation();
            MostPopularLocation = mostPopularLocation;
            NumberOfRequestsByLocation = maxCountLocation;
        }
        private void ShowMostPopularLanguage()
        {
            (string mostPopularLanguage, int maxCountLanguage) = _tourRequestService.GetMostPopularLanguage();
            MostPopularLanguage = mostPopularLanguage;
            NumberOfRequestsByLanguage = maxCountLanguage;
        }
        private void ExecuteSearchCommand(object parameter)
        {
            UpdateTourRequests();
        }
        private void UpdateTourRequests()
        {
            TourRequests = new ObservableCollection<StatisticTourRequestDTO>(_tourRequestService.GetTourRequestStatistics());
            if (SelectedLocation != null /*&& SelectedLanguage == "Language"*/)
            {
                //SelectedLanguage = null;
                TourRequests = new ObservableCollection<StatisticTourRequestDTO>(_tourRequestService.GetTourRequestStatisticsByLocation(SelectedLocation));
            }
            if (SelectedLanguage != null /*&& SelectedLocation == "Location"*/)
            {
                //SelectedLocation = null;
                TourRequests = new ObservableCollection<StatisticTourRequestDTO>(_tourRequestService.GetTourRequestStatisticsByLanguage(SelectedLanguage));
            }
            if (SelectedYear != 0/* && SelectedLanguage == "Language" && SelectedLocation == "Location"*/)
            {
                TourRequests = new ObservableCollection<StatisticTourRequestDTO>(_tourRequestService.GetTourRequestStatisticsByYear(SelectedYear));
            }
        }
    }
}
