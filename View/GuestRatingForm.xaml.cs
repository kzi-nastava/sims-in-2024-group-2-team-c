using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;


namespace BookingApp.View
{
    /// <summary>
    /// Interaction logic for GuestRatingForm.xaml
    /// </summary>
    public partial class GuestRatingForm : Window
    {

        private ReservationRepository reservationRepository; //mislim da mi samo guest rep treba
        private GuestRatingRepository guestRatingRepository;
        private GuestRepository guestRepository; //mozda readonly
        private AccommodationRepository accommodationRepository;
        private Owner owner;
        private Reservation selectedReservation;


        public GuestRatingForm()
        {
            InitializeComponent();
            reservationRepository = new ReservationRepository();
            reservationRepository.LoadReservationsFromCSV("../../../Resources/Data/reservations.csv");
            guestListView.ItemsSource = reservationRepository.Reservations;
            guestRatingRepository = new GuestRatingRepository();
            guestRepository = new GuestRepository();
            accommodationRepository = new AccommodationRepository();
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (guestListView.SelectedItem != null)
            {
               selectedReservation = (Reservation)guestListView.SelectedItem;
            }
        }



        private void SaveGuestRating(object sender, RoutedEventArgs e)
        {
            if (selectedReservation == null)
            {
                MessageBox.Show("Please select a reservation.");
                return;
            }

            Guest guest = guestRepository.GetGuestById(selectedReservation.Guest.Id);
            int cleanliness = int.Parse(txtCleanliness.Text);
            if (cleanliness<1 || cleanliness > 5)
            {
                MessageBox.Show("Please, enter a number between 1 and 5 for Cleanliness");
                return;
            }
            int ruleRespecting = int.Parse(txtRuleRespecting.Text);
            if(ruleRespecting<1 || ruleRespecting > 5)
            {
                MessageBox.Show("Please, enter a number between 1 and 5 for Rule Respecting");
                return;
            }
            string comment = txtComment.Text.Trim().ToLower();
            DateTime ratingDate = DateTime.Now;
            if(ratingDate < selectedReservation.DepartureDate)
            {
                MessageBox.Show("Guest didn't leave the accommodation. You can not give a feedback!");
                return;
            } else if (ratingDate > selectedReservation.DepartureDate.AddDays(5))
            {
                MessageBox.Show("More than 5 days passed. You can not rate this guest!");
                return;
            }
           


            GuestRating newGuestRating = new GuestRating()
            {
                Guest = guest, 
                Cleanliness = cleanliness,
                RuleRespecting = ruleRespecting,
                Comment = comment,
                RatingDate = ratingDate,

            };

         


           /* TimeSpan timeSinceDeparture = todaysDate - selectedReservation.DepartureDate;
            if (timeSinceDeparture.Days < 5) 
            {
                // Slanje e-pošte vlasniku smeštaja
                string ownerEmail = GetOwnerEmail(selectedReservation.Accommodation.Id); // Pretpostavljamo da imamo metodu za dobavljanje e-pošte vlasnika smeštaja
                if (!string.IsNullOrEmpty(ownerEmail))
                {
                    SendEmail(ownerEmail, "Podsetnik za ocenjivanje gosta", "Molimo vas da ocenite gosta koji je nedavno boravio u vašem smeštaju.");
                }
                else
                {
                    MessageBox.Show("E-mail adresa vlasnika smeštaja nije pronađena.");
                }
            }*/


            guestRatingRepository.Save(newGuestRating);


            MessageBox.Show("Guest rating saved successfully.");

            Close();
        }

     
        // Metoda za dobavljanje emaila vlasnika smeštaja na osnovu ID smeštaja
       /* public string GetOwnerEmail(int accommodationId)
        {
            // Pronalaženje smeštaja na osnovu ID-a
            Accommodation accommodation = accommodationRepository.GetAccommodationById(accommodationId);

            // Provera da li je smeštaj pronađen
            if (accommodation != null)
            {
                // Vraćanje emaila vlasnika smeštaja
                return accommodation.Owner.Email;
            }

            // Ako smeštaj nije pronađen, možete postaviti neku podrazumevanu vrednost ili baciti izuzetak
            return "DefaultOwnerEmail@example.com";
        }



        private void SendEmail(string recipient, string subject, string body)
    {
        try
        {
            // Postavke SMTP servera za slanje e-pošte
            string smtpHost = "smtp.example.com"; // Postavite SMTP server
            int smtpPort = 587; // Postavite SMTP port
            string smtpUsername = "your_username"; // Postavite korisničko ime za SMTP autentifikaciju
            string smtpPassword = "your_password"; // Postavite lozinku za SMTP autentifikaciju

            // Kreiranje klijentskog objekta za slanje e-pošte
            SmtpClient smtpClient = new SmtpClient(smtpHost, smtpPort);
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
            smtpClient.EnableSsl = true; 

            // Kreiranje e-pošte
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(smtpUsername);
            mailMessage.To.Add(new MailAddress(recipient));
            mailMessage.Subject = subject;
            mailMessage.Body = body;

            // Slanje e-pošte
            smtpClient.Send(mailMessage);

            MessageBox.Show("E-mail successfully sent.");
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Failed to send e-mail: {ex.Message}");
        }
    }
       */

}
}
