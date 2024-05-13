using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Xml.Linq;

namespace BookingApp.Model
{
    public class TourReccommendations : ISerializable
    {

        public int Id;
        public int TourId { get; set; }

        public int RequestId { get; set; }
        public bool IsLanguage { get; set; }
        public bool IsLocation { get; set; }

        public string Location { get; set; }

        public string Language { get; set; }
        public string GuideName { get; set; }

        public TourReccommendations() { }

        public TourReccommendations( int tourId, bool isLanguage, bool isLocation, string location, string language, string guideName, int requestId)
        {
            TourId = tourId;
            IsLanguage = isLanguage;
            IsLocation = isLocation;
            Location = location;
            Language = language;
            GuideName = guideName;
            RequestId = requestId;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            TourId = Convert.ToInt32(values[1]);
            IsLanguage = Convert.ToBoolean(values[2]);
            IsLocation = Convert.ToBoolean(values[3]);
            Location = values[4];
            Language = values[5];
            GuideName = values[6];
            RequestId = Convert.ToInt32(values[7]);
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), TourId.ToString(), IsLanguage.ToString(), IsLocation.ToString(),Location,Language,GuideName,RequestId.ToString() };
            return csvValues;
        }
    }
}
