using BookingApp.Model;
using BookingApp.Service.TourServices;
using BookingApp.WPF.ViewModel.TouristViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModel.GuideViewModel
{
    public class QuitJob_ViewModel : ViewModelBase
    {
        private readonly MainWindow_ViewModel _mainViewModel;

        private FutureToursService futureToursService;
        private TourVoucherService tourVoucherService;
        public ICommand QuitCommand { get; set; }
        //public ICommand BackCommand { get; set; }
        public QuitJob_ViewModel() 
        {
            QuitCommand = new ViewModelCommandd(Quit);
            //BackCommand = new ViewModelCommandd(Back);
            _mainViewModel = LoggedInUser.mainGuideViewModel;
            futureToursService = new FutureToursService();
            tourVoucherService = new TourVoucherService();
        }

        private void Quit(object obj)
        {
            int guideId = LoggedInUser.Id;
            futureToursService.CancelToursByGuide(guideId);
            //tourVoucherService.UpdateVouchersForGuide(guideId);
        }
    }
}
