using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BookingApp.Model
{
    public class TourReview : ISerializable
    {
        public int Id { get; set; }
        public int TourInstanceId { get; set; }
        public int GuideId { get; set; }
        public int TouristId { get; set; }
        public int KnowledgeGrade {  get; set; }
        public int LanguageGrade {  get; set; }
        public int InterestingGrade {  get; set; }
        public string Comment { get; set; }
        public List<string> Images { get; set; }

        public TourReview() { }
        public TourReview(int id, int tourInstanceId, int guideId, int touristId, int knowledgeGrade, int languageGrade, int interestingGrade, string comment, List<string> images)
        {
            Id = id;
            TourInstanceId = tourInstanceId;
            GuideId = guideId;
            TouristId = touristId;
            KnowledgeGrade = knowledgeGrade;
            LanguageGrade = languageGrade;
            InterestingGrade = interestingGrade;
            Comment = comment;
            this.Images = images;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), TourInstanceId.ToString(), GuideId.ToString(), TouristId.ToString(),
            KnowledgeGrade.ToString(), LanguageGrade.ToString(), InterestingGrade.ToString(), Comment, string.Join(",", Images)};
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            TourInstanceId = Convert.ToInt32(values[1]);
            GuideId = Convert.ToInt32(values[2]);
            TouristId = Convert.ToInt32(values[3]);
            KnowledgeGrade = Convert.ToInt32(values[4]);
            LanguageGrade = Convert.ToInt32(values[5]);
            InterestingGrade = Convert.ToInt32(values[6]);
            Comment = values[7];
            
            if (!string.IsNullOrEmpty(values[8]))
            {
                Images = values[8].Split(',').ToList();
            }
            else
            {
                Images = new List<string>();
            }
        }
    }
}
