using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Service.AccommodationServices;
using BookingApp.WPF.View.GuestView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;

namespace BookingApp.WPF.ViewModel.GuestViewModel
{
    public class ForumDetailsViewModel : ViewModelBase
    {
        private readonly ForumService _forumService;
        private ForumDTO selectedForum;
        private MainGuestWindow mainGuestWindow;
        private NavigationService navigationService;
        private ForumDTO _selectedForum;
        private string _location;
        private string _newComment;

        public ICommand SubmitNewForumCommentCommand { get; }

        public string NewComment
        {
            get { return _newComment; }
            set
            {
                _newComment = value;
                OnPropertyChanged(nameof(NewComment));
            }
        }

        public ForumDTO SelectedForum
        {
            get { return _selectedForum; }
            set
            {
                _selectedForum = value;
                OnPropertyChanged(nameof(SelectedForum));
            }
        }

        public string LocationDetails
        {
            get { return _location; }
            set
            {
                _location = value;
                OnPropertyChanged(nameof(LocationDetails));
            }
        }

        private ObservableCollection<ForumDTO> _forumComment;

        public ObservableCollection<ForumDTO> ForumComments
        {
            get { return _forumComment; }
            set
            {
                _forumComment = value;
                OnPropertyChanged(nameof(ForumComments));
            }
        }

        private void AddNewComment(ForumDTO newForum)
        {
            if (NewComment == null)
            {
                MessageBox.Show("Please type in something if you want to submit new comment.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            _forumService.AddNewComment(SelectedForum, NewComment);
        }


        public ForumDetailsViewModel(ForumDTO selectedForum, MainGuestWindow mainGuestWindow, NavigationService navigationService)
        {
            SelectedForum = selectedForum;
            LocationDetails = selectedForum.Location;
            ForumComments = new ObservableCollection<ForumDTO>();
            _forumService = new ForumService();
            this.mainGuestWindow = mainGuestWindow;
            this.navigationService = navigationService;
            SubmitNewForumCommentCommand = new ViewModelCommand<ForumDTO>(AddNewComment);

            LoadForumComments();
        }

        private void LoadForumComments()
        {
            try
            {
                var Forums = _forumService.GetAllForumComments(SelectedForum);
                ForumComments.Clear();
                foreach (var forum in Forums)
                {
                    ForumComments.Add(forum);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading reservations: {ex.Message}");
            }
        }



    }
}
