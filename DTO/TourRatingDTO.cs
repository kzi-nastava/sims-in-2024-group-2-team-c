using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.DTO
{
    public class TourRatingDTO
    {

        public int KnowledgeGrade { get; set; } 
        public int LanguageGrade { get; set; } 
        public int InterestingGrade { get; set; }
        public string Comment { get; set; }
        public List<string> Images { get; set; }

        public TourRatingDTO() { }

        public TourRatingDTO(int knowledgeGrade,int languageGrade,int interestingGrade,string comment, List<string> images)
        {
            KnowledgeGrade = knowledgeGrade;
            LanguageGrade = languageGrade;
            InterestingGrade = interestingGrade;
            Comment = comment;
            Images = images;
        }




    }
}
