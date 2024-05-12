using BookingApp.Commands;
using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Service.TourServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModel.GuideViewModel
{
    public class TourRequests_ViewModel : ViewModelBase
    {
        private readonly TourRequestService _tourRequestService;
        private readonly MainWindow_ViewModel mainView;
        private ObservableCollection<TourRequestDTO> _tourRequests;
        public ObservableCollection<TourRequestDTO> TourRequests
        {
            get { return _tourRequests; }
            set { _tourRequests = value; OnPropertyChanged(nameof(TourRequests)); }
        }

        private TourRequestDTO _selectedTourRequest;
        public TourRequestDTO SelectedTourRequest
        {
            get { return _selectedTourRequest; }
            set { _selectedTourRequest = value; OnPropertyChanged(nameof(SelectedTourRequest)); }
        }
        private DateTime _selectedStartDate;
        public DateTime SelectedStartDate
        {
            get { return _selectedStartDate; }
            set
            {
                _selectedStartDate = value;
                OnPropertyChanged(nameof(SelectedStartDate));
            }
        }

        private DateTime _selectedEndDate;
        public DateTime SelectedEndDate
        {
            get { return _selectedEndDate; }
            set
            {
                _selectedEndDate = value;
                OnPropertyChanged(nameof(SelectedEndDate));
            }
        }

        private string _selectedLocation;
        public string SelectedLocation
        {
            get { return _selectedLocation; }
            set
            {
                _selectedLocation = value;
                OnPropertyChanged(nameof(SelectedLocation));
            }
        }

        private int _selectedNumberOfTourists;
        public int SelectedNumberOfTourists
        {
            get { return _selectedNumberOfTourists; }
            set
            {
                _selectedNumberOfTourists = value;
                OnPropertyChanged(nameof(SelectedNumberOfTourists));
            }
        }

        private string _selectedLanguage;
        public string SelectedLanguage
        {
            get { return _selectedLanguage; }
            set
            {
                _selectedLanguage = value;
                OnPropertyChanged(nameof(SelectedLanguage));
            }
        }
        public ViewModelCommandd BackCommand { get; }

        public ViewModelCommandd AcceptTourRequestCommand { get; }

        public ViewModelCommandd SearchTourRequestCommand { get; }
        public TourRequests_ViewModel() 
        {
            mainView = LoggedInUser.mainGuideViewModel;
            _tourRequestService = new TourRequestService();
            LoadRequests();
            BackCommand = new ViewModelCommandd(Back);
            AcceptTourRequestCommand = new ViewModelCommandd(AcceptRequest);
            SearchTourRequestCommand = new ViewModelCommandd(SearchTourRequests);
        }
        public void LoadRequests()
        {
            TourRequests = new ObservableCollection<TourRequestDTO>(_tourRequestService.GetAllTourRequestDTOs());
        }
        /*public void LoadDatePicker()
        {
            DateTime? selectedStartDate = SelectedStartDate;
            DateTime? selectedEndDate = SelectedEndDate;
            if (selectedStartDate.HasValue && selectedEndDate.HasValue)
            {
                DateTime startDate = selectedStartDate.Value;
                DateTime endDate = selectedEndDate.Value;
            }
        }*/

        private void SearchTourRequests(object obj)
        {
            var allTourRequests = new ObservableCollection<TourRequestDTO>(_tourRequestService.GetAllTourRequestDTOs());
            //LoadDatePicker();
            if (SelectedStartDate == default && SelectedEndDate == default && SelectedLocation == null && SelectedNumberOfTourists == 0 && SelectedLanguage == null)
            {
                TourRequests = allTourRequests;
                return;
            }

            var filteredTourRequests = new List<TourRequestDTO>(allTourRequests);

            DateTime? selectedStartDate = SelectedStartDate;
            DateTime? selectedEndDate = SelectedEndDate;
            if (selectedStartDate.HasValue && selectedEndDate.HasValue)
            {
                DateTime startDate = selectedStartDate.Value;
                DateTime endDate = selectedEndDate.Value;
                if (startDate != default && endDate != default)
                {
                    filteredTourRequests = _tourRequestService.FilterRequestsByDate(SelectedStartDate, SelectedEndDate);

                }
            }
            /*if (SelectedStartDate != default && SelectedEndDate != default)
            {
                filteredTourRequests = _tourRequestService.FilterRequestsByDate(SelectedStartDate, SelectedEndDate);

            }*/

            if (!string.IsNullOrEmpty(SelectedLocation))
            {
                filteredTourRequests = _tourRequestService.FilterRequestsByLocation(SelectedLocation);
                    
            }
            if (SelectedNumberOfTourists > 0)
            {
                filteredTourRequests = _tourRequestService.FilterRequestsByNumberOfPeople(SelectedNumberOfTourists);
            }

            if (!string.IsNullOrEmpty(SelectedLanguage))
            {
                
                filteredTourRequests = _tourRequestService.FilterRequestsByLanguage(SelectedLanguage);
            }
            TourRequests = new ObservableCollection<TourRequestDTO>(filteredTourRequests);
        }

        private void AcceptRequest(object obj)
        {
            
            if (SelectedTourRequest != null)
            {
                var canAccept = _tourRequestService.AcceptRequest(SelectedTourRequest);
                if (canAccept == true)
                    LoadRequests();
               // MessageBox.Show("Ok");
            }
            /*else
            {
                MessageBox.Show("Select");
            }*/
        }

        private void Back(object t)
        {
            //GuideHomePage_ViewModel homePage = new GuideHomePage_ViewModel();
            var execute = mainView.NavigateToHomePageCommand;
        }
    }
}
