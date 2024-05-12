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

        public SeriesCollection SeriesCollection { get; set; }
        public List<string> Labels { get; set; }
        public List<int> Values { get; set; }


        public ViewModelCommandd GoBackCommand { get; }

        private readonly MainViewModel _mainViewModel;
        private readonly TourRequestService _tourRequestService;


        public RequestStatisticsViewModel() {
            _mainViewModel = LoggedInUser.mainViewModel;
            _tourRequestService = new TourRequestService();

            GoBackCommand = new ViewModelCommandd(ExecuteGoBackCommand);

            Load();
        }

        public void ExecuteGoBackCommand(object obj)
        {
             _mainViewModel.ExecuteUserCommand(obj);
        }


        private void Load()
        {
            List<TourRequest> requests = _tourRequestService.GetByTourist(LoggedInUser.Id);

            var languageCounts = requests.GroupBy(t => t.Language).ToDictionary(g => g.Key, g => g.Count());

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
