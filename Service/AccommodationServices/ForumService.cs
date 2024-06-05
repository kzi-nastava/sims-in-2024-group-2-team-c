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
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace BookingApp.Service.AccommodationServices
{
    public class ForumService
    {
        private readonly IForumRepository _forumRepository;
        private readonly UserRepository _userRepository;
        private readonly LocationService _locationService;
        private readonly GuestReservationService _guestReservationService;
        private readonly AccommodationRepository accommodationRepository;

        public ForumService()
        {
            _forumRepository = Injectorr.CreateInstance<IForumRepository>();
            _locationService = new LocationService();
            _guestReservationService = new GuestReservationService();
            _userRepository = new UserRepository();
            accommodationRepository = new AccommodationRepository();
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
            HashSet<string> addedLocations = new HashSet<string>();

            foreach (Forum forum in forums)
            {
                if (forum.User.Id == LoggedInUserId && forum.IsForumMine == true)
                {
                    Location forumLocation = GetLocationsByForumId(forum.Id);
                    string location = $"{forumLocation.Country}, {forumLocation.City}";

                    if (!addedLocations.Contains(location))
                    {
                        var commentsFromGuests = forums
                            .Where(f => f.Location.Id == forumLocation.Id && f.HasBeenVisited)
                            .Count();

                        List<Accommodation> accommodations = accommodationRepository.GetAll();
                        var commentsFromOwners = accommodations
                            .Where(f => f.Location.Id == forumLocation.Id) //&& LoggedInUser.Id == f.Owner.Id)
                            .Count();

                        bool isVeryUseful = commentsFromGuests >= 3 && commentsFromOwners >= 2;

                        int numberfComments = CountCommentsForLocation(forumLocation.Id);

                        ForumDTO myForumDTO = new ForumDTO
                        {
                            Id = forum.Id,
                            Location = location,
                            NumOfComments = numberfComments,
                            IsForumVeryUseful = isVeryUseful
                        };

                        myForums.Add(myForumDTO);
                        addedLocations.Add(location);
                    }
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

        public Location GetLocationsByForumId(int forumId)
        {
            List<Forum> forums = _forumRepository.GetAll();
            foreach (Forum forum in forums)
            {
                if (forum.Id == forumId)
                {
                    int LocationId = forum.Location.Id;
                    Location location = _locationService.GetById(LocationId);
                    return location;
                }
            }
            return null;
        }


        public List<ForumDTO> GetAllOtherForums(int LoggedInUserId)
        {
            List<ForumDTO> otherForums = new List<ForumDTO>();
            List<Forum> forums = _forumRepository.GetAll();
            HashSet<string> addedLocations = new HashSet<string>();

            foreach (Forum forum in forums)
            {
                if (forum.User.Id != LoggedInUserId) // forum.User.Id == LoggedInUserId && forum.IsForumMine == false)
                {

                    Location forumLocation = GetLocationsByForumId(forum.Id);
                    string location = $"{forumLocation.Country}, {forumLocation.City}";

                    if (!addedLocations.Contains(location))
                    {

                        var commentsFromGuests = forums
                            .Where(f => f.Location.Id == forumLocation.Id && f.HasBeenVisited)
                            .Count();

                        List<Accommodation> accommodations = accommodationRepository.GetAll();
                        var commentsFromOwners = accommodations
                            .Where(f => f.Location.Id == forumLocation.Id) //&& LoggedInUser.Id == f.Owner.Id)
                            .Count();

                        bool isVeryUseful = commentsFromGuests >= 3 && commentsFromOwners >= 2;

                        int numberfComments = CountCommentsForLocation(forumLocation.Id);

                        ForumDTO otherForumDTO = new ForumDTO
                        {
                            Id = forum.Id,
                            Location = location,
                            NumOfComments = numberfComments,
                            IsForumVeryUseful = isVeryUseful
                        };

                        otherForums.Add(otherForumDTO);
                        addedLocations.Add(location);
                    }
                }

            }

            return otherForums;
        }

        public List<ForumDTO> GetAllOtherForumsForOwner(int ownerId)
        {
            List<ForumDTO> otherForums = new List<ForumDTO>();
            List<Forum> forums = _forumRepository.GetAll();
            List<Accommodation> ownerAccommodations = accommodationRepository.GetAll().Where(a => a.Owner.Id == ownerId).ToList();
            HashSet<string> addedLocations = new HashSet<string>();

            foreach (Forum forum in forums)
            {
                if (forum.User.Id != ownerId)
                {
                    Location forumLocation = GetLocationsByForumId(forum.Id);
                    string location = $"{forumLocation.Country}, {forumLocation.City}";

                    if (!addedLocations.Contains(location) && ownerAccommodations.Any(a => a.Location.Id == forumLocation.Id))
                    {
                        var commentsFromGuests = forums
                            .Where(f => f.Location.Id == forumLocation.Id && f.HasBeenVisited)
                            .Count();

                        var commentsFromOwners = ownerAccommodations
                            .Where(a => a.Location.Id == forumLocation.Id)
                            .Count();

                        bool isVeryUseful = commentsFromGuests >= 3 && commentsFromOwners >= 2;

                        int numberOfComments = CountCommentsForLocation(forumLocation.Id);

                        ForumDTO otherForumDTO = new ForumDTO
                        {
                            Id = forum.Id,
                            Location = location,
                            NumOfComments = numberOfComments,
                            IsForumVeryUseful = isVeryUseful
                        };

                        otherForums.Add(otherForumDTO);
                        addedLocations.Add(location);
                    }
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

        public List<ForumDTO> GetAllForumComments(ForumDTO selectedForum)
        {
            List<ForumDTO> Forums = new List<ForumDTO>();
            List<Forum> forums = _forumRepository.GetAll();

            foreach (Forum forum in forums)
            {

                Location forumLocation = GetLocationsByForumId(forum.Id);
                string location = $"{forumLocation.Country}, {forumLocation.City}";

                if (location == selectedForum.Location) 
                {

                    User user = _userRepository.GetUserById(forum.User.Id);
                    bool hasVisitedLocation = _guestReservationService.HasGuestVisitedLocation(forumLocation.Id);

                    ForumDTO ForumDTO = new ForumDTO
                    {
                        Id = forum.Id,
                        //Location = location,
                        //NumOfComments = 0,
                        ForumComment = forum.ForumComment,
                        Username = user.Username,
                        HasBeenVisited = hasVisitedLocation
                    };

                    Forums.Add(ForumDTO);

                }
            }
            return Forums;
        }

        public void AddNewComment(ForumDTO selectedForum, string newComment)
        {
            try
            {
                List<Forum> forums = _forumRepository.GetAll();

                foreach (Forum forum in forums)
                {
                    if(forum.IsForumClosed == true)
                    {
                        MessageBox.Show("This forum has been closed. You can not add new comments.");
                    }
                }

                string location = selectedForum.Location;
                string[] locationParts = location.Split(new[] { ", " }, StringSplitOptions.None);
                string country = locationParts[0];
                string city = locationParts[1];

                Location location1 = _locationService.FindLocation(city, country);

                bool mine = false;
                List<ForumDTO> myForums= GetAllMyForums(LoggedInUser.Id);
                ForumDTO thisForum = myForums.FirstOrDefault(a => a.Id == selectedForum.Id);
                if (thisForum != null)
                {
                    mine = true;
                }
                else
                {
                    mine = false;
                }

                bool hasVisitedLocation = _guestReservationService.HasGuestVisitedLocation(location1.Id);

                var data = new Forum
                {

                    Location = new Location() { Id = location1.Id },
                    User = new User() { Id = LoggedInUser.Id },
                    IsForumMine = mine,
                    ForumComment = newComment,
                    HasBeenVisited = hasVisitedLocation,
                    IsForumClosed = false
                };

                _forumRepository.Save(data);


                MessageBox.Show("Your comment has been successfully added.");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw;
            }
        }

        public void AddNewCommentOwner(ForumDTO selectedForum, string newComment)
        {
            try
            {

                string location = selectedForum.Location;
                string[] locationParts = location.Split(new[] { ", " }, StringSplitOptions.None);
                string country = locationParts[0];
                string city = locationParts[1];

                Location location1 = _locationService.FindLocation(city, country);
                // Kreiranje objekta Forum
                var forum = new Forum
                {
                    Location = new Location() { Id = location1.Id },
                    User = new User() { Id = LoggedInUser.Id },
                    IsForumMine = false,
                    ForumComment = newComment,
                    HasBeenVisited = true,
                    IsForumClosed = false
                };

                // Čuvanje komentara u bazi
                _forumRepository.Save(forum);

                MessageBox.Show("Your comment has been successfully added.");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw;
            }
        }

      

    }
}
