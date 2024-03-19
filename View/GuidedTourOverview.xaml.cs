using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BookingApp.View
{
    /// <summary>
    /// Interaction logic for GuidedTourOverview.xaml
    /// </summary>
    public partial class GuidedTourOverview : Window, INotifyPropertyChanged
    {
        private Tour _tour;
        private TourInstance _tourInstance;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Tour SelectedTour
        {
            get { return _tour; }
            set
            {
                if (_tour != value)
                {
                    _tour = value;
                    OnPropertyChanged();
                }
            }
        }

        public TourInstance TourInstance
        {
            get { return _tourInstance; }
            set
            {
                if (_tourInstance != value)
                {
                    _tourInstance = value;
                    OnPropertyChanged();
                }
            }
        }

        private Location _location;

        public Location Location
        {
            get { return _location; }
            set
            {
                if (_location != value)
                {
                    _location = value;
                    OnPropertyChanged();
                }
            }
        }

        private readonly TourRepository _tourRepository;
        private readonly TourInstanceRepository _tourInstanceRepository;
        private readonly LocationRepository _locationRepository;

        public GuidedTourOverview(Tour selectedTour)
        {
            InitializeComponent();
            _tourRepository = new TourRepository();
            _tourInstanceRepository = new TourInstanceRepository();
            _locationRepository = new LocationRepository();
            SelectedTour = selectedTour;
            Location =_locationRepository.GetById(SelectedTour.LocationId);
            DateTime today = DateTime.Today;
            //TourInstance = _tourInstanceRepository.GetByTourId(SelectedTour.Id);
            TourInstance = FilterInstanceByDate(today);
            DataContext = this;
        }
        public TourInstance FilterInstanceByDate(DateTime date)
        {
            List<TourInstance> instances = _tourInstanceRepository.FindByDate(date.Date);
            if (instances != null)
            {
                foreach (TourInstance instance in instances)
                {
                    if (instance.IdTour == SelectedTour.Id)
                        return instance;
                    
                }
            }
            return instances.Find(i => i.IdTour == SelectedTour.Id);
        }
        private void StartTour_Click(object sender, RoutedEventArgs e)
        {
            // Implementacija početka ture
        }

        private void EndTour_Click(object sender, RoutedEventArgs e)
        {
            // Implementacija završetka ture
        }

        private void BackToTourOverview_Click(object sender, RoutedEventArgs e)
        {
            // Povratak na prethodni prozor (TourOverview)
            Close();
        }

        private void ViewKeyPointsButton_Click(object sender, RoutedEventArgs e)
        {
            // Otvaranje prozora za prikaz ključnih tačaka ture
            // Implementacija zavisi od vaših modela i logike aplikacije
            // Na primer:
            // KeyPointsWindow keyPointsWindow = new KeyPointsWindow(_tour.KeyPoints);
            // keyPointsWindow.ShowDialog();
        }
    }
}
