using BookingApp.Interfaces;
using BookingApp.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    public class TourVoucherRepository : ITourVoucherRepository
    {

        private const string FilePath = "../../../Resources/Data/tourvouchers.csv";

        private readonly Serializer<TourVoucher> _serializer;

        private List<TourVoucher> _tourVouchers;

        public TourVoucherRepository()
        {
            _serializer = new Serializer<TourVoucher>();
            _tourVouchers = _serializer.FromCSV(FilePath);
        }


        public List<TourVoucher> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public TourVoucher Save(TourVoucher tourVoucher)
        {
            tourVoucher.Id = NextId();
            _tourVouchers = _serializer.FromCSV(FilePath);
            _tourVouchers.Add(tourVoucher);
            _serializer.ToCSV(FilePath, _tourVouchers);
            return tourVoucher;
        }

        public int NextId()
        {
            _tourVouchers = _serializer.FromCSV(FilePath);
            if (_tourVouchers.Count < 1)
            {
                return 1;
            }
            return _tourVouchers.Max(c => c.Id) + 1;
        }

        public void Delete(TourVoucher tourVoucher)
        {
            _tourVouchers = _serializer.FromCSV(FilePath);
            TourVoucher founded = _tourVouchers.Find(c => c.Id == tourVoucher.Id);
            _tourVouchers.Remove(founded);
            _serializer.ToCSV(FilePath, _tourVouchers);
        }

        public TourVoucher Update(TourVoucher tourVoucher)
        {
            _tourVouchers = _serializer.FromCSV(FilePath);
            TourVoucher current = _tourVouchers.Find(c => c.Id == tourVoucher.Id);
            int index = _tourVouchers.IndexOf(current);
            _tourVouchers.Remove(current);
            _tourVouchers.Insert(index, tourVoucher);       // keep ascending order of ids in file 
            _serializer.ToCSV(FilePath, _tourVouchers);
            return tourVoucher;
        }


    }
}
