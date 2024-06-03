using BookingApp.Model;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BookingApp.Service.AccommodationServices;
using Microsoft.Win32;
using System.Diagnostics;

namespace BookingApp.WPF.ViewModel.OwnerViewModel
{
    public class AccommodationReportViewModel
    {
     
         private readonly AccommodationRateService _accommodationRateService;

         public AccommodationReportViewModel()
         {
             _accommodationRateService = new AccommodationRateService(); // Inicijalizacija servisa
         }

         public void GenerateReport()
         {
             var accommodationRatings = new List<AccommodationRating>
             {
                 new AccommodationRating { Name = "Sheraton", CleanlinessRating = 4.2, OwnerRateRating = 4.0 },
                 new AccommodationRating { Name = "Allegro", CleanlinessRating = 4.5, OwnerRateRating = 4.2 },
                 new AccommodationRating { Name = "Maria", CleanlinessRating = 4.0, OwnerRateRating = 3.8 },
                 new AccommodationRating { Name = "Sara", CleanlinessRating = 4.5, OwnerRateRating = 4.0 },
                 new AccommodationRating { Name = "Harry's", CleanlinessRating = 4.2, OwnerRateRating = 3.5 },
                 new AccommodationRating { Name = "Lena", CleanlinessRating = 4.8, OwnerRateRating = 4.5 },
                 new AccommodationRating { Name = "Anamana", CleanlinessRating = 3.9, OwnerRateRating = 3.0 },
                 new AccommodationRating { Name = "Ana", CleanlinessRating = 4.3, OwnerRateRating = 4.2 },
                 new AccommodationRating { Name = "La vie", CleanlinessRating = 4.1, OwnerRateRating = 3.7 },
                 new AccommodationRating { Name = "Hotel Moskva", CleanlinessRating = 4.4, OwnerRateRating = 4.1 },
                 new AccommodationRating { Name = "Silvia", CleanlinessRating = 4.0, OwnerRateRating = 3.9 },
                 new AccommodationRating { Name = "Hilton", CleanlinessRating = 4.2, OwnerRateRating = 4.0 },
                 new AccommodationRating { Name = "Peninsula", CleanlinessRating = 4.7, OwnerRateRating = 4.5 },
                 new AccommodationRating { Name = "Mala", CleanlinessRating = 3.8, OwnerRateRating = 3.2 },
                 new AccommodationRating { Name = "Ikea", CleanlinessRating = 4.6, OwnerRateRating = 4.3 },
                 new AccommodationRating { Name = "Anci", CleanlinessRating = 4.0, OwnerRateRating = 3.8 },
                 new AccommodationRating { Name = "Heaven", CleanlinessRating = 3.5, OwnerRateRating = 3.1 }
             };

             // Generisanje PDF izveštaja
             CreatePdfReport(accommodationRatings);
         }

        private void CreatePdfReport(List<AccommodationRating> accommodationRatings)
        {
            // Korišćenje FileDialog-a za izbor lokacije za čuvanje PDF-a
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PDF file (.pdf)|.pdf";
            saveFileDialog.Title = "Save PDF File";
            saveFileDialog.FileName = "AccommodationRatingsReport.pdf";

            // Provera da li je korisnik odabrao lokaciju za čuvanje
            if (saveFileDialog.ShowDialog() == true)
            {
                // Putanja do PDF fajla
                string path = saveFileDialog.FileName;

                // Kreiranje PDF dokumenta
                using (var doc = new Document())
                {
                    // Kreiranje PdfWriter objekta i otvaranje dokumenta
                    PdfWriter.GetInstance(doc, new FileStream(path, FileMode.Create));
                    doc.Open();

                    // Dodavanje naslova
                    var title = new Paragraph("Accommodation Ratings Report", new Font(Font.FontFamily.HELVETICA, 24, Font.BOLD));
                    title.Alignment = Element.ALIGN_CENTER;
                    doc.Add(title);
                    doc.NewPage();

                    // Dodavanje tabele sa podacima
                    var table = new PdfPTable(4);
                    table.WidthPercentage = 100f;

                    // Kolone tabele
                    table.AddCell("Accommodation Name");
                    table.AddCell("Cleanliness Rating");
                    table.AddCell("Owner Rate Rating");
                    table.AddCell("Total Rating");

                    // Popunjavanje redova sa podacima
                    foreach (var rating in accommodationRatings)
                    {
                        table.AddCell(rating.Name);
                        table.AddCell(rating.CleanlinessRating.ToString("F2"));
                        table.AddCell(rating.OwnerRateRating.ToString("F2"));

                        // Računanje ukupne prosečne ocene
                        double totalRating = (rating.CleanlinessRating + rating.OwnerRateRating) / 2;
                        table.AddCell(totalRating.ToString("F2"));
                    }

                    doc.Add(table);
                    doc.Close();
                }

                // Otvori PDF fajl u web pregledaču
                Process.Start(new ProcessStartInfo(path) { UseShellExecute = true });
            }
        }

        /* private readonly AccommodationRateService _accommodationRateService;

      public AccommodationReportViewModel()
      {
          _accommodationRateService = new AccommodationRateService(); // Inicijalizacija servisa
      }

      public void GenerateReport()
      {
          // Dobijamo sve ocene smeštaja iz servisa
          List<AccommodationRate> accommodationRates = _accommodationRateService.GetAll();

          // Grupisanje ocena po imenu smeštaja i izračunavanje prosečnih ocena
          var accommodationRatings = accommodationRates
                  .GroupBy(r => r.Reservation.Accommodation.Name)
                  .Select(group => new AccommodationRating
                  {
                      Name = group.Key,
                      CleanlinessRating = group.Any() ? group.Average(r => r.Cleanliness) : 0, // Ako nema ocena, postavite prosečnu ocenu na 0
                      OwnerRateRating = group.Any() ? group.Average(r => r.OwnerRate) : 0 // Ako nema ocena, postavite prosečnu ocenu na 0
                  })
                  .ToList();


          // Generisanje PDF izveštaja
          CreatePdfReport(accommodationRatings);
      }

      private void CreatePdfReport(List<AccommodationRating> accommodationRatings)
      {
          string downloadsPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
          string fileName = "AccommodationRatingsReport.pdf";
          string path = Path.Combine(downloadsPath, "Downloads", fileName);

          Document doc = new Document();
          PdfWriter.GetInstance(doc, new FileStream(path, FileMode.Create));
          doc.Open();

          // Dodajemo naslov
          Paragraph title = new Paragraph("Accommodation Ratings Report", new Font(Font.FontFamily.HELVETICA, 24, Font.BOLD));
          title.Alignment = Element.ALIGN_CENTER;
          doc.Add(title);
          doc.NewPage();

          // Dodajemo tabelu sa podacima
          PdfPTable table = new PdfPTable(4);
          table.WidthPercentage = 100f;

          // Kolone tabele
          table.AddCell("Accommodation Name");
          table.AddCell("Cleanliness Rating");
          table.AddCell("Owner Rate Rating");
          table.AddCell("Total Rating");

          // Popunjavamo redove sa podacima
          foreach (var rating in accommodationRatings)
          {
              table.AddCell(rating.Name);
              table.AddCell(rating.CleanlinessRating.ToString("F2"));
              table.AddCell(rating.OwnerRateRating.ToString("F2"));

              // Računamo ukupnu prosečnu ocenu
              double totalRating = (rating.CleanlinessRating + rating.OwnerRateRating) / 2;
              table.AddCell(totalRating.ToString("F2"));
          }

          doc.Add(table);
          doc.Close();
      }*/
    }
}
