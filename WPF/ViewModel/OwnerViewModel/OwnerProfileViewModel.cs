using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Service.OwnerService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.ViewModel.OwnerViewModel
{
    public class OwnerProfileViewModel : INotifyPropertyChanged
    {
        private Owner _owner;
        private string _ownerName;
        private string _ownerSurname;
        private string _ownerType;
        public string OwnerUsername;
        private SuperOwnerService _superOwnerService;
        private OwnerRepository _ownerRepository;
        public string OwnerType
        {
            get { return _ownerType; }
            set
            {
                _ownerType = value;
                OnPropertyChanged(nameof(OwnerType));
            }
        }

        public string OwnerName
        {
            get { return _ownerName; }
            set
            {
                _ownerName = value;
                OnPropertyChanged(nameof(OwnerName));
            }
        }

        public string OwnerSurname
        {
            get { return _ownerSurname; }
            set
            {
                _ownerSurname = value;
                OnPropertyChanged(nameof(OwnerSurname));
            }
        }


        /*public OwnerProfileViewModel(SuperOwnerService superOwnerService, OwnerRepository ownerRepository)
        {
            _superOwnerService = superOwnerService;
            _ownerRepository = ownerRepository;
            int ownerId = LoggedInUser.Id;
            _owner = _ownerRepository.GetOwnerByLoggedInUserId(ownerId);
            //_owner = _superOwnerService.GetOwnerById(ownerId);
            OwnerUsername = LoggedInUser.Username;
           // SetOwnerUsername(ownerId);
            SetOwnerType(ownerId);
            //OwnerUsername = _owner.Username;
           
           
        }
         private void SetOwnerType(int ownerId)
        {
            
            Owner owner = _superOwnerService.GetOwnerById(ownerId);

         
            if (owner != null && owner.Super)
            {
                OwnerType = "SuperOwner";
            }
            else
            {
                OwnerType = "Owner";
            }
        }
         
         */

        public OwnerProfileViewModel(OwnerRepository ownerRepository)
        {
            _ownerRepository = ownerRepository;
            int ownerId = LoggedInUser.Id;
            _owner = _ownerRepository.GetOwnerById(ownerId);
            OwnerUsername = LoggedInUser.Username;
            SetOwnerType();
            OwnerName = _owner.Name;
            OwnerSurname = _owner.Surname;
        }

        private void SetOwnerType()
        {
            if (_owner != null && _owner.Super)
            {
                OwnerType = "SuperOwner";
            }
            else
            {
                OwnerType = "Owner";
            }
        }

        private void SetOwnerUsername(int ownerId)
        {
            Owner owner = _superOwnerService.GetOwnerById(ownerId);

            if (owner != null)
            {
                OwnerUsername = owner.Username;
            }
            else
            {
                OwnerUsername = "Unknown";
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
