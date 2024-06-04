using BookingApp.Commands;
using BookingApp.DTO;
using BookingApp.Service.TourServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.Windows.Navigation;
using BookingApp.Model;
using System.IO;
//using System.Reflection.Metadata;
//using System.Xml.Linq;
//using System.DirectoryServices.ActiveDirectory;
//using System.Windows.Documents;
using iTextSharp.text;
using iTextSharp.text.pdf;


namespace BookingApp.WPF.ViewModel.GuideViewModel
{
    class TourStatistic_ViewModel : ViewModelBase
    {
        private ObservableCollection<TourStatisticDTO> _endedTours;
        public ObservableCollection<TourStatisticDTO> EndedTours
        {
            get { return _endedTours; }
            set
            {
                if (_endedTours != value)
                {
                    _endedTours = value;
                    OnPropertyChanged(nameof(EndedTours));
                }
            }
        }

        private TourStatisticDTO _selectedTour;
        public TourStatisticDTO SelectedTour
        {
            get { return _selectedTour; }
            set
            {
                if (_selectedTour != value)
                {
                    _selectedTour = value;
                    OnPropertyChanged(nameof(SelectedTour));
                }
            }
        }

        private readonly EndedToursService _endedToursService;
        private readonly TourService _tourService;
        private readonly TourInstanceService _tourInstanceService;
        private readonly KeyPointService _keyPointService;
        private readonly PeopleInfoService _peopleInfoService;

        //public ICommand ShowCommand { get; private set; }
        //public ICommand BackCommand { get; private set; }
        public ICommand GenerateReportCommand { get;  }

        public TourStatistic_ViewModel()
        {
            _endedToursService = new EndedToursService();
            _tourService = new TourService();
            _tourInstanceService = new TourInstanceService();
            _keyPointService = new KeyPointService();
            _peopleInfoService = new PeopleInfoService();
            GenerateReportCommand = new ViewModelCommandd(Generate);
            //ShowCommand = new Commands.RelayCommand(ShowExecute);
            //BackCommand = new Commands.RelayCommand(BackExecute);
            LoadEndedTours();
        }

        private void Generate(object obj)
        {
            string path = @"C:\Users\Korisnik\Desktop\SIMS HCI\";
            string outputPath= path + SelectedTour.Name.ToString() + ".pdf";
            GenerateTourReport(SelectedTour,outputPath);
        }

        /*private void BackExecute()
        {
            NavigationService?.GoBack();
        }*/

        /*private void ShowExecute()
        {
            if (SelectedTour != null)
            {
                
                MaxTouristsBlock.DataContext = SelectedTour;
                ReservedTouristsBlock.DataContext = SelectedTour;
                PresentTouristsBlock.DataContext = SelectedTour;

                LessBlock.DataContext = SelectedTour;
                BetweenBlock.DataContext = SelectedTour;
                MoreBlock.DataContext = SelectedTour;
            }
           else
            {
                MessageBox.Show("Please select a tour.", "Tour Statistics", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }*/

        private void LoadEndedTours()
        {
            EndedTours = new ObservableCollection<TourStatisticDTO>(_endedToursService.GetEndedTours());
        }
        public void GenerateTourReport(TourStatisticDTO tourStatistic, string outputPath)
        {
            using (FileStream fs = new FileStream(outputPath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                Document document = new Document();
                PdfWriter writer = PdfWriter.GetInstance(document, fs);
                document.Open();

                // Naslov
                Paragraph title = new Paragraph("Tour Report")
                {
                    Alignment = Element.ALIGN_CENTER
                };
                title.Font.Size = 20;
                document.Add(title);

                // Informacije o turi
                document.Add(new Paragraph($"Tour Name: {tourStatistic.Name}"));
                document.Add(new Paragraph($"Description: {tourStatistic.Description}"));
                document.Add(new Paragraph($"Location: {tourStatistic.Location}"));
                document.Add(new Paragraph($"Language: {tourStatistic.Language}"));
                document.Add(new Paragraph($"Duration: {tourStatistic.Duration} minutes"));
                document.Add(new Paragraph($"Date: {tourStatistic.Date.ToString("dd-MM-yyyy")}"));

                // Statistike o turistima
                document.Add(new Paragraph($"Maximum Number of Tourists: {tourStatistic.MaxTourists}"));
                document.Add(new Paragraph($"Reserved Tourists: {tourStatistic.ReservedTourists}"));
                document.Add(new Paragraph($"Present Tourists: {tourStatistic.PresentTourists}"));
                document.Add(new Paragraph($"Tourists under 18: {tourStatistic.LessThan18}"));
                document.Add(new Paragraph($"Tourists between 18 and 50: {tourStatistic.Between18And50}"));
                document.Add(new Paragraph($"Tourists over 50: {tourStatistic.MoreThan50}"));
                document.Add(new Paragraph($"Attendance: {tourStatistic.Attendence * 100}%"));
                document.Add(new Paragraph($"Key Point: {tourStatistic.KeyPoint}"));

                // Lista turista
                document.Add(new Paragraph("List of Tourists:"));
                List<PeopleInfo> touristList = GetListOfTourists(tourStatistic.TourInstanceId);

                int i = 1;
                foreach (var tourist in touristList)
                {
                    document.Add(new Paragraph($"    {i}. " + tourist.FirstName + " " + tourist.LastName));
                    i++;
                }

                // Zatvori dokument
                document.Close();
                writer.Close();
            }
        }
        public List<PeopleInfo> GetListOfTourists(int instanceId)
        {
            TourInstance instance = _tourInstanceService.GetById(instanceId);
            Tour tour = _tourService.GetById(instance.IdTour);
            List<KeyPoint> keyPoints = _keyPointService.GetKeypointsByIds(tour.KeyPointIds);
            List<PeopleInfo> people = new List<PeopleInfo>();
            foreach(KeyPoint kp in keyPoints)
            {
                foreach(int id in kp.PresentPeopleIds)
                {
                    PeopleInfo p = _peopleInfoService.GetById(id);
                    if (!people.Contains(p))
                    {
                        people.Add(p);
                    }
                }
            }
            return people;
        }
    }
}
