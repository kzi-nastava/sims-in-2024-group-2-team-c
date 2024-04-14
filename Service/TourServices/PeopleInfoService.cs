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
    public class PeopleInfoService
    {
        public PeopleInfoRepository _peopleInfoRepository { get; set; }


        public PeopleInfoService()
        {
            _peopleInfoRepository = new PeopleInfoRepository();
        }
        public PeopleInfo GetById(int id)
        {
            return _peopleInfoRepository.GetById(id);
        }
    }
}
