using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Service.TourServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace BookingApp.WPF.View.GuideView
{
    /// <summary>
    /// Interaction logic for FutureToursOverview.xaml
    /// </summary>
    public partial class FutureToursOverview : Window
    {
         private ObservableCollection<FutureTourDTO> _futureTours;
         public ObservableCollection<FutureTourDTO> futureTours
         {
             get { return _futureTours; }
             set { _futureTours = value; }
         }
        //public List<FutureTourDTO> futureTours;

        private FutureTourDTO _selectedTour;
        public FutureTourDTO SelectedTour
        {
            get => _selectedTour;
            set
            {
                if (value != _selectedTour)
                {
                    _selectedTour = value;
                   // OnPropertyChanged();
                }
            }
        }
        private readonly TourService _tourService;
        private readonly FutureToursService _futureToursService;

        public FutureToursOverview()
        {
            InitializeComponent();
            DataContext = this;
            _tourService = new TourService();
            _futureToursService = new();
            //tourView.IsEnabled = true;
            LoadFutureTours();
        }
        void LoadFutureTours()
        {
            var futureToursView = _futureToursService.GetFutureTourDTOs();
            //futureTours = futureToursView;
            futureTours = new ObservableCollection<FutureTourDTO>(futureToursView);
        }
        private void FutureToursView_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (tourView.SelectedItem != null)
            {
                //Tour selectedTour = (Tour)tourView.SelectedItem;
                SelectedTour = (FutureTourDTO)tourView.SelectedItem;
            }

        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        // Event handler za klik na dugme "CANCEL TOUR"
        private void CancelTourButton_Click(object sender, RoutedEventArgs e)
        {
            _futureToursService.DeliverVoucherToTourists(SelectedTour.Id);
            _futureToursService.CancelTour(SelectedTour.Id);
            futureTours.Remove(SelectedTour);
        }
    }
}