using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.ViewModel.TouristViewModel
{
    public class SavedTourRequestViewModel : ViewModelBase
    {

        public ViewModelCommandd GoHomeCommand { get; }
        public ViewModelCommandd TourRequestCommand {  get; }


        private readonly MainViewModel mainViewModel;


        public SavedTourRequestViewModel() {
            mainViewModel = LoggedInUser.mainViewModel;
            GoHomeCommand = new ViewModelCommandd(ExecuteGoHome);
            TourRequestCommand = new ViewModelCommandd(ExecuteTourRequest);
        }


        public void ExecuteGoHome(object obj)
        {

            mainViewModel.ExecuteShowTourCommand(obj);
        }


        public void ExecuteTourRequest(object obj)
        {

            mainViewModel.ExecuteTourRequestView(obj);
        }


    }
}
