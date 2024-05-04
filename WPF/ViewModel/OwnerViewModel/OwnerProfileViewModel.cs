using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.ComponentModel;

namespace BookingApp.WPF.ViewModel.OwnerViewModel
{
    public class OwnerProfileViewModel : INotifyPropertyChanged
    {
       
        private OwnerRepository _ownerRepository;
        public event EventHandler RequestClose;


        public OwnerProfileViewModel()
        {
            _ownerRepository = new OwnerRepository();
            LoadOwnerData();
        }

        private void LoadOwnerData()
        {
            Owner loggedInUser = GetOwnerByLoggedInUserId();
            if (loggedInUser != null)
            {
                OwnerName = loggedInUser.Name;
                OwnerSurname = loggedInUser.Surname;
                OwnerPhone = loggedInUser.PhoneNumber;
                OwnerEmail = loggedInUser.Email;
                SuperOwner = loggedInUser.Super;
            }
        }

        public Owner GetOwnerByLoggedInUserId()
        {
            int loggedInUserId = LoggedInUser.Id;
            return _ownerRepository.GetOwnerById(loggedInUserId);
        }

 
        private string _ownerName;
        public string OwnerName
        {
            get { return _ownerName; }
            set
            {
                _ownerName = value;
                OnPropertyChanged(nameof(OwnerName));
            }
        }

        private string _ownerSurname;
        public string OwnerSurname
        {
            get { return _ownerSurname; }
            set
            {
                _ownerSurname = value;
                OnPropertyChanged(nameof(OwnerSurname));
            }
        }

        private string _ownerPhone;
        public string OwnerPhone
        {
            get { return _ownerPhone; }
            set
            {
                _ownerPhone = value;
                OnPropertyChanged(nameof(OwnerPhone));
            }
        }

        private string _ownerEmail;
        public string OwnerEmail
        {
            get { return _ownerEmail; }
            set
            {
                _ownerEmail = value;
                OnPropertyChanged(nameof(OwnerEmail));
            }
        }

        private bool _superOwner;
        public bool SuperOwner
        {
            get { return _superOwner; }
            set
            {
                _superOwner = value;
                OnPropertyChanged(nameof(SuperOwner));
            }
        }

        public void LogOut()
        {
            LoggedInUser.Reset();
            RequestClose?.Invoke(this, EventArgs.Empty);
        }



        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
