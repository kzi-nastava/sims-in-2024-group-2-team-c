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
    /// Interaction logic for KeyPointsListOverview.xaml
    /// </summary>
    public partial class KeyPointsOverview : Window
    {
        private readonly KeyPointRepository _keyPointRepository;
        private readonly TouristRepository _touristRepository;
        private readonly PeopleInfoRepository _peopleInfoRepository;

        private ObservableCollection<KeyPoint> _keyPoints;
        public ObservableCollection<KeyPoint> KeyPoints
        {
            get { return _keyPoints; }
            set
            {
                if (_keyPoints != value)
                {
                    _keyPoints = value;
                    OnPropertyChanged();
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

         public KeyPointsOverview(List<KeyPoint> toursKeyPoints)
         {
             InitializeComponent();
             DataContext = this;
             keyPointsListView.IsEnabled = true;
             _keyPointRepository = new KeyPointRepository();
            _peopleInfoRepository = new PeopleInfoRepository();
            _touristRepository = new TouristRepository();
             KeyPoints = new ObservableCollection<KeyPoint>();
             LoadKeyPoints(toursKeyPoints);

         }
       /* public KeyPointsOverview(List<int> toursKeyPointsid)
        {
            InitializeComponent();
            DataContext = this;
            keyPointsListView.IsEnabled = true;
            _keyPointRepository = new KeyPointRepository();
            KeyPoints = new ObservableCollection<KeyPoint>(_keyPointRepository.GetByIdList(toursKeyPointsid));
            //KeyPoints = new ObservableCollection<KeyPoint>(_keyPointRepository.GetAll());

            //LoadKeyPoints(KeyPoints);
            keyPointsListView.ItemsSource = _keyPoints;

        }*/

        private void LoadKeyPoints(List<KeyPoint> keyPoints)
        {
            keyPointsListView.ItemsSource = keyPoints;
        }

        private KeyPoint _previouslySelectedKeyPoint;
        private void ActivateKeyPointButton_Click(object sender, RoutedEventArgs e)
        {
            KeyPoint selectedKeyPoint = (sender as FrameworkElement).DataContext as KeyPoint;

            if (_previouslySelectedKeyPoint != null && _previouslySelectedKeyPoint != selectedKeyPoint)
            {
                _previouslySelectedKeyPoint.Active = !_previouslySelectedKeyPoint.Active;
                _keyPointRepository.Update(_previouslySelectedKeyPoint);
            }

            _previouslySelectedKeyPoint = selectedKeyPoint;

            selectedKeyPoint.Active = !selectedKeyPoint.Active;
            _keyPointRepository.Update(selectedKeyPoint);

            keyPointsListView.Items.Refresh();
            /* KeyPoint selectedKeyPoint = (sender as FrameworkElement).DataContext as KeyPoint;
             if(selectedKeyPoint.Active == true)
             {
                 selectedKeyPoint.Active = false;
                 _keyPointRepository.Update(selectedKeyPoint);
                 keyPointsListView.Items.Refresh();
             }
             else
             {
                 selectedKeyPoint.Active = true;
                 _keyPointRepository.Update(selectedKeyPoint);
                 keyPointsListView.Items.Refresh();
             }*/
        }
        private void TouristsOverviewButton_Click(object sender, RoutedEventArgs e)
        {
            KeyPoint selectedKeyPoint = (sender as FrameworkElement).DataContext as KeyPoint;

            
            //List<Tourist> turists = new List<Tourist>();
            List<PeopleInfo> peopleInfo = new List<PeopleInfo>();
            foreach (int id in selectedKeyPoint.PeopleIds)
            {
                //Tourist t = _touristRepository.GetById(id);
               // turists.Add(t);

                PeopleInfo p  = _peopleInfoRepository.GetById(id);
                peopleInfo.Add(p);
            }
            
            KeyPointsTuristsView touristsView = new KeyPointsTuristsView(peopleInfo, selectedKeyPoint);
            touristsView.Show();



            /*if (keyPointsListView.SelectedItem != null)
            {
                KeyPoint selectedKeyPoint = (KeyPoint)keyPointsListView.SelectedItem;
                TouristsOverview touristsOverviewWindow = new TouristsOverview(selectedKeyPoint);
                touristsOverviewWindow.Show();
            }
             KeyPoint selectedKeyPoint = (sender as FrameworkElement).DataContext as KeyPoint;

            // Otvori novi prozor za pregled turista na selektovanoj ključnoj tački
            TouristsOverview touristsOverviewWindow = new TouristsOverview(selectedKeyPoint);
             touristsOverviewWindow.Show();*/
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            /*GuidedTourOverview guidedTourOverviewWindow = new GuidedTourOverview();
            guidedTourOverviewWindow.Show();*/
            Close();
        }
    }
}
