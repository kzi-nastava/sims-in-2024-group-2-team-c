using BookingApp.Serializer;

namespace BookingApp.Model
{
    public class Location : ISerializable
    {
        public string City { get; set; }
        public string Country { get; set; }
        public void FromCSV(string[] values)
        {
            //string[] csvValues = values.Split(' ');
            City = values[0];
            Country = values[1];
        }

        public string[] ToCSV()
        {
            string[] csvValues = { City, Country };
            return csvValues;
        }
    }
}
