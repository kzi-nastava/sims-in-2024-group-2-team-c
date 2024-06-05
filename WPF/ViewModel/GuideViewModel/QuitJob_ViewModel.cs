using BookingApp.Model;
using BookingApp.Service.TourServices;
using BookingApp.View;
using BookingApp.WPF.View.GuideView;
using BookingApp.WPF.ViewModel.TouristViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModel.GuideViewModel
{
    public class QuitJob_ViewModel : ViewModelBase
    {
        private readonly MainWindow_ViewModel _mainViewModel;
       // private readonly Window _window;
        private FutureToursService futureToursService;
        private TourVoucherService tourVoucherService;
        private GuideService guideService;
        public ICommand QuitCommand { get; set; }
        //public ICommand BackCommand { get; set; }
        public QuitJob_ViewModel() 
        {
            QuitCommand = new ViewModelCommandd(Quit);
            //BackCommand = new ViewModelCommandd(Back);
            //_window = window ?? throw new ArgumentNullException(nameof(window));
            _mainViewModel = LoggedInUser.mainGuideViewModel;
            futureToursService = new FutureToursService();
            tourVoucherService = new TourVoucherService();
            guideService = new GuideService();
        }

        private void Quit(object obj)
        {
            int guideId = LoggedInUser.Id;
            //futureToursService.CancelToursByGuide(guideId);
            futureToursService.CancelToursByGuide(LoggedInUser.Username);
            //MessageBox.Show("Uspesno ste dali otkaz");
            MessageBoxResult result = MessageBox.Show("Da li želite da nastavite?", "Potvrda", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                Guide guide = guideService.GetByUserName(LoggedInUser.Username);
                guide.resigned = true;
                guide = guideService.Update(guide);
                //Window parentWindow = Window.GetWindow(this);
                //parentWindow.Close();
                //_window.Close();
                SignInForm signInForm = new SignInForm();
                signInForm.Show();
            }
            else
            {
                // Korisnik je kliknuo No ili zatvorio prozor
            }
            //logika za brisanje instanci, i korisnika
        }
    }
}
