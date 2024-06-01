using BookingApp.DTO;
using BookingApp.Injector;
using BookingApp.Interfaces;
using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Service.TourServices;
using BookingApp.WPF.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingApp.Service.AccommodationServices
{
    public class ForumService
    {
        private readonly IForumRepository _forumRepository;
        private readonly LocationService _locationService;
        private readonly GuestReservationService _guestReservationService;

        public ForumService()
        {
            _forumRepository = Injectorr.CreateInstance<IForumRepository>();
            _locationService = new LocationService();
            _guestReservationService = new GuestReservationService();
        }

        public void SaveForum(string location, string forumComment)
        {
            try
            {
                int LocationId = _locationService.GetIdByCityorCoutry(location);

                bool hasVisitedLocation = _guestReservationService.HasGuestVisitedLocation(LocationId);

                var data = new Forum
                {

                    Location = new Location() { Id = LocationId },
                    User = new User() { Id = LoggedInUser.Id},
                    IsForumMine = true,
                    ForumComment = forumComment,
                    HasBeenVisited = hasVisitedLocation,
                    IsForumClosed = false
                };

                _forumRepository.Save(data);


                MessageBox.Show("Forum successfully opened.");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw;
            }
        }

        public List<ForumDTO> GetAllMyForums(int LoggedInUserId)
        {
            List<ForumDTO> myForums = new List<ForumDTO>();
            List<Forum> forums = _forumRepository.GetAll();

            foreach (Forum forum in forums)
            {
                if (forum.User.Id == LoggedInUserId && forum.IsForumMine == true) {

                    Location forumLocation = GetLocationsByUserId(LoggedInUserId);
                    string location = $"{forumLocation.Country}, {forumLocation.City}";

                    int numberfComments = CountCommentsForLocation(forumLocation.Id);

                    ForumDTO myForumDTO = new ForumDTO
                    {
                        Id = forum.Id,
                        Location = location,
                        NumOfComments = numberfComments
                    };

                    myForums.Add(myForumDTO);
                }
                
            }

            return myForums;
        }

        private int CountCommentsForLocation(int id)
        {
            int count = 0;
            List<Forum> forums = _forumRepository.GetAll();
            foreach (Forum forum in forums)
            {
                if(forum.Location.Id == id)
                {
                    count++;
                }
            }
            return count;
        }

        public Location GetLocationsByUserId(int loggedInUserId)
        {
            List<Forum> forums = _forumRepository.GetAll();
            foreach (Forum forum in forums)
            {
                if (forum.User.Id == loggedInUserId)
                {
                    int LocationId = forum.Location.Id;
                    Location location = _locationService.GetById(LocationId);
                    return location;
                }
            }
            return null; ;
        }


        public List<ForumDTO> GetAllOtherForums(int LoggedInUserId)
        {
            List<ForumDTO> otherForums = new List<ForumDTO>();
            List<Forum> forums = _forumRepository.GetAll();

            foreach (Forum forum in forums)
            {
                if (forum.User.Id == LoggedInUserId && forum.IsForumMine == false)
                {

                    Location forumLocation = GetLocationsByUserId(LoggedInUserId);
                    string location = $"{forumLocation.Country}, {forumLocation.City}";

                    int numberfComments = CountCommentsForLocation(forumLocation.Id);

                    ForumDTO otherForumDTO = new ForumDTO
                    {
                        Id = forum.Id,
                        Location = location,
                        NumOfComments = numberfComments
                    };

                    otherForums.Add(otherForumDTO);
                }

            }

            return otherForums;
        }

        public string CloseForum(int forumId)
        {
            try
            {
                Forum forumToClose = _forumRepository.GetById(forumId);
                if (forumToClose != null)
                {
                    forumToClose.IsForumClosed = true;
                    _forumRepository.Update(forumToClose);
                    return "Forum successfully closed.";
                }
                else
                {
                    return "Forum not found.";
                }
            }
            catch (Exception ex)
            {
                return $"Error closing forum: {ex.Message}";
            }
        }
    }
}
