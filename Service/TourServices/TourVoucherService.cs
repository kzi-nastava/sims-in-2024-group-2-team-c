using BookingApp.Interfaces;
using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Service.TourServices
{
    public class TourVoucherService
    {

        private readonly ITourVoucherRepository _tourVoucherRepository;
            
        public TourVoucherService(ITourVoucherRepository tourVoucherRepository)
        {
            _tourVoucherRepository = tourVoucherRepository;
        }

        public void Send(TourVoucher tourVoucher)
        {
           _tourVoucherRepository.Save(tourVoucher);
        }

    }
}
