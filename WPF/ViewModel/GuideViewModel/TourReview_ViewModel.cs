using BookingApp.Commands;
using BookingApp.DTO;
using BookingApp.Repository;
using BookingApp.Service.TourServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModel.GuideViewModel
{
    class TourReview_ViewModel : ViewModelBase
    {
        private readonly TourReviewService _tourReviewService;
        private ObservableCollection<TourReviewDTO> reviews;
        public ObservableCollection<TourReviewDTO> Reviews
        {
            get => reviews;
            set
            {
                reviews = value;
                OnPropertyChanged(nameof(Reviews));
            }
        }
        private Visibility _reportMessageVisibility;
        public Visibility ReportMessageVisibility
        {
            get { return _reportMessageVisibility; }
            set
            {
                if (_reportMessageVisibility != value)
                {
                    _reportMessageVisibility = value;
                    OnPropertyChanged(nameof(ReportMessageVisibility));
                }
            }
        }

        private Visibility _unableToReportVisibility;
        public Visibility UnableToReportVisibility
        {
            get { return _unableToReportVisibility; }
            set
            {
                if (_unableToReportVisibility != value)
                {
                    _unableToReportVisibility = value;
                    OnPropertyChanged(nameof(UnableToReportVisibility));
                }
            }
        }

        public ICommand ReportReviewCommand { get; }

        public TourReview_ViewModel()
        {
           // ReportReviewCommand = new Commands.RelayCommand(ReportReview);
            ReportReviewCommand = new ViewModelCommandd(ReportReview);
            _tourReviewService = new TourReviewService();
            Reviews = new ObservableCollection<TourReviewDTO>();
            LoadReviews();
        }

       /* private void ReportReview()
        {
            //if (selectedReview == null || !(selectedReview is TourReviewDTO review))
            //    return;
            TourReviewDTO selectedReview = (sender as FrameworkElement).DataContext as TourReviewDTO;
            bool canReport = _tourReviewService.ReportReview((TourReviewDTO)selectedReview);
            if (canReport)
            {
                review.Reported = true;
                UpdateVisibility();
            }
            else
            {
                UpdateVisibility();
            }
        }*/
        private void LoadReviews()
        {
            var ReviewView = _tourReviewService.GetReviewDTOs();
            Reviews = new ObservableCollection<TourReviewDTO>(ReviewView);
            ReportMessageVisibility = Visibility.Hidden;
            UnableToReportVisibility = Visibility.Hidden;
        }

        private void ReportReview(object selectedReview)
        {
            //TourReviewDTO selectedReview = (sender as FrameworkElement).DataContext as TourReviewDTO;
            if (selectedReview == null || !(selectedReview is TourReviewDTO review))
                return;
            bool canReport = _tourReviewService.ReportReview((TourReviewDTO)selectedReview);
            if (canReport)
            {
                review.Reported = true;
                selectedReview = review;
                UpdateVisibility();
                //selectedReview.Reported = true;
                //ReportMessageTextBlock.Visibility = Visibility.Visible;
                //UnableToReportTextBox.Visibility = Visibility.Hidden;
                // Ovde možete dodati dodatne radnje kao što su ažuriranje baze podataka,
                // prikazivanje poruke o uspešnom prijavljivanju, ažuriranje prikaza recenzija itd.
            }
            else
            {
                UpdateVisibility();
                //ReportMessageTextBlock.Visibility = Visibility.Hidden;
                //UnableToReportTextBox.Visibility = Visibility.Visible;
                // Ako je recenzija već prijavljena, možete izvršiti odgovarajuće akcije, kao što je
                // prikazivanje poruke o nemogućnosti ponovnog prijavljivanja
            }
        }
        public void UpdateVisibility()
        {
            // Provera da li je poslednja recenzija prijavljena
            if (Reviews.LastOrDefault()?.Reported == true)
            {
                ReportMessageVisibility = Visibility.Visible;
                UnableToReportVisibility = Visibility.Hidden;
            }
            else
            {
                ReportMessageVisibility = Visibility.Hidden;
                UnableToReportVisibility = Visibility.Visible;
            }
        }
    }
}
