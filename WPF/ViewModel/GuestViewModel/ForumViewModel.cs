using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Service.AccommodationServices;
using BookingApp.Service.OwnerService;
using BookingApp.WPF.View.GuestView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModel.GuestViewModel
{
    public class ForumViewModel : ViewModelBase
    {

        private readonly System.Windows.Navigation.NavigationService _navigationService;
        private readonly MainGuestWindow _mainGuestWindow;
        private readonly ForumService _forumService;
        private readonly OwnerNotificationService _ownerNotificationService;

        public ICommand OpenForumPageCommand { get; }

        public ICommand CloseSelectedForumCommand => new ViewModelCommand<object>(CloseForum);

        public ICommand ForumDoubleClickCommand { get; }

        private ObservableCollection<ForumDTO> _myForums;

        public ObservableCollection<ForumDTO> MyForums
        {
            get { return _myForums; }
            set
            {
                _myForums = value;
                OnPropertyChanged(nameof(MyForums));
            }
        }

        private ObservableCollection<ForumDTO> _otherForums;
        public ObservableCollection<ForumDTO> OtherForums
        {
            get { return _otherForums; }
            set
            {
                _otherForums = value;
                OnPropertyChanged(nameof(OtherForums));
            }
        }

        public ForumViewModel(MainGuestWindow mainGuestWindow, System.Windows.Navigation.NavigationService navigationService)
        {
            _mainGuestWindow = mainGuestWindow;
            _navigationService = navigationService;
            _forumService = new ForumService();
            MyForums = new ObservableCollection<ForumDTO>();
            OtherForums = new ObservableCollection<ForumDTO>();
            OpenForumPageCommand = new ViewModelCommand<object>(OpenForum);
            ForumDoubleClickCommand = new ViewModelCommand<object>(ForumDoubleClick);
            _ownerNotificationService = new OwnerNotificationService();

            LoadMyForums();
            LoadOtherForums();
        }

        private void LoadOtherForums()
        {
            try
            {
                var otherForums = _forumService.GetAllOtherForums(LoggedInUser.Id);
                OtherForums.Clear();
                foreach (var forum in otherForums)
                {
                    OtherForums.Add(forum);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading reservations: {ex.Message}");
            }
        }

        private void LoadMyForums()
        {
            try
            {
                var myForums = _forumService.GetAllMyForums(LoggedInUser.Id);
                MyForums.Clear();
                foreach (var forum in myForums)
                {
                    MyForums.Add(forum);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading reservations: {ex.Message}");
            }
        }

        private void OpenForum(object parameter)
        {
            if (_mainGuestWindow != null)
            {
                _mainGuestWindow.ChangeHeaderText("Open new forum");
                _navigationService?.Navigate(new OpenForumView(_mainGuestWindow, _navigationService));
            }
        }

        private void CloseForum(object selectedForumObj)
        {
            try
            {
                if (!(selectedForumObj is ForumDTO selectedForum))
                {
                    MessageBox.Show("Please select a forum to close from the list 'My Forums'.");
                    return;
                }
                int forumId = selectedForum.Id;
                string result = _forumService.CloseForum(forumId);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error cancelling reservation: {ex.Message}");
            }
        }

        private void ForumDoubleClick(object parameter)
        {
            if (parameter is ForumDTO selectedForum)
            {
                _mainGuestWindow.ChangeHeaderText("Forum");
                _navigationService?.Navigate(new ForumDetailsView(selectedForum, _mainGuestWindow, _navigationService));
            }
        }

    }
}
