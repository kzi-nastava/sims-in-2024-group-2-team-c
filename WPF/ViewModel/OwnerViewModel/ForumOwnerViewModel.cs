using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Service;
using BookingApp.Service.AccommodationServices;

namespace BookingApp.WPF.ViewModel.OwnerViewModel
{
    public class OwnerForumViewModel : INotifyPropertyChanged
    {
        private readonly ForumService _forumService;
        private ForumDTO _selectedForum;
        private ObservableCollection<ForumDTO> _comments;
        private string _newComment;
        private ObservableCollection<ForumDTO> _otherForums;

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ForumDTO SelectedForum
        {
            get { return _selectedForum; }
            set
            {
                _selectedForum = value;
                OnPropertyChanged(nameof(SelectedForum));
                LoadComments();
            }
        }

        public ObservableCollection<ForumDTO> Comments
        {
            get { return _comments; }
            set
            {
                _comments = value;
                OnPropertyChanged(nameof(Comments));
            }
        }

        public string NewComment
        {
            get { return _newComment; }
            set
            {
                _newComment = value;
                OnPropertyChanged(nameof(NewComment));
            }
        }

        public ObservableCollection<ForumDTO> OtherForums
        {
            get { return _otherForums; }
            set
            {
                _otherForums = value;
                OnPropertyChanged(nameof(OtherForums));
            }
        }

        public OwnerForumViewModel()
        {
            _forumService = new ForumService();
            OtherForums = new ObservableCollection<ForumDTO>();
            Comments = new ObservableCollection<ForumDTO>();
            LoadOtherForums();
        }

        private void LoadComments()
        {
            if (SelectedForum == null) return;

            try
            {
                var comments = _forumService.GetAllForumComments(SelectedForum);
                Comments.Clear();
                foreach (var comment in comments)
                {
                    Comments.Add(comment);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading comments: {ex.Message}");
            }
        }

        private void LoadOtherForums()
        {
            try
            {
                //var otherForums = _forumService.GetAllOtherForums(LoggedInUser.Id);
                var otherForums = _forumService.GetAllOtherForumsForOwner(LoggedInUser.Id);
                OtherForums.Clear();
                foreach (var forum in otherForums)
                {
                    OtherForums.Add(forum);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading other forums: {ex.Message}");
            }
        }

        public void Forum_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                SelectedForum = e.AddedItems[0] as ForumDTO;
            }
        }

        public void AddCommentButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedForum == null || string.IsNullOrWhiteSpace(NewComment)) return;

            try
            {
                _forumService.AddNewCommentOwner(SelectedForum, NewComment);
                LoadComments();
                NewComment = string.Empty;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding comment: {ex.Message}");
            }
        }
    }
}
