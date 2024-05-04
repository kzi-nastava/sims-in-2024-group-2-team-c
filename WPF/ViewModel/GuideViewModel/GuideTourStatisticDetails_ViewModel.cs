using BookingApp.Commands;
using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Service;
using BookingApp.Service.TourServices;
using BookingApp.WPF.View.GuideView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using System.Windows;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModel.GuideViewModel
{
    public class GuideTourStatisticDetails_ViewModel : ViewModelBase
    {
        private readonly EndedToursService _endedTourService;
        //private readonly System.Windows.Navigation.NavigationService navigationService;
        //private readonly MainWindow_ViewModel _mainViewModel;
        private int _selectedYear;
        private ObservableCollection<int> _years;
        private TourStatisticDTO _mostPopularTour;

        public ObservableCollection<int> Years
        {
            get => _years;
            set
            {
                _years = value;
                OnPropertyChanged(nameof(Years));
            }
        }

        public int SelectedYear
        {
            get => _selectedYear;
            set
            {
                _selectedYear = value;
                OnPropertyChanged(nameof(SelectedYear));
                LoadTourStatistic();
            }
        }

        public TourStatisticDTO MostPopularTour
        {
            get => _mostPopularTour;
            set
            {
                _mostPopularTour = value;
                OnPropertyChanged(nameof(MostPopularTour));
            }
        }

        public ICommand ShowTourCommand { get; }
       /* public RelayCommand NavigateBackCommand { get; }
        public RelayCommand NavigateToHomePageCommand { get; }*/

        public GuideTourStatisticDetails_ViewModel()
        {
            _endedTourService = new EndedToursService();
            //_mainViewModel = LoggedInUser.mainGuideViewModel;
            //navigationService = new System.Windows.Navigation.NavigationService();
            Years = new ObservableCollection<int>(Enumerable.Range(2010, DateTime.Now.Year - 2010 + 1));
            SelectedYear = DateTime.Now.Year;
            MostPopularTour = _endedTourService.FindMostVisitedTour();
            ShowTourCommand = new RelayCommand(ShowTour);
           // NavigateBackCommand = new RelayCommand(NavigateBack);
           // NavigateToHomePageCommand = new RelayCommand(NavigateToHomePage);
        }

       /* private void NavigateToHomePage()
        {
            MainWindow_ViewModel main = new MainWindow_ViewModel();
            GuideHomePage_ViewModel homePage = new GuideHomePage_ViewModel();
            //this.NavigationService.Navigate(main, homePage);
            _mainViewModel
        }

        private void NavigateBack()
        {
            this.NavigationService.GoBack();
        }*/

        private void LoadTourStatistic()
        {
            MostPopularTour = _endedTourService.FindMostVisitedTourForYear(SelectedYear);
        }

        private void ShowTour()
        {
            int selectedYear = SelectedYear;
            TourStatisticDTO mostPopularTour = _endedTourService.FindMostVisitedTourForYear(selectedYear);

            if (mostPopularTour != null)
            {
                //TourViewModel tourViewModel = new TourViewModel(mostPopularTour);
                //TourView.DataContext = tourViewModel; 
                MostPopularTour = mostPopularTour;
            }
            else
            {
                MessageBox.Show("No data available for the selected year.");
            }
        }
    }
}
