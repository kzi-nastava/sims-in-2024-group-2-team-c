using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Service.TourServices;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModel.TouristViewModel
{
    public class RateTourViewModel : ViewModelBase
    {

        private int _knowledgeGrade;
        public int KnowledgeGrade
        {
            get { return _knowledgeGrade; }
            set
            {

                _knowledgeGrade = value;
                OnPropertyChanged(nameof(KnowledgeGrade));
                OnPropertyChanged(nameof(CanRate));


            }
        }


        private int _interestingGrade;
        public int InterestingGrade
        {
            get { return _interestingGrade; }
            set
            {

                _interestingGrade = value;
                OnPropertyChanged(nameof(InterestingGrade));
                OnPropertyChanged(nameof(CanRate));


            }
        }

        private string _comment;
        public string Comment
        {
            get { return _comment; }
            set
            {

                _comment = value;
                OnPropertyChanged(nameof(Comment));


            }
        }

        private List<string> _images;
        public List<string> Images
        {
            get { return _images; }
            set
            {

                _images = value;
                OnPropertyChanged(nameof(Images));


            }
        }


        public bool CanRate
        {
            get
            {
                // Check if all grades are set and not empty (greater than 0)
                return KnowledgeGrade > 0 && LanguageGrade > 0 && InterestingGrade > 0;
            }
        }


        private int _languageGrade;
        public int LanguageGrade
        {
            get { return _languageGrade; }
            set
            {

                _languageGrade = value;
                OnPropertyChanged(nameof(LanguageGrade));
                OnPropertyChanged(nameof(CanRate));


            }
        }

        private TourReviewService _tourReviewService;
        private FollowingTourDTO _selectedTour; 

        public ICommand RateCommand { get; }
        public ICommand AddPictureCommand { get; }

        public RateTourViewModel(FollowingTourDTO selectedTour)
        {
            
            _selectedTour = selectedTour; 
            _tourReviewService = new TourReviewService();
            RateCommand = new ViewModelCommandd(saveTheReview);
            AddPictureCommand = new ViewModelCommandd(OpenFileExplorer);

           
        }

        
        public void saveTheReview(object parameter)
        {

            TourRatingDTO tourRatingDTO = new TourRatingDTO(KnowledgeGrade, LanguageGrade, InterestingGrade, Comment ?? "", Images ?? new List<string>());
            _tourReviewService.SaveReviews(_selectedTour.TourInstanceId,3,LoggedInUser.Id, tourRatingDTO);


        }

       
        private List<string> _selectedFilePaths;
        public List<string> SelectedFilePaths
        {
            get { return _selectedFilePaths; }
            set
            {
                _selectedFilePaths = value;
                OnPropertyChanged(nameof(SelectedFilePaths));
            }
        }

       

        private void OpenFileExplorer(object parametar)
        {

            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Multiselect = true, // Allow multiple file selection
                Filter = "All Files|*.*|Text Files|*.txt|Image Files|*.jpg;*.png"
            };
            ShowDialog(openFileDialog);
        }

        private void ShowDialog(OpenFileDialog openFileDialog)
        {
            if (openFileDialog.ShowDialog() == true)
            {
                // Get the selected file paths
                Images = new List<string>(openFileDialog.FileNames);
                string selectedFiles = string.Join("\n", Images);

            }
        }
    }
}
