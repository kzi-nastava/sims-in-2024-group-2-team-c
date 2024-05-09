using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Service.TourServices;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModel.TouristViewModel
{
    public class RateTourViewModel : ViewModelBase
    {

        private int? _knowledgeGrade;
        public int? KnowledgeGrade
        {
            get { return _knowledgeGrade; }
            set
            {

                _knowledgeGrade = value;
                OnPropertyChanged(nameof(KnowledgeGrade));
                OnPropertyChanged(nameof(CanRate));


            }
        }


        private int? _interestingGrade;
        public int? InterestingGrade
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

        private List<string> _images = new List<string>();
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
                return KnowledgeGrade > 0 && LanguageGrade > 0 && InterestingGrade > 0 &&
            IsValid(KnowledgeGrade, LanguageGrade, InterestingGrade);
            }
        }


        private int? _languageGrade;
        public int? LanguageGrade
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



        /*private void OpenFileExplorer(object parametar)
        {

            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Multiselect = true, // Allow multiple file selection
                Filter = "All Files|*.*|Text Files|*.txt|Image Files|*.jpg;*.png"
            };
            ShowDialog(openFileDialog);
        }*/

        /* private void OpenFileExplorer(object parameter)
         {
             OpenFileDialog openFileDialog = new OpenFileDialog
             {
                 Multiselect = true, // Allow multiple file selection
                 Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif" // Filter for image files
             };

             if (openFileDialog.ShowDialog() == true)
             {
                 // Get the selected file paths
                 Images = new List<string>(openFileDialog.FileNames);
             }
         }*/


        /* private void OpenFileExplorer(object parameter)
         {
             OpenFileDialog openFileDialog = new OpenFileDialog
             {
                 Multiselect = true, // Allow multiple file selection
                 Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif" // Filter for image files
             };

             if (openFileDialog.ShowDialog() == true)
             {
                 // Get the selected file paths
                 string destinationFolder = "Resources/Images";
                 Directory.CreateDirectory(destinationFolder); // Create the folder if it doesn't exist

                 foreach (string filePath in openFileDialog.FileNames)
                 {
                     string fileName = Path.GetFileName(filePath);
                     string destinationPath = Path.Combine(destinationFolder, fileName);

                     // Copy the file to the destination folder
                     File.Copy(filePath, destinationPath, true);

                     // Add the destination path to the Images list
                     Images.Add(destinationPath);
                 }
             }
         }*/


        /*  private void OpenFileExplorer(object parametar)
          {
              OpenFileDialog openFileDialog = new OpenFileDialog
              {
                  Multiselect = true, // Allow multiple file selection
                  Filter = "All Files|*.*|Image Files|*.jpg;*.png"
              };

              if (openFileDialog.ShowDialog() == true)
              {
                  // Get the selected file paths
                  foreach (var filePath in openFileDialog.FileNames)
                  {
                      // Get the file name without the full path
                      string fileName = System.IO.Path.GetFileName(filePath);

                      // Construct the destination path
                      string destinationPath = "Resources/Images/" + fileName;

                      try
                      {
                          // Copy the file to the destination folder
                          System.IO.File.Copy(filePath, destinationPath);

                          // Add the file name to the list of images
                          Images.Add(fileName);
                      }
                      catch (Exception ex)
                      {
                          // Handle any exceptions, such as file already exists, permission denied, etc.
                          Console.WriteLine("An error occurred while copying the file: " + ex.Message);
                      }
                  }

                  // Notify property change for the Images property
                  OnPropertyChanged(nameof(Images));
              }
          }*/

       private void OpenFileExplorer(object parameter)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Multiselect = true, // Allow multiple file selection
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif" // Filter for image files
            };

            if (openFileDialog.ShowDialog() == true)
            {
                // Get the selected file paths
                string destinationFolder = "D:/Mila/AHHHHHHHHHHHH/sims-in-2024-group-2-team-c/Resources/Images/";
                Directory.CreateDirectory(destinationFolder); // Create the folder if it doesn't exist

               

                foreach (string filePath in openFileDialog.FileNames)
                {
                    string fileName = Path.GetFileName(filePath);
                    string destinationPath = Path.Combine(destinationFolder, fileName);

                    // Copy the file to the destination folder
                    File.Copy(filePath, destinationPath, true);

                    // Add just the file name to the Images list
                    Images.Add(fileName);
                }
            }
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

        private bool IsValid(params int?[] grades)
        {
            // Check if all provided grades are within the valid range
            foreach (var grade in grades)
            {
                if ( grade < 1 || grade > 5)
                {
                    return false;
                }
            }
            return true;
        }



    }
}
