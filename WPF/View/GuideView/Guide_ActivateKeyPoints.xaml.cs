using BookingApp.Injector;
using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.View;
using BookingApp.WPF.ViewModel.GuideViewModel;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookingApp.WPF.View.GuideView
{
    /// <summary>
    /// Interaction logic for Guide_ActivateKeyPoints.xaml
    /// </summary>
    public partial class Guide_ActivateKeyPoints : Page
    {
        private readonly KeyPointRepository _keyPointRepository;
        private readonly PeopleInfoRepository _peopleInfoRepository;
        public Guide_ActivateKeyPoints()
        {
            InitializeComponent();
            DataContext = new ActiveKeyPoint_ViewModel();
            _keyPointRepository = new KeyPointRepository();
            _peopleInfoRepository = new PeopleInfoRepository();
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

                PeopleInfo p = _peopleInfoRepository.GetById(id);
                peopleInfo.Add(p);
            }

            KeyPointsTuristsView touristsView = new KeyPointsTuristsView(peopleInfo, selectedKeyPoint);
            touristsView.Show();
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }
        private void HomePageButton_Click(object sender, RoutedEventArgs e)
        {
            Guide_HomePage home = new Guide_HomePage();
            this.NavigationService?.Navigate(home);
        }
    }
}
