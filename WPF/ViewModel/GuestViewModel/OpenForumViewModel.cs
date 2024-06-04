using BookingApp.Service.AccommodationServices;
using BookingApp.WPF.View.GuestView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModel.GuestViewModel
{
    public class OpenForumViewModel : ViewModelBase
    {
        private readonly System.Windows.Navigation.NavigationService _navigationService;
        private readonly ForumService forumService;
        private readonly MainGuestWindow _mainGuestWindow;
        private string _location;
        private string _forumComment;

        public ICommand OpenForumCommand { get; set; }

        public string Location
        {
            get { return _location; }
            set
            {
                if (_location != value)
                {
                    _location = value;
                    OnPropertyChanged(nameof(Location));
                }
            }
        }

        public string ForumComment
        {
            get { return _forumComment; }
            set
            {
                if (_forumComment != value)
                {
                    _forumComment = value;
                    OnPropertyChanged(nameof(ForumComment));
                }
            }
        }


        public OpenForumViewModel(MainGuestWindow mainGuestWindow, System.Windows.Navigation.NavigationService navigationService)
        {
            _mainGuestWindow = mainGuestWindow;
            _navigationService = navigationService;
            forumService = new ForumService();
            OpenForumCommand = new ViewModelCommand<object>(SaveForum);

        }

        private void SaveForum(object obj)
        {
            forumService.SaveForum(Location, ForumComment);
        }
    }
}
