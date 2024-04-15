using BookingApp.DTO;
using BookingApp.Model;
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
      
        
        private FollowingTourDTO _selectedTour; // Store the selected tour

        public ICommand RateCommand { get; }
        public ICommand AddPictureCommand { get; }

        public RateTourViewModel(FollowingTourDTO selectedTour)
        {
            //_selectedTour = selectedTour;
            
            _selectedTour = selectedTour; // Save the selected tour for future use
            _tourReviewService = new TourReviewService();
            RateCommand = new ViewModelCommand(saveTheReview);
            AddPictureCommand = new ViewModelCommand(OpenFileExplorer);

           
        }

        // Properties for data binding
       




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
        
        public void saveTheReview(object parameter)
        {

            

            TourRatingDTO tourRatingDTO = new TourRatingDTO(KnowledgeGrade, LanguageGrade, InterestingGrade, Comment ?? "", Images ?? new List<string>());
            _tourReviewService.SaveReviews(_selectedTour.TourInstanceId,3,LoggedInUser.Id, tourRatingDTO);


        }

        /* public void saveTheReview(TourRatingDTO _tourRating, FollowingTourDTO selectedTour)
         {
             // Check if any of the parameters are null
             if (_tourRating == null)
             {
                 throw new ArgumentNullException(nameof(_tourRating), "Tour rating cannot be null.");
             }
             if (selectedTour == null)
             {
                 throw new ArgumentNullException(nameof(selectedTour), "Selected tour cannot be null.");
             }

             // Initialize TourReviewService if it is null
             if (_tourReviewService == null)
             {
                 _tourReviewService = new TourReviewService();
             }

             // Assuming LoggedInUser.Id is not null
             // Ensure LoggedInUser.Id is properly initialized
             if ( LoggedInUser.Id <= 0)
             {
                 throw new InvalidOperationException("Invalid logged-in user ID.");
             }

             // Call the service method only if the required parameters are valid
             _tourReviewService.SaveReviews(
                 selectedTour.TourInstanceId,
                 3, // guideId, you may need to fetch it from somewhere else
                 LoggedInUser.Id,
                 KnowledgeGrade,
                 LanguageGrade,
                 InterestingGrade,
                 Comment,
                 Images ?? new List<string>()  // Ensure images is not null
             );
         }*/
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

        /* private string _selectedFilePath;
         public string SelectedFilePath
         {
             get { return _selectedFilePath; }
             set
             {
                 _selectedFilePath = value;
                 OnPropertyChanged(nameof(SelectedFilePath));
             }
         }
          private void OpenFileExplorer(object parametar)
          {
              // Create a new instance of OpenFileDialog
              OpenFileDialog openFileDialog = new OpenFileDialog();

              // Set the filter for file types (optional)
              openFileDialog.Filter = "All Files|*.*|Text Files|*.txt|Image Files|*.jpg;*.png";

              // Show the dialog and check if the user selected a file
              if (openFileDialog.ShowDialog() == true)
              {
                  // Get the selected file path
                  SelectedFilePath = openFileDialog.FileName;

                  // Handle the selected file path (e.g., perform operations on the file path)
                  // You can now use SelectedFilePath as needed in your application
                  // For demonstration, let's show a message box with the selected file path
                  MessageBox.Show("Selected file: " + SelectedFilePath);
              }
          }*/

        private void OpenFileExplorer(object parametar)
        {
            // Create a new instance of OpenFileDialog
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Multiselect = true, // Allow multiple file selection
                Filter = "All Files|*.*|Text Files|*.txt|Image Files|*.jpg;*.png"
            };

            // Show the dialog and check if the user selected files
            if (openFileDialog.ShowDialog() == true)
            {
                // Get the selected file paths
                Images = new List<string>(openFileDialog.FileNames);

                // Handle the selected file paths (e.g., perform operations on the file paths)
                // You can now use SelectedFilePaths as needed in your application
                // For demonstration, let's show a message box with the selected file paths
                string selectedFiles = string.Join("\n", Images);
                
            }
        }



    }
}
