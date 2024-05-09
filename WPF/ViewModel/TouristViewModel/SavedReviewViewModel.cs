using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModel.TouristViewModel
{
    public class SavedReviewViewModel : ViewModelBase
    {

        public ViewModelCommandd GoHomeCommand { get;}
        private readonly MainViewModel mainViewModel;

        public SavedReviewViewModel() {
           mainViewModel = LoggedInUser.mainViewModel;
            GoHomeCommand = new ViewModelCommandd(ExecuteGoHome);
        }

        public void ExecuteGoHome(object obj)
        {
            
            mainViewModel.ExecuteShowTourCommand(obj);
        }



    }
}
