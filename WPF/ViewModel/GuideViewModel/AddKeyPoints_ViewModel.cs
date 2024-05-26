using BookingApp.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using BookingApp.Model;
using BookingApp.Service.TourServices;

namespace BookingApp.WPF.ViewModel.GuideViewModel
{
    public class AddKeyPoints_ViewModel : ViewModelBase
    {

        private int _selectedNumberOfPoints;
        public int SelectedNumberOfPoints
        {
            get { return _selectedNumberOfPoints; }
            set
            {
                _selectedNumberOfPoints = value;
                OnPropertyChanged(nameof(SelectedNumberOfPoints));
            }
        }

        private Visibility _isInserted;
        public Visibility isInserted
        {
            get { return _isInserted; }
            set
            {
                _isInserted = value;
                OnPropertyChanged(nameof(isInserted));
            }
        }

        public ObservableCollection<UIElement> GeneratedElements { get; set; }
        private List<KeyPoint> _keyPoints;
        public List<KeyPoint> KeyPoints
        {
            get { return _keyPoints; }
            set
            {
                _keyPoints = value;
                OnPropertyChanged(nameof(KeyPoints));
            }
        }

        public ICommand InsertCommand { get; set; }
        public ICommand SaveTourCommand { get; set; }

        public AddKeyPoints_ViewModel()
        {
            GeneratedElements = new ObservableCollection<UIElement>();
            InsertCommand = new ViewModelCommandd(InsertPoints);
            SaveTourCommand = new ViewModelCommandd(SavePoints);
            isInserted = Visibility.Hidden;
            //_keyPoints = new List<KeyPoint>();
        }

        private void InsertPoints(object parameter)
        {
            GeneratedElements.Clear();
            //_keyPoints.Clear();

            for (int i = 0; i < SelectedNumberOfPoints; i++)
            {
                StackPanel stackPanel = new StackPanel();
                stackPanel.Orientation = Orientation.Horizontal;
                stackPanel.Margin = new Thickness(10);

                TextBox textBox = new TextBox() { Width = 100 };
                stackPanel.Children.Add(textBox);

                GeneratedElements.Add(stackPanel);

                // Dodajemo novu tačku u listu tačaka
                _keyPoints.Add(new KeyPoint() { Name = textBox.Text, Description = $"Key Point {i + 1}" });
            }

            isInserted = Visibility.Visible;
        }

        private void SavePoints(object parameter)
        {
            foreach (var kp in _keyPoints)
            {
                kp.StartingPoint = false;
                kp.EndingPoint = false;
            }
            /*if (_keyPoints.Count > 0)
            {
                _keyPoints.First().StartingPoint = true;
                _keyPoints.Last().EndingPoint = true;
            }*/

            KeyPointService keyPointService = new KeyPointService();
            foreach (var kp in _keyPoints)
            {
                keyPointService.SaveKeyPoint(kp.Name, kp.StartingPoint, kp.EndingPoint);
            }
        }
    }
}
