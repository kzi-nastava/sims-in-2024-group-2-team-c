using BookingApp.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BookingApp.Repository
{
    internal class LocationRepository
    {

        private const string FilePath = "../../../Resources/Data/locations.csv";

        private readonly Serializer<Location> _serializer;

        private List<Location> _locations;

        public LocationRepository()
        {
            _serializer = new Serializer<Location>();
            _locations= _serializer.FromCSV(FilePath);
        }

        public List<Location> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public Location Save(Location location)
        {
            location.Id = NextId();
            _locations = _serializer.FromCSV(FilePath);
            _locations.Add(location);
            _serializer.ToCSV(FilePath, _locations);
            return location;
        }


        public int NextId()
        {
            _locations = _serializer.FromCSV(FilePath);
            if (_locations.Count < 1)
            {
                return 1;
            }
            return _locations.Max(c => c.Id) + 1;
        }

        public void Delete(Location location)
        {
            _locations = _serializer.FromCSV(FilePath);
            Location founded = _locations.Find(c => c.Id == location.Id);
            _locations.Remove(founded);
            _serializer.ToCSV(FilePath, _locations);
        }

        public Location Update(Location location)
        {
            _locations = _serializer.FromCSV(FilePath);
            Location current = _locations.Find(c => c.Id == location.Id);
            int index = _locations.IndexOf(current);
            _locations.Remove(current);
            _locations.Insert(index, location);       // keep ascending order of ids in file 
            _serializer.ToCSV(FilePath, _locations);
            return location;
        }
        public Location FindLocation(string city, string country)
        {
            _locations = _serializer.FromCSV(FilePath);
            Location current = _locations.Find(l => ((l.City.Equals(city)) && (l.Country.Equals(country))));
            if (current == null)
            {
                Location newLocation = new Location(city, country);
                Save(newLocation);
                return newLocation;
            }
            return current;

        }
        public Location GetById(int id)
        {
            _locations = _serializer.FromCSV(FilePath);
            return _locations.Find(c => c.Id == id);

        }

        /*public int GetLocationId(Location location)
        {
            _locations = _serializer.FromCSV(FilePath);
            Location current = _locations.Find(c => c.Id == location.Id);
            if (current == null)
        }*/

        public Location Get(int id)
        {
            return _locations.FirstOrDefault(location => location.Id == id);
        }

        public int GetIdByCityorCoutry(string searchString)
        {
            Location location = _locations.FirstOrDefault(l => l.City.ToLower() == searchString.ToLower() || l.Country.ToLower() == searchString.ToLower());
            return location != null ? location.Id : -1; // Return -1 if location not found
        }


    }
}
