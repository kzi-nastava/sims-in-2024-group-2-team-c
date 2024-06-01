using BookingApp.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModel.TouristViewModel
{
    public class ComplexTourRequestViewModel : ViewModelBase
    {

        public ICommand GeneratePartsCommand { get; }
        


        private int? _numberOfParts;
        private ObservableCollection<PartViewModel> _parts;

        public int? NumberOfParts
        {
            get => _numberOfParts;
            set
            {
                _numberOfParts = value;
                OnPropertyChanged(nameof(NumberOfParts));
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

        public ComplexTourRequestViewModel()
        {
            Parts = new ObservableCollection<PartViewModel>();
            GeneratePartsCommand = new RelayCommand(GenerateParts);
        }

        private void GenerateParts()
        {
            Parts.Clear();
            for (int i = 1; i <= NumberOfParts; i++)
            {
                Parts.Add(new PartViewModel { PartNumber = i, Status = "Fill out" });
            }
        }


    }


    public class PartViewModel
    {
        public int PartNumber { get; set; }
        public string Status { get; set; }
    }
}
