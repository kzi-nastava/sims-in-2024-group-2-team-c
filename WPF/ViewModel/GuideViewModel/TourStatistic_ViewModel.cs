using BookingApp.Commands;
using BookingApp.DTO;
using BookingApp.Service.TourServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.Windows.Navigation;

namespace BookingApp.WPF.ViewModel.GuideViewModel
{
    class TourStatistic_ViewModel : ViewModelBase
    {
        private ObservableCollection<TourStatisticDTO> _endedTours;
        public ObservableCollection<TourStatisticDTO> EndedTours
        {
            get { return _endedTours; }
            set
            {
                if (_endedTours != value)
                {
                    _endedTours = value;
                    OnPropertyChanged(nameof(EndedTours));
                }
            }
        }

        private TourStatisticDTO _selectedTour;
        public TourStatisticDTO SelectedTour
        {
            get { return _selectedTour; }
            set
            {
                if (_selectedTour != value)
                {
                    _selectedTour = value;
                    OnPropertyChanged(nameof(SelectedTour));
                }
            }
        }

        private readonly EndedToursService _endedToursService;

        //public ICommand ShowCommand { get; private set; }
        //public ICommand BackCommand { get; private set; }

        public TourStatistic_ViewModel()
        {
            _endedToursService = new EndedToursService();
            //ShowCommand = new Commands.RelayCommand(ShowExecute);
            //BackCommand = new Commands.RelayCommand(BackExecute);
            LoadEndedTours();
        }

        /*private void BackExecute()
        {
            NavigationService?.GoBack();
        }*/

        /*private void ShowExecute()
        {
            if (SelectedTour != null)
            {
                
                MaxTouristsBlock.DataContext = SelectedTour;
                ReservedTouristsBlock.DataContext = SelectedTour;
                PresentTouristsBlock.DataContext = SelectedTour;

                LessBlock.DataContext = SelectedTour;
                BetweenBlock.DataContext = SelectedTour;
                MoreBlock.DataContext = SelectedTour;
            }
           else
            {
                MessageBox.Show("Please select a tour.", "Tour Statistics", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }*/

        private void LoadEndedTours()
        {
            EndedTours = new ObservableCollection<TourStatisticDTO>(_endedToursService.GetEndedTours());
        }

    }
}
