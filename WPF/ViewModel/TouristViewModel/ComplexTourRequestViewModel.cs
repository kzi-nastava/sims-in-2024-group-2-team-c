using BookingApp.Commands;
using BookingApp.Model;
using BookingApp.Service.TourServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModel.TouristViewModel
{
    public class ComplexTourRequestViewModel : ViewModelBase
    {

        public ICommand GeneratePartsCommand { get; }
        public ICommand FillOutCommand { get; }
        public ICommand CreateCommand { get; }

        private readonly MainViewModel _mainViewModel;

        private readonly ComplexTourRequestService _complexTourRequestService;


        private int? _numberOfParts;
        private ObservableCollection<PartViewModel> _parts;

        public int? NumberOfParts
        {
            get => _numberOfParts;
            set
            {
                _numberOfParts = value;
                OnPropertyChanged(nameof(NumberOfParts));
                CheckToCreate();
            }
        }

        public ObservableCollection<PartViewModel> Parts
        {
            get => _parts;
            set
            {
                _parts = value;
                OnPropertyChanged(nameof(Parts));
            }
        }


        private bool _isCreatable ;
        public bool IsCreatable
        {
            get { return _isCreatable; }
            set { _isCreatable = value; 
            OnPropertyChanged(nameof(IsCreatable));}
        }


        private string _status;

        public string Status
        {
            get => _status;
            set
            {
                _status = value;
                OnPropertyChanged(nameof(Status));
                
            }
        }



        public ComplexTourRequestViewModel()
        {
            _mainViewModel = LoggedInUser.mainViewModel;
            Parts = _mainViewModel.CurrentParts ?? new ObservableCollection<PartViewModel>();
            GeneratePartsCommand = new ViewModelCommandd(GenerateParts);
            FillOutCommand = new ViewModelCommand<PartViewModel>(ExecuteRequestCreation);
            CreateCommand = new ViewModelCommandd(CreateRequest);
            IsCreatable = false;
            CheckToCreate();

            _complexTourRequestService = new ComplexTourRequestService();
        }

        private void CheckToCreate()
        {
            if (Parts.Any())
            {
                int counter = 0;
                foreach (PartViewModel part in Parts)
                {
                    if (part.Status == "Fill out")
                    {
                        counter += 1;
                    }

                }

                if (counter == 0)
                {
                    IsCreatable = true;
                }
                else
                {
                    IsCreatable = false;
                }

                OnPropertyChanged(nameof(IsCreatable));
            }
        }

        public void CreateRequest(object obj)
        {

            _complexTourRequestService.CreateComplexTour(Parts.ToList());
            _mainViewModel.ExecuteSavedComplexTourRequest();
        }

        private void ExecuteRequestCreation(PartViewModel part)
        {
            _mainViewModel.ExecuteComplexRequestCreation(part,this);

        }

        private void GenerateParts(object obj)
        {
            
            Parts.Clear();
            for (int i = 1; i <= NumberOfParts; i++)
            {
                Parts.Add(new PartViewModel { PartNumber = i, Status = "Fill out", IsEnabled = true });
            }
            CheckToCreate();
        }


    }


    public class PartViewModel : ViewModelBase
    {
        public int PartNumber { get; set; }
        public string Status { get; set; }
        
        public int RequestId { get; set; }

        public bool IsEnabled { get; set; }


    }
}
