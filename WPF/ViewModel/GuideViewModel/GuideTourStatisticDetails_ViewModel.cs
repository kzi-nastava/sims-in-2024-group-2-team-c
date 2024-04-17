using BookingApp.Commands;
using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Service.TourServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModel.GuideViewModel
{
    internal class GuideTourStatisticDetails_ViewModel : ViewModelBase
    {
        private readonly EndedToursService _endedTourService;
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

        public GuideTourStatisticDetails_ViewModel()
        {
            _endedTourService = new EndedToursService();
            Years = new ObservableCollection<int>(Enumerable.Range(2010, DateTime.Now.Year - 2010 + 1));
            SelectedYear = DateTime.Now.Year;
            MostPopularTour = _endedTourService.FindMostVisitedTour();
            ShowTourCommand = new RelayCommand(ShowTour);
        }

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
