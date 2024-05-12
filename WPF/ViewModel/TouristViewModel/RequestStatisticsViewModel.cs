using BookingApp.Model;
using BookingApp.Service.TourServices;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace BookingApp.WPF.ViewModel.TouristViewModel
{
    public  class RequestStatisticsViewModel : ViewModelBase
    {

        public SeriesCollection LocationsCollection { get; set; }
        public SeriesCollection SeriesCollection { get; set; }

        public List<string> LocationLabels { get; set; }
        public List<string> Labels { get; set; }
        public List<int> Values { get; set; }

        public Func<double,string> Formatter { get; set; }
        public List<object> LanguageRequests { get; private set; }

        public ViewModelCommandd GoBackCommand { get; }

        private readonly MainViewModel _mainViewModel;
        private readonly TourRequestService _tourRequestService;
        private readonly TouristStatisticsService _tourStatisticsService;


        public RequestStatisticsViewModel() {
            _mainViewModel = LoggedInUser.mainViewModel;
            _tourRequestService = new TourRequestService();
            _tourStatisticsService = new TouristStatisticsService();

            GoBackCommand = new ViewModelCommandd(ExecuteGoBackCommand);

            Load();
            LoadTheLanguageChart();
        }

        public void ExecuteGoBackCommand(object obj)
        {
             _mainViewModel.ExecuteUserCommand(obj);
        }


        private void LoadTheLanguageChart()
        {

            List<TourRequest> requests = _tourRequestService.GetByTourist(LoggedInUser.Id);

            var locationCounts = _tourStatisticsService.GroupByLocation(requests);



        }



        private void Load()
        {
            List<TourRequest> requests = _tourRequestService.GetByTourist(LoggedInUser.Id);

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

        }


    }
}
