using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.ViewModel.TouristViewModel
{
    public class SavedComplexTourRequestViewModel : ViewModelBase
    {

        private readonly MainViewModel mainViewModel;
        public ViewModelCommandd GoHomeCommand { get; }
        public ViewModelCommandd TourCommand { get; }

        public SavedComplexTourRequestViewModel()
        {
            mainViewModel = LoggedInUser.mainViewModel;
            GoHomeCommand = new ViewModelCommandd(ExecuteGoHome);
            TourCommand = new ViewModelCommandd(ExecuteSeeComplexTours);
        }


        private void ExecuteSeeComplexTours(object obj)
        {
            mainViewModel.ExecuteSeeComplexTours();
        }

        private void ExecuteGoHome(object obj)
        {
            mainViewModel.ExecuteShowTourCommand(obj);
        }

    }
}
