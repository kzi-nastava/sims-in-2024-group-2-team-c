using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Service.TourServices;
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

namespace BookingApp.WPF.View.GuideView
{
    /// <summary>
    /// Interaction logic for ReviewsOverview.xaml
    /// </summary>
    public partial class ReviewsOverview : Window
    {
        private readonly TourReviewService _tourReviewService;
        private ObservableCollection<TourReviewDTO> _reviews;
        public ObservableCollection<TourReviewDTO> Reviews
        {
            get { return _reviews; }
            set
            {
                if (_reviews != value)
                {
                    _reviews = value;
                    OnPropertyChanged();
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ReviewsOverview()
        {
            InitializeComponent();
            DataContext = this;
            _tourReviewService = new TourReviewService(new TourReviewsRepository());
            Reviews = new ObservableCollection<TourReviewDTO>();
            LoadReviews();
        }
        void LoadReviews()
        {
            var ReviewView= _tourReviewService.GetReviewDTOs();
            Reviews = new ObservableCollection<TourReviewDTO>(ReviewView);
        }

        private void ReportButton_Click(object sender, RoutedEventArgs e)
        {
            TourReviewDTO selectedReview = (sender as FrameworkElement).DataContext as TourReviewDTO ;
            bool canReport = _tourReviewService.ReportReview(selectedReview);

            if (canReport)
            {
                selectedReview.Reported = true;
                ReportMessageTextBlock.Visibility = Visibility.Visible;
                UnableToReportTextBox.Visibility = Visibility.Hidden;
            }
            else
            {
                ReportMessageTextBlock.Visibility = Visibility.Hidden;
                UnableToReportTextBox.Visibility = Visibility.Visible;
            }
            //Reviews.Refresh();
            reviewsList.Items.Refresh();
        }
    }
}
