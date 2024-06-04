using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Service.TourServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.PeerToPeer.Collaboration;
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
    /// Interaction logic for KeyPointsTuristsView.xaml
    /// </summary>
    public partial class KeyPointsTuristsView : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /*private ObservableCollection<Tourist> _tourists;
        public ObservableCollection<Tourist> Tourists
        {
            get { return _tourists; }
            set
            {
                _tourists = value;
                OnPropertyChanged();
            }
        }*/
        private KeyPoint SelectedKeyPoint;
        private ObservableCollection<PeopleInfo> _tourists;
        public ObservableCollection<PeopleInfo> Tourists
        {
            get { return _tourists; }
            set
            {
                _tourists = value;
                OnPropertyChanged();
            }
        }
        private int _capacity;
        public int Capacity
        {
            get { return _capacity; }
            set
            {
                if (_capacity != value)
                {
                    _capacity = value;
                    OnPropertyChanged();
                }
            }
        }
        private int _reserved;
        public int Reserved
        {
            get { return _reserved; }
            set
            {
                if (_reserved != value)
                {
                    _reserved = value;
                    OnPropertyChanged();
                }
            }
        }
        private int _present;
        public int Present
        {
            get { return _present; }
            set
            {
                if (_present != value)
                {
                    _present = value;
                    OnPropertyChanged();
                }
            }
        }
        private readonly KeyPointRepository _keyPointRepository;
        private readonly TouristService _touristService;
        private readonly PeopleInfoService _peopleInfoService;
        private readonly TouristNotificationService touristNotificationService;
        private readonly TourService tourService;
        public KeyPointsTuristsView(List<PeopleInfo> touristsList, KeyPoint selectedKeyPoint)
        {
            InitializeComponent();
            DataContext = this;
            touristsListBox.IsEnabled = true;
            _keyPointRepository = new KeyPointRepository();
            _peopleInfoService = new PeopleInfoService();
            _touristService = new TouristService();
            touristNotificationService = new TouristNotificationService();
            tourService = new TourService();
            touristsList = _peopleInfoService.GetAll();
            Tourists = new ObservableCollection<PeopleInfo>(touristsList);
            touristsListBox.ItemsSource = touristsList;
            SelectedKeyPoint = selectedKeyPoint;
            LoadLabels(touristsList, selectedKeyPoint);
        }
        public void LoadLabels(List<PeopleInfo> touristsList, KeyPoint selectedKeyPoint)
        {
            Tour tour = tourService.GetById(selectedKeyPoint.TourId);
            Present= tourService.FindPresentTouristsCount(tour.Id);
            Reserved = touristsList.Count;
            Capacity = touristsList.Count;
        }
        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Promene su sačuvane.");
            //this.Close();
        }
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            int i = 3;
            CheckBox checkBox = (CheckBox)sender;
            PeopleInfo peopleInfo = (PeopleInfo)checkBox.DataContext;
            if (SelectedKeyPoint != null)
            {
                i++;
                SelectedKeyPoint.PresentPeopleIds.Add(peopleInfo.Id);
                touristNotificationService.SendNotification(peopleInfo,SelectedKeyPoint);
                _keyPointRepository.Update(SelectedKeyPoint);
            }
            peopleInfo.Active = true;
            _peopleInfoService.Update(peopleInfo);
            Present = i;
            /*CheckBox checkBox = (CheckBox)sender;
            //Tourist tourist = (Tourist)checkBox.DataContext;
            PeopleInfo peopleInfo = (PeopleInfo)checkBox.DataContext;
            SelectedKeyPoint.PresentPeopleIds.Add(peopleInfo.Id);
            _keyPointRepository.Update(SelectedKeyPoint);
            //tourist.Active = true;
            peopleInfo.Active = true;
            _peopleInfoRepository.Update(peopleInfo);
            //_touristRepository.Update(tourist);*/
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }
        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            PeopleInfo peopleInfo = (PeopleInfo)checkBox.DataContext;

            if (SelectedKeyPoint != null)
            {
                SelectedKeyPoint.PresentPeopleIds.Remove(peopleInfo.Id);
                _keyPointRepository.Update(SelectedKeyPoint);
            }
            peopleInfo.Active = false;
            _peopleInfoService.Update(peopleInfo);

            /*CheckBox checkBox = (CheckBox)sender;
            // Tourist tourist = (Tourist)checkBox.DataContext;
            PeopleInfo peopleInfo = (PeopleInfo)checkBox.DataContext;
            SelectedKeyPoint.PresentPeopleIds.Remove(peopleInfo.Id);
            _keyPointRepository.Update(SelectedKeyPoint);
            peopleInfo.Active = false;
            _peopleInfoRepository.Update(peopleInfo);*/

        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
    }
}
