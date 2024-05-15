using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Service.AccommodationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml.Linq;

namespace BookingApp.WPF.ViewModel.GuestViewModel
{
    public class AccommodationRenovateViewModel : ViewModelBase
    {
        private readonly AccommodationRateService _service;
        public ICommand SubmitAccommodationRenovateCommand { get; }
        private string _whatToRenovate;

        private AccommodationRate rate;
        public AccommodationRate Rate
        {
            get { return rate; }
            set
            {
                if (rate != value)
                {
                    rate = value;
                    OnPropertyChanged(nameof(Rate));
                }
            }
        }

        public string WhatToRenovate
        {
            get { return _whatToRenovate; }
            set
            {
                if (_whatToRenovate != value)
                {
                    _whatToRenovate = value;
                    OnPropertyChanged(nameof(WhatToRenovate));
                }
            }
        }

        private bool _isLevel1Checked;
        public bool IsLevel1Checked
        {
            get { return _isLevel1Checked; }
            set
            {
                if (_isLevel1Checked != value)
                {
                    _isLevel1Checked = value;
                    OnPropertyChanged(nameof(IsLevel1Checked));
                }
            }
        }

        private bool _isLevel2Checked;
        public bool IsLevel2Checked
        {
            get { return _isLevel2Checked; }
            set
            {
                if (_isLevel2Checked != value)
                {
                    _isLevel2Checked = value;
                    OnPropertyChanged(nameof(IsLevel2Checked));
                }
            }
        }

        private bool _isLevel3Checked;
        public bool IsLevel3Checked
        {
            get { return _isLevel3Checked; }
            set
            {
                if (_isLevel3Checked != value)
                {
                    _isLevel3Checked = value;
                    OnPropertyChanged(nameof(IsLevel3Checked));
                }
            }
        }

        private bool _isLevel4Checked;
        public bool IsLevel4Checked
        {
            get { return _isLevel4Checked; }
            set
            {
                if (_isLevel4Checked != value)
                {
                    _isLevel4Checked = value;
                    OnPropertyChanged(nameof(IsLevel4Checked));
                }
            }
        }

        private bool _isLevel5Checked;
        public bool IsLevel5Checked
        {
            get { return _isLevel5Checked; }
            set
            {
                if (_isLevel5Checked != value)
                {
                    _isLevel5Checked = value;
                    OnPropertyChanged(nameof(IsLevel5Checked));
                }
            }
        }

        private bool AreAnyLevelsSelected()
        {
            return IsLevel1Checked || IsLevel2Checked || IsLevel3Checked || IsLevel4Checked || IsLevel5Checked;
        }

        private int GetSelectedRenovationLevel()
        {
            if (IsLevel1Checked) return 1;
            if (IsLevel2Checked) return 2;
            if (IsLevel3Checked) return 3;
            if (IsLevel4Checked) return 4;
            if (IsLevel5Checked) return 5;
            return 0;
        }


        private void SubmitAccommodationRenovation(AccommodationRate ratedAccommodation)
        {
            if (WhatToRenovate is null || !AreAnyLevelsSelected())
            {
                MessageBox.Show("Please leave a message about what to renovate and select a level of urgency of renovation.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            _service.UpdateAccommodationRateData(Rate, WhatToRenovate, GetSelectedRenovationLevel());

        }
        public AccommodationRenovateViewModel(AccommodationRate ratedAccommodation)
        {
            Rate = ratedAccommodation;
            _service = new AccommodationRateService(ratedAccommodation);
            SubmitAccommodationRenovateCommand = new ViewModelCommand<AccommodationRate>(SubmitAccommodationRenovation);
        }
    }
}
