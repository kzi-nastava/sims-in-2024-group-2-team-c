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
using iTextSharp.text.pdf.draw;
using Microsoft.Win32;


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


        /* public void GeneratePdfForTours(List<TouristPdfDTO> tours)
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

             LineSeparator line = new LineSeparator(1f, 100f, BaseColor.BLACK, Element.ALIGN_CENTER, -2);
             doc.Add(new Chunk(line));
             doc.Add(new Paragraph('\n'));

             foreach (var tour in tours)
             {
                 List<KeyPoint> keypoints = _allToursService.GetKeyPoints(tour.KeyPointIds);

                 GeneratePdfPageForTour(doc, tour,keypoints );

                 // Add a new page for the next tour
                 //doc.NewPage();
                 LineSeparator newLine = new LineSeparator(1f, 100f, BaseColor.BLACK, Element.ALIGN_CENTER, -2);
                 doc.Add(new Chunk(newLine));
                 doc.Add(new Paragraph('\n'));
                 doc.NewPage();
             }

             // Close the document after adding all tours
             doc.Close();
         }*/

        public void GeneratePdfForTours(List<TouristPdfDTO> tours)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PDF file (*.pdf)|*.pdf";
            saveFileDialog.Title = "Save PDF File";
            saveFileDialog.FileName = "TouristInfo - MyTourApp.pdf";

            if (saveFileDialog.ShowDialog() == true)
            {
                string path = saveFileDialog.FileName;

                Document doc = new Document();
                PdfWriter.GetInstance(doc, new FileStream(path, FileMode.Create));
                doc.Open();

                Paragraph title = new Paragraph("Report on all tour attendances in 2023", new Font(Font.FontFamily.HELVETICA, 24, Font.BOLD))
                {
                    Alignment = Element.ALIGN_CENTER
                };
                doc.Add(title);

                LineSeparator line = new LineSeparator(1f, 100f, BaseColor.BLACK, Element.ALIGN_CENTER, -2);
                doc.Add(new Chunk(line));
                doc.Add(new Paragraph('\n'));

                foreach (var tour in tours)
                {
                    List<KeyPoint> keypoints = _allToursService.GetKeyPoints(tour.KeyPointIds);
                    GeneratePdfPageForTour(doc, tour, keypoints);

                    LineSeparator newLine = new LineSeparator(1f, 100f, BaseColor.BLACK, Element.ALIGN_CENTER, -2);
                    doc.Add(new Chunk(newLine));
                    doc.Add(new Paragraph('\n'));
                    doc.NewPage();
                }

                Paragraph end = new Paragraph("MyTourApp", new Font(Font.FontFamily.HELVETICA, 24, Font.BOLD))
                {
                    Alignment = Element.ALIGN_CENTER
                };
                doc.Add(end);

                doc.Close();

                // Open the saved PDF in the default viewer
                Process.Start(new ProcessStartInfo(path) { UseShellExecute = true });
            }
        }




        public void GeneratePdfPageForTour(Document doc, TouristPdfDTO tour, List<KeyPoint> keypoints)
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

            Paragraph location = new Paragraph($"Location:{tourLocation}", new Font(Font.FontFamily.HELVETICA, 12, Font.NORMAL));
            location.SpacingBefore = 10f;
            location.SpacingAfter = 10f;
            doc.Add(location);

            

            Paragraph language = new Paragraph($"Language:{tourLanguage}", new Font(Font.FontFamily.HELVETICA, 12, Font.NORMAL));
            language.SpacingBefore = 10f;
            language.SpacingAfter = 10f;
            doc.Add(language);

            
            Paragraph date = new Paragraph($"Date:{tourDate}", new Font(Font.FontFamily.HELVETICA, 12, Font.NORMAL));
            date.SpacingBefore = 10f;
            date.SpacingAfter = 10f;
            doc.Add(date);

            Paragraph duration = new Paragraph($"Duration:{Duration}", new Font(Font.FontFamily.HELVETICA, 12, Font.NORMAL));
            duration.SpacingBefore = 10f;
            duration.SpacingAfter = 10f;
            doc.Add(duration);


            Paragraph keys = new Paragraph($"Key Points: ", new Font(Font.FontFamily.HELVETICA, 12, Font.BOLDITALIC));
            keys.SpacingBefore = 10f;
            keys.SpacingAfter = 5f;
            doc.Add(keys);

            int number = 1;
            foreach(KeyPoint keyPoint in keypoints)
            {
                string keyName = keyPoint.Name;
                string desc = keyPoint.Description;
                string numberString = number.ToString();
                Paragraph keypoint = new Paragraph($"{numberString}. {keyName} ", new Font(Font.FontFamily.HELVETICA, 12, Font.BOLD));
                Paragraph keyPoint2 = new Paragraph($"{desc}", new Font(Font.FontFamily.HELVETICA, 12, Font.NORMAL));
                keypoint.SpacingAfter = 5f;
                keypoint.SpacingAfter = 5f;
                keyPoint2.SpacingAfter = 10f;
                doc.Add(keypoint);
                doc.Add(keyPoint2);
                number += 1;

            }




            doc.Add(new Paragraph('\n'));
            doc.Add(new Paragraph('\n'));

            PdfPTable table = new PdfPTable(2);
            table.WidthPercentage = 100f; 
            string directoryPath = @"D:\Mila\HAHHHAHAAH\sims-in-2024-group-2-team-c\Resources\Images";


          /*  string baseImagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Images");
            // string imagePath = Path.Combine("D:\\Mila\\AHHHHHHHHHHHH\\sims-in-2024-group-2-team-c\\Resources\\Images\\", imageName);
            // string imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Images", imageName);
            string path = Directory.GetCurrentDirectory();
            Console.WriteLine(path);
            //string imagePath = Path.Combine(@"..\\Resources\\Images\\", imageName);

            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

            // Find the index of the substring "\bin\Debug\net6.0-windows\"
            int index = baseDirectory.IndexOf("\\bin\\Debug\\net6.0-windows\\", StringComparison.OrdinalIgnoreCase);

            // Remove the substring "\bin\Debug\net6.0-windows\" from the base directory
            string directoryPath = baseDirectory.Remove(index);

            
            */

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
