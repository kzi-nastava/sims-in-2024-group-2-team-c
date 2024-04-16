using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Service.TourServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.ViewModel.TouristViewModel
{
    public class TouristVoucherViewModel : ViewModelBase
    {

        private ObservableCollection<TouristVoucherDTO> _voucher;

        public ObservableCollection<TouristVoucherDTO> Vouchers
        {
            get { return _voucher; }
            set
            {
                _voucher = value;
                OnPropertyChanged(nameof(Vouchers));
            }
        }

        public readonly TourVoucherService _voucherService;

        public TouristVoucherViewModel() {

            _voucherService = new TourVoucherService();
            CheckExpirationDate();
            LoadVouchers();
        }

        private void LoadVouchers()
        {

            Vouchers = new ObservableCollection<TouristVoucherDTO>(_voucherService.GetVouchersByTouristId(LoggedInUser.Id));

        }

        void CheckExpirationDate()
        {
            _voucherService.RemoveExpiredVouchers();
        }

    }
}
