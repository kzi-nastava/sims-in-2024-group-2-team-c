using BookingApp.DTO;
using BookingApp.Injector;
using BookingApp.Interfaces;
using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.WPF.View.TouristView;
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
        
        private readonly TourService tourService;

        public TourVoucherService()
        {
            _tourVoucherRepository = Injectorr.CreateInstance<ITourVoucherRepository>();
            tourService = new TourService();
        }

        public void Send(TourVoucher tourVoucher)
        {
           _tourVoucherRepository.Save(tourVoucher);
        }

        public void Delete(TourVoucher tourVoucher)
        {
            _tourVoucherRepository.Delete(tourVoucher);
        }

        public List<TouristVoucherDTO> GetVouchersByTouristId(int touristId)
        {
            List<TourVoucher> filteredVouchers = GetByTouristId(touristId);
            List<TouristVoucherDTO> vouchersDTO = FilterVouchers(filteredVouchers);

            return vouchersDTO;
        }
        public List<TourVoucher> GetByTouristId(int touristId) 
        {
            List<TourVoucher> allVouchers = _tourVoucherRepository.GetAll();
            List<TourVoucher> filteredVouchers = allVouchers.Where(v => v.TouristId == touristId).ToList();
            return filteredVouchers;
        }

        private List<TouristVoucherDTO> FilterVouchers(List<TourVoucher> filteredVouchers)
        {
            return filteredVouchers.Select(voucher => new TouristVoucherDTO
            {
                TourId = voucher.TourId,
                TouristId = voucher.TouristId,
                ExpirationDate = voucher.ExpirationDate,
                TourName = tourService.GetTourNameById(voucher.TourId),
                IsUniversal = voucher.IsUniversal
            }).ToList();
        }


        public void RemoveExpiredVouchers()
        {
            
            List<TourVoucher> allVouchers = _tourVoucherRepository.GetAll();

            DateTime currentDate = DateTime.Now;

            // Iterate through the vouchers
            foreach (TourVoucher voucher in allVouchers)
            {
                if (voucher.ExpirationDate < currentDate)
                {
                    // If expired, remove the voucher from the repository
                    _tourVoucherRepository.Delete(voucher);
                }
            }
        }
        public bool TouristContainVoucher(int touristsId, Tour tour)
        {
            List<TourVoucher> touristsVouchers = GetByTouristId(touristsId);
            foreach(TourVoucher voucher in touristsVouchers)
            {
                if(voucher.TourId == tour.Id)
                {
                    return true;
                }
            }
            return false;
        }

        public List<TourVoucher> GetVouchersByTourId(int tourId)
        {
            
            List<TourVoucher> allVouchers = _tourVoucherRepository.GetAll();


            List<TourVoucher> filteredVouchers = allVouchers.Where(v => v.TouristId == LoggedInUser.Id && (v.TourId == tourId || v.IsUniversal)).ToList();

            return filteredVouchers;
        }

        public void CreateUniversalTourVoucher(Tourist t) { 
            
            TourVoucher voucher = new TourVoucher(0,t.Id, DateTime.Now.AddMonths(6),true);
            Send(voucher);
        }



    }
}
