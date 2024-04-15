using BookingApp.Commands;
using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModel.GuideViewModel
{
    public class MainWindow_ViewModel : ViewModelBase
    {
        private string _currentPage;

        public string CurrentPage
        {
            get { return _currentPage; }
            set
            {
                _currentPage = value;
                OnPropertyChanged(nameof(CurrentPage));
            }
        }

        public ICommand NavigateToHomePageCommand { get; }
        public MainWindow_ViewModel() 
        {
            //
            //LoggedInUser.mainViewModel = this;
            NavigateToHomePageCommand = new RelayCommand(NavigateToHomePage);
            // Set default page
            CurrentPage = "Guide_HomePage.xaml";
        }
        private void NavigateToHomePage()
        {
            CurrentPage = "Guide_HomePage.xaml";
        }
    }
}
