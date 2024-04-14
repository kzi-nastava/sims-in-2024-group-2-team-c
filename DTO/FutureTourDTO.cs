using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.DTO
{
    public class FutureTourDTO : INotifyPropertyChanged
    {
        private Tour _tour;
        private TourInstance _tourInstance;
        public FutureTourDTO() 
        {
            _tour = new Tour();
            _tourInstance = new TourInstance();
        }
        public FutureTourDTO(Tour tour, TourInstance tourInstance)
        {
            _tour = tour;
            _tourInstance = tourInstance;
        }
        public int Id 
        {
            get => _tourInstance.Id;
            set
            {
                if(value != _tourInstance.Id)
                {
                    _tourInstance.Id = value;
                    OnPropertyChanged();
                }
            } 
        }
        public string Name 
        {
            
                get => _tour.Name;
                set
                {
                    if (value != _tour.Name)
                    {
                        _tour.Name = value;
                        OnPropertyChanged();
                    }
                }
        }
        public string Description 
        {
            get => _tour.Description;
            set
            {
                if (_tour.Description != value)
                {
                    _tour.Description = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Location 
        {
           get => _tour.ViewLocation;
           set
            {
                if(_tour.ViewLocation != value)
                {
                    _tour.ViewLocation = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Language 
        {
            get => _tour.Language;
            set
            {
                if (!_tour.Language.Equals(value))
                {
                    _tour.Language = value;
                    OnPropertyChanged();
                }
            }
        }
        public int Duration 
        {
            get => _tour.Duration;
            set
            {
                if( value != _tour.Duration)
                {
                    _tour.Duration = value;
                    OnPropertyChanged();
                }
            }
        }
        public DateTime Date 
        {
            get => _tourInstance.Date;
            set
            {
                if (value.Date != _tourInstance.Date.Date){
                    _tourInstance.Date = value.Date;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public FutureTourDTO(string name, string description, string location, string language, int duration, DateTime date)
        {
            Name = name;
            Description = description;
            Location = location;
            Language = language;
            Duration = duration;
            Date = date;
        }
    }
}
