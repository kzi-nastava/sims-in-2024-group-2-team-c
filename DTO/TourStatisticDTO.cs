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
    public class TourStatisticDTO
    {
        /*private Tour _tour;
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
        }*/
        public int TourInstanceId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string Language { get; set; }
        public int Duration { get; set; }

        public DateTime Date { get; set; }
        public int MaxTourists { get; set; }
        public int ReservedTourists { get; set; }
        public int PresentTourists { get; set; }
        public int LessThan18 { get; set; }
        public int Between18And50 { get; set; }
        public int MoreThan50 { get; set; }
        public TourStatisticDTO() { }
        public TourStatisticDTO(int tourInstanceId, string name, string description, string location, string language, int duration, DateTime date, int maxTourists, int reservedTourists, int presentTourists,
            int lessThan18, int between18and50, int moreThan50)
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
            PresentTourists = presentTourists;
            LessThan18 = lessThan18;
            Between18And50 = between18and50;
            MoreThan50 = moreThan50;
        }
    }
}
