using BookingApp.DTO;
using BookingApp.Service.TourServices;
using BookingApp.WPF.View.TouristView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.IO;


namespace BookingApp.WPF.ViewModel.TouristViewModel
{
    public class TouristHomeViewModel : ViewModelBase
    {
        public ICommand ViewTourCommand { get;  }
        private readonly TourService _tourService;


      


        private ObservableCollection<HomeTourDTO> _tours;

        public ObservableCollection<HomeTourDTO> Tours
        { 
            get { return _tours; }
            set
            {
                _tours = value;
                OnPropertyChanged(nameof(Tours));
            }

        }


        public TouristHomeViewModel() {

            _tourService = new TourService();
            Tours = new ObservableCollection<HomeTourDTO>(_tourService.GetAllTourDTOs());

           

            // Load images for all tours
           




        }

        


    }
}
