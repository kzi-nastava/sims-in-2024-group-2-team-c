using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.WPF.View.OwnerView;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace BookingApp.View
{
    /// <summary>
    /// Interaction logic for SignInForm.xaml
    /// </summary>
    public partial class SignInForm : Window
    {

        private readonly UserRepository _repository;

        private string _username;
        public string Username
        {
            get => _username;
            set
            {
                if (value != _username)
                {
                    _username = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public SignInForm()
        {
            InitializeComponent();
            DataContext = this;
            _repository = new UserRepository();
        }

        private void SignIn(object sender, RoutedEventArgs e)
        {
            User user = _repository.GetByUsername(Username);
            if (user != null)
            {
                if(user.Password == txtPassword.Password)
                {

                    OwnerWindow ownerWindow = new OwnerWindow();
                    AccommodationOverview accommodationOverview = new AccommodationOverview();
                    TourOverview tourOverview = new TourOverview();
                    Window1 w1 = new Window1();


                    LoggedInUser.Id = user.Id;
                    LoggedInUser.Username = user.Username;
                    LoggedInUser.Role = user.Role.ToString();


                    if (user.Role == UserRole.owner)
                    {
                        ownerWindow.Show();
                    }
                    else if (user.Role == UserRole.guest)
                    {
                        accommodationOverview.Show();
                    }
                    else if (user.Role == UserRole.guide)
                    {
                        tourOverview.Show();
                    }
                    else if (user.Role == UserRole.tourist)
                    {
                        w1.Show();
                    }
                    Close();




                } 
                else
                {
                    MessageBox.Show("Wrong password!");
                }
            }
            else
            {
                MessageBox.Show("Wrong username!");
            }
            
        }
    }
}
