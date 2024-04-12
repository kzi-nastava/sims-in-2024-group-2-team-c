using BookingApp.Model;
using BookingApp.Repository;
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
    public partial class KeyPointsTuristsView : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<Tourist> _tourists;
        private readonly PeopleInfoRepository _peopleInfoRepository;
        public ObservableCollection<Tourist> Tourists
        {
            get { return _tourists; }
            set
            {
                _tourists = value;
                OnPropertyChanged();
            }
        }
        private readonly KeyPointRepository _keyPointRepository;
        private readonly TouristRepository _touristRepository;

        public KeyPointsTuristsView(List<PeopleInfo> touristsList)
        {
            InitializeComponent();
            DataContext = this;
            touristsListBox.IsEnabled = true;
            _keyPointRepository = new KeyPointRepository();
            _peopleInfoRepository = new PeopleInfoRepository();
            _touristRepository = new TouristRepository();
            Tourists = new ObservableCollection<Tourist>();
            touristsListBox.ItemsSource = touristsList;
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Promene su sačuvane.");
            Close();
        }
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            //Tourist tourist = (Tourist)checkBox.DataContext;
            PeopleInfo peopleInfo = (PeopleInfo)checkBox.DataContext;
            //tourist.Active = true;
            peopleInfo.Active = true;
            _peopleInfoRepository.Update(peopleInfo);
            //_touristRepository.Update(tourist);
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            // Tourist tourist = (Tourist)checkBox.DataContext;
            PeopleInfo peopleInfo = (PeopleInfo)checkBox.DataContext;
            peopleInfo.Active = false;
            _peopleInfoRepository.Update(peopleInfo);

        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
    }
}
