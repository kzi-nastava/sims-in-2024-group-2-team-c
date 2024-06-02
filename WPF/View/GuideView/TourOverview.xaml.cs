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
    public partial class TourOverview : Window, INotifyPropertyChanged
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
        private DateTime _filterDate = DateTime.Today;
       /* public DateTime FilterDate
        {
            get { return _filterDate; }
            set
            {
                if (_filterDate != value)
                {
                    _filterDate = value;
                    OnPropertyChanged(nameof(FilterDate));
                    FilterToursByDate(_filterDate);
                }
            }
        }*/

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
            //SelectedTour = (Tour)tourView.SelectedItem;
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
            SelectedTour = (Tour)tourView.SelectedItem;
            if (SelectedTour != null)
            {
                SelectedTour = (Tour)tourView.SelectedItem;
                //GuidedTourOverview guidedTourOverview = new GuidedTourOverview(SelectedTour);
                //guidedTourOverview.Show();
                //TourOverview tourOverviewWindow = new TourOverview();
                
                //Close();
            }
            else
            {
                MessageBox.Show("Please select a tour.");
            }
            //MessageBox.Show("Follow Tour button clicked!");
        }
        private void TourView_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (tourView.SelectedItem != null)
            {
                Tour selectedTour = (Tour)tourView.SelectedItem;

            }

        }
        private void FilterDate(object sender, RoutedEventArgs e)
        {
            FilterToursByDate(_filterDate);
        }

        private void FilterToursByDate(DateTime date)
        {
            List<TourInstance> instances = _tourInstanceRepository.FindByDate(date.Date);
            if (instances != null)
            {
                foreach(TourInstance instance in instances)
                {
                    int tourId = instance.IdTour;
                    Tours = new ObservableCollection<Tour>(_tours.Where(t => t.Id == tourId));
                    tourView.ItemsSource = Tours;
                }
            }
            else
            {
                Tours = new ObservableCollection<Tour>();
                tourView.ItemsSource = Tours;
            }
           
        }

    }
}
