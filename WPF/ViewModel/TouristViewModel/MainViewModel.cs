using BookingApp.Model;
using BookingApp.WPF.View.TouristView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Navigation;

namespace BookingApp.WPF.ViewModel.TouristViewModel
{
    public class MainViewModel : ViewModelBase
    {

        private ViewModelBase _currentChildView;
        public ViewModelBase CurrentChildView
        {
            get
            {
                return _currentChildView;
            }

            set
            {
                _currentChildView = value;
                OnPropertyChanged(nameof(CurrentChildView));
            }
        }


        public ViewModelCommand FollowTourCommand { get;  }
        public ViewModelCommand ShowKeyPointsCommand { get; }

        public ViewModelCommand UserCommand {  get; }

        public ViewModelCommand NotificationCommand { get; }

        public MainViewModel() {

            LoggedInUser.mainViewModel = this;
            FollowTourCommand = new ViewModelCommand(ExecuteFollowTourCommand);
            //ShowKeyPointsCommand = new ViewModelCommand(ExecuteShowKeyPointsCommand);
            UserCommand = new ViewModelCommand(ExecuteUserCommand);
            NotificationCommand = new ViewModelCommand(ExecuteNotificationCommand);


        }

        public void ExecuteNotificationCommand(object obj)
        {
            /* if (obj is MainTouristView mainTouristView)
             {
                 mainTouristView.MainFrame.Navigate(new FollowTourView());
             }*/

            CurrentChildView = new NotificationViewModel();

        }

        public void ExecuteFollowTourCommand(object obj)
        {
            /* if (obj is MainTouristView mainTouristView)
             {
                 mainTouristView.MainFrame.Navigate(new FollowTourView());
             }*/

            CurrentChildView = new FollowTourViewModel();
            
        }

        public void ExecuteUserCommand(object obj)
        {
            /* if (obj is MainTouristView mainTouristView)
             {
                 mainTouristView.MainFrame.Navigate(new FollowTourView());
             }*/

            CurrentChildView = new TouristUserViewModel();

        }

        /*public void ExecuteShowKeyPointsCommand(object obj)
        {
            /*if (obj is MainTouristView mainTouristView)
            {
                mainTouristView.MainFrame.Navigate(new FollowKeyPointsView());
            }
            CurrentChildView = new FollowKeyPointsViewModel();
        }
    */

    }
}
