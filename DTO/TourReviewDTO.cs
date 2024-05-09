using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.DTO
{
    public class TourReviewDTO
    {
        //public TourReview tourReview;
        public int Id { get; set; }
        public string TourName { get; set; }
        public string Tourist { get; set; }
        public string Comment { get; set; }
        public int? KnowledgeGrade { get; set; }
        public int? LanguageGrade { get; set; }
        public int? InterestingGrade { get; set; }
        public string StartKeyPoint { get; set; }
        public bool Reported { get; set; }
        public DateTime Date {  get; set; }
        public List<string> Images { get; set; }
        public TourReviewDTO() { }
    }
}
