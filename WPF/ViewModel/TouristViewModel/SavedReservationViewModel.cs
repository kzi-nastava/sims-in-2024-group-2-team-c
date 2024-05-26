using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.ViewModel.TouristViewModel
{
    public class SavedReservationViewModel : ViewModelBase
    {
        private readonly MainViewModel mainViewModel;
        public ViewModelCommandd GoHomeCommand { get; }
        public ViewModelCommandd TourCommand { get; }

        public SavedReservationViewModel() {
            mainViewModel = LoggedInUser.mainViewModel;
            GoHomeCommand = new ViewModelCommandd(ExecuteGoHome);
            TourCommand = new ViewModelCommandd(ExecuteFollowTourCommand);
        }

        private void ExecuteGoHome(object obj)
        {
            mainViewModel.ExecuteShowTourCommand(obj);
            }


        private void ExecuteFollowTourCommand(object obj)
        {
            mainViewModel.ExecuteFollowTourCommand(obj);
        }

    }
}
