using BookingApp.Injector;
using BookingApp.Interfaces;
using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Service.TourServices
{
    public class LanguageService
    {
        private readonly ILanguageRepository _languageRepository;

        public LanguageService() {
            _languageRepository = Injectorr.CreateInstance<ILanguageRepository>();
        }


        public List<Language> GetAll() { 
            return _languageRepository.GetAll();
        }

    }
}
