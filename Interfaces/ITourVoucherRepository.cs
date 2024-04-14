using BookingApp.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Interfaces
{
    public  interface ITourVoucherRepository
    {



        public List<TourVoucher> GetAll();
        public TourVoucher Save(TourVoucher tourVoucher);

        public int NextId();
        public void Delete(TourVoucher tourVoucher);

        public TourVoucher Update(TourVoucher tourVoucher);



    }
}
