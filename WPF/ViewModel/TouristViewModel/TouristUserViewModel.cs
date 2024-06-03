using BookingApp.Model;
using BookingApp.Service.TourServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Diagnostics;
using BookingApp.DTO;
using System.Windows.Media.Imaging;
using System.Drawing.Imaging;


namespace BookingApp.WPF.ViewModel.TouristViewModel
{
    public class TouristUserViewModel : ViewModelBase
    {

        private readonly MainViewModel _mainViewModel;
        private readonly TouristService _touristService;
        private readonly AllTouristsToursService _allToursService;
        private readonly TourInstanceService _tourInstanceService;

        private string _username;
        public string Username
        {
            get { return _username; }
            set
            {

                _username = value;
                OnPropertyChanged(nameof(Username));
                


            }
        }

        private string _name;

        public string Name
        {
            get { return _name; }
            set
            {

                _name = value;
                OnPropertyChanged(nameof(Name));



            }
        }


        private string _lastName;
        public string LastName
        {

            get { return _lastName; }
            set
            {

                _lastName = value;
                OnPropertyChanged(nameof(LastName));



            }
        }

        public ViewModelCommandd CreateTourCommand { get; }
        public ViewModelCommandd DownloadCommand { get; }

        public TouristUserViewModel() {
            _mainViewModel = LoggedInUser.mainViewModel;
            Username = LoggedInUser.Username;

            _touristService = new TouristService();
            _allToursService = new AllTouristsToursService();
            _tourInstanceService = new TourInstanceService();

            Name = _touristService.GetFirstName(LoggedInUser.Id);
            LastName = _touristService.GetLastName(LoggedInUser.Id);

            CreateTourCommand = new ViewModelCommandd(ExecuteCreateTourCommand);
            DownloadCommand = new ViewModelCommandd(ExecuteGeneratePdfCommand);


        }


        public void ExecuteCreateTourCommand(object obj)
        {

            _mainViewModel.ExecuteRequestCreation(obj);
        }



        public void ExecuteGeneratePdfCommand(object obj)
        {

            List<TouristPdfDTO> tours = _allToursService.GetAllToursInYear();
            GeneratePdfForTours(tours);
            
        }


        public void GeneratePdfForTours(List<TouristPdfDTO> tours)
        {
            // Create a new document for all tours
            string downloadsPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string fileName = "TouristInfo.pdf";
            string path = Path.Combine(downloadsPath, "Downloads", fileName);

            // Create a Document object
            Document doc = new Document();

            // Create a PdfWriter to write to the specified output path
            PdfWriter.GetInstance(doc, new FileStream(path, FileMode.Create));


            // Open the document for writing
            doc.Open();

            Paragraph title = new Paragraph("Report on all tour attendances in 2023", new Font(Font.FontFamily.HELVETICA, 24, Font.BOLD));
            title.Alignment = Element.ALIGN_CENTER;
            doc.Add(title);
            doc.NewPage();

            foreach (var tour in tours)
            {
                
                GeneratePdfPageForTour(doc, tour );

                // Add a new page for the next tour
                doc.NewPage();
            }

            // Close the document after adding all tours
            doc.Close();
        }



        public void GeneratePdfPageForTour(Document doc, TouristPdfDTO tour)
        {
            string tourName = tour.Name;
            string tourDescription = tour.Description;
            string tourLocation = tour.Location;
            string tourLanguage = tour.Language;
            string tourDate = tour.Date.ToString("dd.MM.yyyy. HH:mm:ss");
            string Duration = tour.Duration.ToString();
            List<string> images = tour.Images; 


            
            Paragraph titleFirstPage = new Paragraph(tourName, new Font(Font.FontFamily.HELVETICA, 16, Font.BOLD));
            titleFirstPage.Alignment = Element.ALIGN_CENTER;
            titleFirstPage.SpacingBefore = 20f;
            titleFirstPage.SpacingAfter = 20f;
            doc.Add(titleFirstPage);
            Paragraph description = new Paragraph(tourDescription, new Font(Font.FontFamily.HELVETICA, 12, Font.BOLDITALIC));
            description.Alignment = Element.ALIGN_CENTER;
            description.SpacingBefore = 10f;
            description.SpacingAfter = 10f;
            doc.Add(description);

            Paragraph location = new Paragraph("Location:", new Font(Font.FontFamily.HELVETICA, 12, Font.BOLD));
            location.SpacingBefore = 10f;
            location.SpacingAfter = 5f;
            doc.Add(location);

            Paragraph locationText = new Paragraph(tourLocation, new Font(Font.FontFamily.HELVETICA, 12, Font.NORMAL));
            locationText.SpacingBefore = 5f;
            locationText.SpacingAfter = 10f;
            doc.Add(locationText);

            Paragraph language = new Paragraph("Language:", new Font(Font.FontFamily.HELVETICA, 12, Font.BOLD));
            language.SpacingBefore = 10f;
            language.SpacingAfter = 5f;
            doc.Add(language);

            Paragraph languageText = new Paragraph(tourLanguage, new Font(Font.FontFamily.HELVETICA, 12, Font.NORMAL));
            languageText.SpacingBefore = 5f;
            languageText.SpacingAfter = 10f;
            doc.Add(languageText);


            Paragraph date = new Paragraph("Date:", new Font(Font.FontFamily.HELVETICA, 12, Font.BOLD));
            date.SpacingBefore = 10f;
            date.SpacingAfter = 5f;
            doc.Add(date);

            Paragraph dateText = new Paragraph(tourDate, new Font(Font.FontFamily.HELVETICA, 12, Font.NORMAL));
            dateText.SpacingBefore = 5f;
            dateText.SpacingAfter = 10f;
            doc.Add(dateText);

            Paragraph duration = new Paragraph("Duration:", new Font(Font.FontFamily.HELVETICA, 12, Font.BOLD));
            duration.SpacingBefore = 10f;
            duration.SpacingAfter = 5f;
            doc.Add(duration);

            Paragraph durationText = new Paragraph(Duration, new Font(Font.FontFamily.HELVETICA, 12, Font.NORMAL));
            durationText.SpacingBefore = 5f;
            durationText.SpacingAfter = 30f;
            doc.Add(durationText);



            doc.Add(new Paragraph('\n'));
            doc.Add(new Paragraph('\n'));

            PdfPTable table = new PdfPTable(2);
            table.WidthPercentage = 100f; 
            string directoryPath = @"D:\Mila\HAHHHAHAAH\sims-in-2024-group-2-team-c\Resources\Images";
            
            int count = 0;
            PdfPCell cell = null;
            foreach (var imageName in images)
            {
                string imagePath = Path.Combine(directoryPath, imageName);

                if (File.Exists(imagePath))
                {
                    iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(imagePath);
                    img.ScaleToFit(200f, 200f);

                    
                    cell = new PdfPCell(img);
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Padding = 5f; 
                    table.AddCell(cell);

                    count++;
                    
                    if (count % 2 == 0)
                    {
                        table.CompleteRow();
                    }
                }
            }

            doc.Add(table);

           
        }



    }
}
