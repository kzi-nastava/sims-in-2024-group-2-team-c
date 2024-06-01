using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Service.TourServices;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace BookingApp.WPF.ViewModel.TouristViewModel
{
    public  class RequestStatisticsViewModel : ViewModelBase
    {

        private double _acceptedPercentage;

        public double AcceptedPercentage
        {

            get { return _acceptedPercentage; }
            set
            {
                _acceptedPercentage = value;
                OnPropertyChanged(nameof(AcceptedPercentage));
                
            }

        }


        private double _invalidPercentage;

        public double InvalidPercentage
        {

            get { return _invalidPercentage; }
            set
            {
                _invalidPercentage = value;
                OnPropertyChanged(nameof(InvalidPercentage));

            }

        }



        private  ObservableCollection<int> _years;

        public ObservableCollection<int> Years
        {
            get { return _years; }
            set
            {
                _years = value;
                OnPropertyChanged(nameof(Years));
            }
        }

        private int _selectedItem;

        public int SelectedItem
        {

            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
                LoadPieChart();
                LoadTheLanguageChart();
                Load();

                OnPropertyChanged(nameof(PieSeriesCollection));
              //  LoadPieChart();

            }

        }


        private int _selectedYear;

        public int SelectedYear
        {

            get { return _selectedYear; }
            set
            {
                _selectedYear = value;
                OnPropertyChanged(nameof(SelectedYear));
                LoadNumberOfPeople();
                
                //OnPropertyChanged(nameof(NumberOfPeople));
            }

        }


        private double _numberOfPeople;

        public double NumberOfPeople
        {

            get { return _numberOfPeople; }
            set
            {
                _numberOfPeople = value;
                OnPropertyChanged(nameof(NumberOfPeople));
            }

        }


        public SeriesCollection LocationsCollection { get; set; }
        public SeriesCollection SeriesCollection { get; set; }

        public SeriesCollection PieSeriesCollection { get; set; }

        

        public List<string> LocationLabels { get; set; }
        public List<string> Labels { get; set; }
        public List<int> Values { get; set; }

        public List<int>  LocationValues { get; set; }
        public Func<double,string> Formatter { get; set; }
        public List<object> LanguageRequests { get; private set; }

        public List<object>  LocationRequests { get; private set; }

        public ViewModelCommandd GoBackCommand { get; }

        private readonly MainViewModel _mainViewModel;
        private readonly TourRequestService _tourRequestService;
        private readonly TouristStatisticsService _tourStatisticsService;


        public RequestStatisticsViewModel() {
            _mainViewModel = LoggedInUser.mainViewModel;
            _tourRequestService = new TourRequestService();
            _tourStatisticsService = new TouristStatisticsService();

            GoBackCommand = new ViewModelCommandd(ExecuteGoBackCommand);
            LoadTheYears();
            Load();
            LoadTheLanguageChart();
            LoadPieChart();
            LoadNumberOfPeople();
            
        }


        private void LoadNumberOfPeople()
        {

            NumberOfPeople = Math.Round(_tourStatisticsService.GetPeople(SelectedYear),2);


        }





        private void LoadPieChart()
        {

            int invalidRequests = _tourStatisticsService.CountInvalidRequests(SelectedItem);
            int validRequests = _tourStatisticsService.CountAcceptedRequests(SelectedItem);

            PieSeriesCollection = new SeriesCollection
            {
                new PieSeries
                {
                    Title="Accepted",
                    Values = new ChartValues<ObservableValue>{new ObservableValue(validRequests)},
                    DataLabels = true,
                    Fill = Brushes.LightGreen
                },
                new PieSeries
                {
                    Title="Invalid",
                    Values = new ChartValues<ObservableValue>{new ObservableValue(invalidRequests)},
                    DataLabels = true,
                    Fill = Brushes.LightSalmon
                }

            };


            double totalValue = 0;

            // Calculate total value
            double totalAcceptedValue = ((ObservableValue)PieSeriesCollection[0].Values[0]).Value;
            double totalInvalidValue = ((ObservableValue)PieSeriesCollection[1].Values[0]).Value;

            AcceptedPercentage = Math.Round((totalAcceptedValue / (totalAcceptedValue + totalInvalidValue)) * 100, 0);
            InvalidPercentage = Math.Round((totalInvalidValue / (totalAcceptedValue + totalInvalidValue)) * 100,0);




        }

        private void LoadTheYears()
        {

            Years = new ObservableCollection<int>(_tourStatisticsService.GetRequestYears());
            
        }

        public void ExecuteGoBackCommand(object obj)
        {
             _mainViewModel.ExecuteUserCommand(obj);
        }


        private void LoadTheLanguageChart()
        {

            List<TourRequest> requests = new List<TourRequest>();

            if (SelectedItem == 0)
            {
                 requests = _tourRequestService.GetByTourist(LoggedInUser.Id);
            }
            else
            {
                 requests = _tourRequestService.GetByTouristandYear(LoggedInUser.Id, SelectedItem);
            }

            

            var locationCounts = _tourStatisticsService.GroupByLocation(requests).OrderByDescending(x => x.Value) 
                         .ToList();

            LocationsCollection = new SeriesCollection();

            LocationLabels = locationCounts.Select(x => x.Key).ToList();
            LocationValues = locationCounts.Select(x => x.Value).ToList();



            LocationsCollection = new SeriesCollection
            {
                    new RowSeries
                    {
                        Title = "Tour Requests",
                        Values = new ChartValues<int>(LocationValues),
                        Fill = Brushes.LightGreen
                    }
            };

            Formatter = value => value.ToString();

            

            LocationRequests = new List<object>();

            var locationRequests = locationCounts
                                   .Select(g => new { Location = g.Key, LocationRequests = g.Value })
                                   .ToList();

            LocationRequests  = locationRequests.Cast<object>().ToList();

            OnPropertyChanged(nameof(LocationRequests));
            OnPropertyChanged(nameof(LocationsCollection));
            OnPropertyChanged(nameof(LocationLabels));
            OnPropertyChanged(nameof(LocationValues));

        }



        private void Load()
        {
            List<TourRequest> requests = new List<TourRequest>();

            if (SelectedItem == 0)
            {
                requests = _tourRequestService.GetByTourist(LoggedInUser.Id);
            }
            else
            {
                requests = _tourRequestService.GetByTouristandYear(LoggedInUser.Id, SelectedItem);
            }

            var languageCounts = requests.GroupBy(t => t.Language).ToDictionary(g => g.Key, g => g.Count());

            LanguageRequests = new List<object>();

            var languageRequests = requests.GroupBy(t => t.Language)
                                   .Select(g => new { Language = g.Key, Requests = g.Count() })
                                   .ToList();

            // Assign the languageRequests to the LanguageRequests property
            LanguageRequests = languageRequests.Cast<object>().ToList();


            SeriesCollection = new SeriesCollection();
            Labels = languageCounts.Keys.ToList();
            Values = languageCounts.Values.ToList();

            SeriesCollection.Add(
                
               new ColumnSeries{
                    Title = "Tour Requests",
                    Values = new ChartValues<int>(Values),
                    Fill = Brushes.LightGreen
               }
               
               );

            OnPropertyChanged(nameof(LanguageRequests));
            OnPropertyChanged(nameof(SeriesCollection));
            OnPropertyChanged(nameof(Labels));
            OnPropertyChanged(nameof(Values));

        }


    }
}
