using BookingApp.DTO;
using BookingApp.Service.TourServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModel.GuideViewModel
{
    public class Status_ViewModel : ViewModelBase
    {
        private readonly GuideStatusService guideStatusService;
        //private readonly LanguageService languageService;
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
        private int _year;
        public int Year
        {
            get => _year;
            set
            {
                _year = value;
                OnPropertyChanged(nameof(Year));
            }
        }
        private StatusDTO _statusDTO;
        public StatusDTO StatusDTO
        {
            get => _statusDTO;
            set
            {
                _statusDTO = value;
                OnPropertyChanged(nameof(StatusDTO));
            }
        }
        private string _isSuperguide;
        public string IsSuperguide
        {
            get => _isSuperguide;
            set
            {
                _isSuperguide = value;
                OnPropertyChanged(nameof(IsSuperguide));
            }
        }
        private string _superguideLanguage;
        public string SuperguideLanguage
        {
            get => _superguideLanguage;
            set
            {
                _superguideLanguage = value;
                OnPropertyChanged(nameof(SuperguideLanguage));
            }
        }
        public ICommand FindCommand;
        public Status_ViewModel() 
        {
            guideStatusService = new GuideStatusService();
            Year = DateTime.Now.Year;
            Languages = new ObservableCollection<string>(guideStatusService.GetLanguages());
            FindCommand = new ViewModelCommandd(FindResults);
            if(isGuideSuperguide(Year))
            {
                IsSuperguide = $"Yes";
                SuperguideLanguage = guideStatusService.GetSuperguideLanguage(Year);
            }
            else
            {
                IsSuperguide = $"No";
                SuperguideLanguage = $"/";
            }
        }
        public bool isGuideSuperguide(int Year)
        {
            return guideStatusService.IsSuperGuide(Year);
        }

        private void FindResults(object obj)
        {
            if (SelectedLanguage != null)
            {
                StatusDTO = guideStatusService.GetGuideStatus(Year, SelectedLanguage);
            }
        }
    }
}
