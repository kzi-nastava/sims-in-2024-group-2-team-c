using BookingApp.DTO;
using BookingApp.Interfaces;
using BookingApp.Model;
using BookingApp.Repository;
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
        private readonly ITourRepository _tourRepository;
            
        public TourVoucherService(ITourVoucherRepository tourVoucherRepository)
        {
            _tourVoucherRepository = tourVoucherRepository;
            _tourRepository = new TourRepository();
        }

        public void Send(TourVoucher tourVoucher)
        {
           _tourVoucherRepository.Save(tourVoucher);
        }

        public List<TouristVoucherDTO> GetVouchersByTouristId(int touristId)
        {
            // Retrieve all vouchers from the repository
            List<TourVoucher> allVouchers = _tourVoucherRepository.GetAll();

            // Filter vouchers based on the given touristId
            List<TourVoucher> filteredVouchers = allVouchers.Where(v => v.TouristId == touristId).ToList();

            // Map the filtered list of TourVoucher to TouristVoucherDTO
            List<TouristVoucherDTO> vouchersDTO = filteredVouchers.Select(voucher => new TouristVoucherDTO
            {
                TourId = voucher.TourId,
                TouristId = voucher.TouristId,
                ExpirationDate = voucher.ExpirationDate,
                TourName = _tourRepository.GetTourNameById(voucher.TourId) // Implement this function to get the tour name by its ID
            }).ToList();

            // Return the list of TouristVoucherDTO
            return vouchersDTO;
        }


        public void RemoveExpiredVouchers()
        {
            // Retrieve all vouchers from the repository
            List<TourVoucher> allVouchers = _tourVoucherRepository.GetAll();

            // Get the current date and time
            DateTime currentDate = DateTime.Now;

            // Iterate through the vouchers
            foreach (TourVoucher voucher in allVouchers)
            {
                // Check if the voucher has expired
                if (voucher.ExpirationDate < currentDate)
                {
                    // If expired, remove the voucher from the repository
                    _tourVoucherRepository.Delete(voucher);
                }
            }
        }


    }
}
