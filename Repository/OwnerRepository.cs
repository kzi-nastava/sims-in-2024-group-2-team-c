using BookingApp.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    public class OwnerRepository
    {

        private const string FilePath = "../../../Resources/Data/owners.csv";
        private readonly Serializer<Owner> _serializer;
        private List<Owner> _owners; 

        public int NextId()  
        {
            List<Owner> owners = _serializer.FromCSV(FilePath);
            if (owners.Count < 1)
            {
                return 1;
            }
            return owners.Max(guest => guest.Id) + 1;
        }
        
        public OwnerRepository()
        {
            _serializer = new Serializer<Owner>(); // Inicijalizujemo _serializer
            _owners = _serializer.FromCSV(FilePath);
        }

        // Dodavanje novog vlasnika
        public void AddOwner(Owner owner)
        {
            _owners.Add(owner);
        }

        // Dohvatanje vlasnika po ID-ju
        public Owner GetOwnerById(int ownerId)
        {
            return _owners.FirstOrDefault(o => o.Id == ownerId);
        }

        // Dohvatanje svih vlasnika
        public List<Owner> GetAllOwners()
        {
            return _owners;
        }

        // Ažuriranje podataka o vlasniku
        public void UpdateOwner(Owner updatedOwner)
        {
            var existingOwner = _owners.FirstOrDefault(o => o.Id == updatedOwner.Id);
            if (existingOwner != null)
            {
                existingOwner.Username = updatedOwner.Username;
                existingOwner.Password = updatedOwner.Password;
                existingOwner.Role = updatedOwner.Role;
                existingOwner.Super = updatedOwner.Super;
                existingOwner.Name = updatedOwner.Name;
                existingOwner.Surname = updatedOwner.Surname;
                existingOwner.Email = updatedOwner.Email;
                existingOwner.PhoneNumber = updatedOwner.PhoneNumber;
                existingOwner.NumberOfRatings = updatedOwner.NumberOfRatings;
                existingOwner.TotalRating = updatedOwner.TotalRating;
            }
        }

        // Brisanje vlasnika
        public void DeleteOwner(int ownerId)
        {
            _owners.RemoveAll(o => o.Id == ownerId);
        }
        public Owner GetOwnerByLoggedInUserId(int loggedInUserId)
        {
           
            return _owners.FirstOrDefault(owner => owner.Id == loggedInUserId);
        }
    }
}