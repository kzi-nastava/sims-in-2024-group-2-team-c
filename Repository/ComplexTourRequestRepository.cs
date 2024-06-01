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
    public class ComplexTourRequestRepository : IComplexTourRequestRepository
    {


        private const string FilePath = "../../../Resources/Data/complextourrequests.csv";
        
        private readonly Serializer<ComplexTourRequest> _serializer;

        private List<ComplexTourRequest> _requests;

        public ComplexTourRequestRepository()
        {
            _serializer = new Serializer<ComplexTourRequest>();
            _requests = _serializer.FromCSV(FilePath);
        }

        public List<ComplexTourRequest> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }


        public ComplexTourRequest Save(ComplexTourRequest request)
        {
            request.Id = NextId();
            _requests = _serializer.FromCSV(FilePath);
            _requests.Add(request);
            _serializer.ToCSV(FilePath, _requests);
            return request;
        }

        public int NextId()
        {
            _requests = _serializer.FromCSV(FilePath);
            if (_requests.Count < 1)
            {
                return 1;
            }
            return _requests.Max(c => c.Id) + 1;
        }

        public void Delete(ComplexTourRequest request)
        {
            _requests = _serializer.FromCSV(FilePath);
            ComplexTourRequest founded = _requests.Find(c => c.Id == request.Id);
            _requests.Remove(founded);
            _serializer.ToCSV(FilePath, _requests);
        }

        public ComplexTourRequest Update(ComplexTourRequest request)
        {
            _requests = _serializer.FromCSV(FilePath);
            ComplexTourRequest current = _requests.Find(c => c.Id == request.Id);
            int index = _requests.IndexOf(current);
            _requests.Remove(current);
            _requests.Insert(index, request);       // keep ascending order of ids in file 
            _serializer.ToCSV(FilePath, _requests);
            return request;
        }




    }
}
