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
    public class TourStatisticDTO : INotifyPropertyChanged
    {
        private Tour _tour;
        private TourInstance _tourInstance;
        public TourStatisticDTO() 
        {
            _tour = new Tour();
            _tourInstance = new TourInstance();
        }
        public TourStatisticDTO(Tour tour, TourInstance instance) 
        {
            _tour = tour;
            _tourInstance = instance;
        }
        public int TourInstanceId 
        {
            get => _tourInstance.Id;
            set
            {
                if (value != _tourInstance.Id)
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
                if (_tour.ViewLocation != value)
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
                if (value != _tour.Duration)
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
                if (value.Date != _tourInstance.Date.Date)
                {
                    _tourInstance.Date = value.Date;
                    OnPropertyChanged();
                }
            }
        }
        public int MaxTourists
        {
            get => _tourInstance.MaxTourists;
            set
            {
                if (value != _tourInstance.MaxTourists)
                {
                    _tourInstance.MaxTourists = value;
                    OnPropertyChanged();
                }
            }
        }
        public int ReservedTourists
        {
            get => _tourInstance.ReservedTourists;
            set
            {
                if (value != _tourInstance.ReservedTourists)
                {
                    _tourInstance.ReservedTourists = value;
                    OnPropertyChanged();
                }
            }
        }
        //public int PresentTourists { get; set; }
        /*{
            get => _tourInstance.ReservedTourists;
            set
            {
                if (value != _tourInstance.ReservedTourists)
                {
                    _tourInstance.ReservedTourists = value;
                    OnPropertyChanged();
                }
            }
        }*/
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public int PresentTourists { get; set; }
        public TourStatisticDTO(int tourInstanceId, string name, string description, string location, string language, int duration, DateTime date, int maxTourists, int reservedTourists/*, int presentTourists*/)
        {
            TourInstanceId = tourInstanceId;
            Name = name;
            Description = description;
            Location = location;
            Language = language;
            Duration = duration;
            Date = date;
            MaxTourists = maxTourists;
            ReservedTourists = reservedTourists;
            //PresentTourists = presentTourists;
        }
    }
}
