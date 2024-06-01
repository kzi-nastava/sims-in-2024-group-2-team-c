﻿using BookingApp.DTO;
using BookingApp.Injector;
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
                TourName = tourService.GetTourNameById(voucher.TourId)
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
            // Retrieve all vouchers from the repository
            List<TourVoucher> allVouchers = _tourVoucherRepository.GetAll();

            // Filter vouchers based on the given tourId
            List<TourVoucher> filteredVouchers = allVouchers.Where(v => v.TourId == tourId && v.TouristId == LoggedInUser.Id).ToList();

            // Map the filtered list of TourVoucher to TouristVoucherDTO
           /* List<TouristVoucherDTO> vouchersDTO = filteredVouchers.Select(voucher => new TouristVoucherDTO
            {
                TourId = voucher.TourId,
                TouristId = voucher.TouristId,
                ExpirationDate = voucher.ExpirationDate,
                // Get the tour name using the tour repository
                TourName = tourService.GetTourNameById(voucher.TourId)
            }).ToList();*/

            // Return the list of TouristVoucherDTO
            return filteredVouchers;
        }



    }
}
