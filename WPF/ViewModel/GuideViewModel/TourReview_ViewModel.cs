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
                    OnPropertyChanged(nameof(Reviews));
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
                    OnPropertyChanged(nameof(Reviews));
                }
            }
        }

        public ICommand ReportReviewCommand { get; }
        //public ICommand BackCommand { get; }

        public TourReview_ViewModel()
        {
           // ReportReviewCommand = new Commands.RelayCommand(ReportReview);
            ReportReviewCommand = new ViewModelCommandd(ReportReview);
            //BackCommand = new ViewModelCommandd(BackClick);
            _tourReviewService = new TourReviewService();
            Reviews = new ObservableCollection<TourReviewDTO>();
            ReportMessageVisibility = Visibility.Hidden;
            UnableToReportVisibility = Visibility.Hidden;
            LoadReviews();
        }

       /* private void BackClick(object obj)
        {
            throw new NotImplementedException();
        }*/

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
                //UpdateVisibility();
                LoadReviews();
                //selectedReview.Reported = true;
                ReportMessageVisibility = Visibility.Visible;
                UnableToReportVisibility = Visibility.Hidden;
            }
            else
            {
                //UpdateVisibility();
                LoadReviews();
                ReportMessageVisibility = Visibility.Hidden;
                UnableToReportVisibility = Visibility.Visible;
            }
        }
        /*public void UpdateVisibility()
        {
            // Provera da li je poslednja recenzija prijavljena
            //if (Reviews.LastOrDefault()?.Reported == true)
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
        }*/
    }
}
