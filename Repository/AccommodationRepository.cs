using BookingApp.Interfaces;
using BookingApp.Model;
using BookingApp.Serializer;
using BookingApp.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;

namespace BookingApp.Repository
{
    public class AccommodationRepository : IAccommodationRepository
    {
        private const string FilePath = "../../../Resources/Data/accommodations.csv";
        private readonly Serializer<Accommodation> _serializer;
        private List<Accommodation> _accommodations;
        private readonly LocationRepository _locationRepository;


        public void AddImagesToCSV(List<string> imagePaths, string accommodationName)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(FilePath, true))
                {
                    foreach (string imagePath in imagePaths)
                    {
                        string relativeImagePath = Path.GetRelativePath(Environment.CurrentDirectory, imagePath);
                        writer.WriteLine($"{accommodationName},{relativeImagePath}");
                    }
                }

                Console.WriteLine("Images added to accommodations.csv successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding images to accommodations.csv: {ex.Message}");
            }
        }


        public AccommodationRepository()
        {
            _serializer = new Serializer<Accommodation>();
            _accommodations = _serializer.FromCSV(FilePath);
        }

        
        public List<Accommodation> GetAll()
        {
            List<Accommodation> accommodations = _serializer.FromCSV(FilePath);

            foreach (var accommodation in accommodations)
            {
                string[] imagePathsArray = accommodation.Images.ToArray();
                accommodation.Images = imagePathsArray.ToList();
            }

            return accommodations;
        }

        public List<Accommodation> GetToursByLocationId(int locationId)
        {
            return _accommodations.Where(acc => acc.Location.Id == locationId).ToList();
        }

        public List<Accommodation> GetAccommodationsByName(string searchTerm)
        {
            return _accommodations
                .Where(acc => acc.Name.ToLower().Contains(searchTerm.ToLower()))
                .ToList();
        }

        public List<Accommodation> GetAccommodationsByType(string type)
        {
            return _accommodations
                .Where(acc => acc.Type.ToLower() == type.ToLower())
                .ToList();
        }

        public List<Accommodation> GetAccommodationsByNumOfGuests(string numOfGuestsStr)
        {
            if (!int.TryParse(numOfGuestsStr, out int numOfGuests))
            {
                Console.WriteLine("Invalid input. Please enter a valid number of guests.");
                return new List<Accommodation>();
            }

            List<Accommodation> validAccommodations = _accommodations
                .Where(acc => acc.MaxGuests >= numOfGuests)
                .ToList();

            foreach (var acc in _accommodations.Except(validAccommodations))
            {
                Console.WriteLine($"Accommodation '{acc.Name}' cannot accommodate {numOfGuests} guests. Maximum guests allowed: {acc.MaxGuests}");
            }

            return validAccommodations;
        }


        public List<Accommodation> GetAccommodationsByBookingDays(string bookingDaysStr)
        {
            if (!int.TryParse(bookingDaysStr, out int bookingDays))
            {
                Console.WriteLine("Invalid input. Please enter a valid number of booking days.");
                return new List<Accommodation>();
            }

            List<Accommodation> validAccommodations = _accommodations
                .Where(acc => acc.MinBookingDays <= bookingDays)
                .ToList();

            foreach (var acc in _accommodations.Except(validAccommodations))
            {
                Console.WriteLine($"Accommodation '{acc.Name}' requires minimum booking days of {acc.MinBookingDays}. Your input: {bookingDays} days.");
            }

            return validAccommodations;
        }


        public Accommodation Save(Accommodation accommodation)
        {
            accommodation.Id = NextId();
            _accommodations = _serializer.FromCSV(FilePath);
            _accommodations.Add(accommodation);
            _serializer.ToCSV(FilePath, _accommodations);
            return accommodation;
        }

        public int NextId()
        {
            _accommodations = _serializer.FromCSV(FilePath);
            if (_accommodations.Count < 1)
            {
                return 1;
            }
            return _accommodations.Max(a => a.Id) + 1;
        }

        public void Delete(Accommodation accommodation)
        {
            _accommodations = _serializer.FromCSV(FilePath);
            Accommodation founded = _accommodations.Find(a => a.Id == accommodation.Id);
            _accommodations.Remove(founded);
            _serializer.ToCSV(FilePath, _accommodations);
        }

        public Accommodation Update(Accommodation accommodation)
        {
            _accommodations = _serializer.FromCSV(FilePath);
            Accommodation current = _accommodations.Find(a => a.Id == accommodation.Id);
            int index = _accommodations.IndexOf(current);
            _accommodations.Remove(current);
            _accommodations.Insert(index, accommodation);
            _serializer.ToCSV(FilePath, _accommodations);
            return accommodation;
        }
        public Accommodation GetAccommodationById(int id) //dodato
        {
            _accommodations = _serializer.FromCSV(FilePath);
            return _accommodations.FirstOrDefault(a => a.Id == id);
        }


    }
}
