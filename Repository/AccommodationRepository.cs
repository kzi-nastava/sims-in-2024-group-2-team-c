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
    public class AccommodationRepository
    {
        private const string FilePath = "../../../Resources/Data/accommodations.csv";
        private readonly Serializer<Accommodation> _serializer;
        private List<Accommodation> _accommodations;


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
            return _serializer.FromCSV(FilePath);
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
