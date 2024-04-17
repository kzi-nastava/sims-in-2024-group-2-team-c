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


        public ViewModelCommandd FollowTourCommand { get;  }
        public ViewModelCommandd ShowKeyPointsCommand { get; }

        public ViewModelCommandd UserCommand {  get; }

        public ViewModelCommandd NotificationCommand { get; }

        public MainViewModel() {

            LoggedInUser.mainViewModel = this;
            FollowTourCommand = new ViewModelCommandd(ExecuteFollowTourCommand);
            //ShowKeyPointsCommand = new ViewModelCommand(ExecuteShowKeyPointsCommand);
            UserCommand = new ViewModelCommandd(ExecuteUserCommand);
            NotificationCommand = new ViewModelCommandd(ExecuteNotificationCommand);


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
         
            CurrentChildView = new FollowTourViewModel();
            
        }

        public void ExecuteUserCommand(object obj)
        {
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
