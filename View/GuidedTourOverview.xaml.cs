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
            DataContext = this;
            SelectedTour = selectedTour;
            Location = _locationRepository.GetById(SelectedTour.LocationId);
            LoadLocation(Location);
            DateTime today = DateTime.Today;
            //TourInstance = _tourInstanceRepository.GetByTourId(SelectedTour.Id);
            TourInstance = FilterInstanceByDate(today);


        }
        public TourInstance FilterInstanceByDate(DateTime date)
        {
            List<TourInstance> instances = _tourInstanceRepository.FindByDate(date);
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
        private void LoadLocation(Location location)
        {
            Location = location;
            LocationTextBlock.Text = $"{location.City}, {location.Country}";
        }

        private void StartTour_Click(object sender, RoutedEventArgs e)
        {
            if (TourInstance != null)
            {
                TourInstance.Started = true;
                _tourInstanceRepository.Update(TourInstance);
                InfoTextBlock.Text = "Tour started successfully.";
                InfoTextBlock.Visibility = Visibility.Visible;
            }
            else
            {
                InfoTextBlock.Text = "Tour instance is not available.";
                InfoTextBlock.Visibility = Visibility.Visible;
            }
        }

        private void EndTour_Click(object sender, RoutedEventArgs e)
        {
            if (TourInstance != null)
            {
                TourInstance.Ended = true;
                 _tourInstanceRepository.Update(TourInstance);
                InfoTextBlock.Text = "Tour ended successfully.";
                InfoTextBlock.Visibility = Visibility.Visible;
            }
            else
            {
                InfoTextBlock.Text = "Tour instance is not available.";
                InfoTextBlock.Visibility = Visibility.Visible;
            }
        }

        private void BackToTourOverview_Click(object sender, RoutedEventArgs e)
        {
            TourOverview tourOverviewWindow = new TourOverview();
            tourOverviewWindow.Show();
            Close();
        }

        private void ViewKeyPointsButton_Click(object sender, RoutedEventArgs e)
        {
            // KeyPointsWindow keyPointsWindow = new KeyPointsWindow(_tour.KeyPoints);
            // keyPointsWindow.ShowDialog();
        }
    }
}

