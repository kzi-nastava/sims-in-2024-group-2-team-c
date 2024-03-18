using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
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
    /// Interaction logic for TourOverview.xaml
    /// </summary>
    public partial class TourOverview : Window
    {
        private ObservableCollection<Tour> _tours;
        public ObservableCollection<Tour> Tours
        {
            get { return _tours; }
            set
            {
                if (_tours != value)
                {
                    _tours = value;
                    OnPropertyChanged(nameof(Tour));
                }
            }
        }

        private Tour _selectedTour;
        public Tour SelectedTour
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

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private readonly TourRepository _tourRepository;
        private readonly TourInstanceRepository _tourInstanceRepository;
        private readonly LocationRepository _locationRepository;

        public TourOverview()
        {
            InitializeComponent();
            _tourRepository = new TourRepository();
            _tourInstanceRepository = new TourInstanceRepository();
            Tours = new ObservableCollection<Tour>(_tourRepository.GetAll());
            //SelectedTour = tourView.SelectedItem;
            tourView.IsEnabled = true;
            FilterToursByDate(DateTime.Today);
            UpdateTourView();
        }

        private void UpdateTourView()
        {
            List<Tour> tours = _tourRepository.GetAll();
           // List<Tour> tours = new List<Tour>();
            List<TourInstance> instances = _tourInstanceRepository.GetAll();
            /*foreach(TourInstance i in instances)
            {
                Tour t = _tourRepository.GetById(i.IdTour);
                tours.Add(t);
            }*/
            //tourView.ItemsSource = instances;
            tourView.ItemsSource = tours;
            //LoadLocations(tours);


        }

        /*private void LoadLocations(List<Tour> tours)
        {

            foreach (Tour tour in tours)
            {
                Location locationObject = _locationRepository.GetById(tour.LocationId);
                tour.LocationId = locationObject.Id;
               /* if (locationObject != null)
                {
                    tour.Location = $"{locationObject.City}, {locationObject.Country}";
                }
                else
                {
                    tour.Location = "Location not found";
                }
            }
            tourView.Items.Refresh();

        }*/

        private void CreateTourButton_Click(object sender, RoutedEventArgs e)
        {
            TourForm tourForm = new TourForm();
            tourForm.Show();
            Close();
        }

        private void FollowTourButton_Click(object sender, RoutedEventArgs e)
        {
            // Dodajte logiku za pracenje ture ovde
            MessageBox.Show("Follow Tour button clicked!");
        }
        private void TourView_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (tourView.SelectedItem != null)
            {
                Tour selectedTour = (Tour)tourView.SelectedItem;

            }

        }

        private void FilterToursByDate(DateTime date)
        {
            TourInstance instance = _tourInstanceRepository.FindByDate(date.Date);
            int tourId = instance != null ? instance.IdTour : -1;

            if (tourId == -1)
            {
                Tours = new ObservableCollection<Tour>();
            }
            else
            {
                Tours = new ObservableCollection<Tour>(_tours.Where(t => t.Id == tourId));
            }
            /*Tours = new ObservableCollection<Tour>(_tourRepository.GetAll());
            List<int> ids = new List<int>();
            List<TourInstance> instances = _tourInstanceRepository.GetAll();
            foreach (TourInstance t in instances)
            {
                TourInstance instance = _tourInstanceRepository.FindByDate(date.Date);
                int TourId = instance.IdTour;
                ids.Add(TourId);
            }
            foreach(int id in ids) 
            {
                Tours = (ObservableCollection<Tour>)_tourRepository.GetAll().Where(t => t.Id == id);
            }*/
        }

    }
}
