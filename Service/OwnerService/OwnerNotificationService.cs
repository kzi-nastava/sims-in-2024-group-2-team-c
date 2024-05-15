using BookingApp.Interfaces;
using BookingApp.Injector;
using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.DTO;
using static System.Net.Mime.MediaTypeNames;
using BookingApp.Repository;

namespace BookingApp.Service.OwnerService
{
    public class OwnerNotificationService
    {
        private readonly IOwnerNotificationRepository _ownerNotificationRepository;
        private readonly OwnerRepository  ownerRepository;
        public OwnerNotificationService()
        {
            _ownerNotificationRepository = Injectorr.CreateInstance<IOwnerNotificationRepository>();
            ownerRepository = new OwnerRepository();
        }
        public IEnumerable<OwnerNotification> GetOwnerNotifications(int id)
        {
            return _ownerNotificationRepository.GetAllByOwnerId(id);
        }

        public void save(GuestReservationDTO selectedReservation, string textMessage)
        {
            int ownerId = ownerRepository.GetOwnerIdByOwnerUsername(selectedReservation.OwnerUsername);
            var data = new OwnerNotification
            {
                OwnerId = ownerId,
                Text = textMessage
             };
            _ownerNotificationRepository.Save(data);
        }
    }
}
