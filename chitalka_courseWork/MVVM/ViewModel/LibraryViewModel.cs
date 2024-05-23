using chitalka_courseWork.MVVM.Model;
using chitalka_courseWork.MVVM.View;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;

namespace chitalka_courseWork.MVVM.ViewModel
{
    public partial class LibraryViewModel : ObservableObject
    {
        [ObservableProperty]
        private RelayCommand _deleteBookCommand;
        [ObservableProperty]
        private RelayCommand _startReadingSessionCommand;
        [ObservableProperty]
        private RelayCommand _stopReadingSessionCommand;

        [ObservableProperty]
        private Book? _selectedBook;

        [ObservableProperty]
        private ObservableCollection<Book> _books;

        [ObservableProperty]
        private ObservableCollection<Book> _inProgressBooks;

        [ObservableProperty]
        private ObservableCollection<Book> _readBooks;

        [ObservableProperty]
        private Dictionary<string, Statistics> _bookStats;

        [ObservableProperty]
        private int _selectedBookProgress;

        public string SelectedBookKey 
        {
            get => $"{SelectedBook.Title} {SelectedBook.Title}";
            set => SelectedBookKey = value; 
        }
        private ReadingSession readingSession;
        private DateTime timeStart;
        private DateTime timeEnd;

        private readonly string booksDataFilePath = "C:\\Users\\Liliia\\source\\repos\\ogurtsy_new\\chitalka_courseWork\\DB\\data.json";
        private readonly string readingSessionsDataFilePath = "C:\\Users\\Liliia\\source\\repos\\ogurtsy_new\\chitalka_courseWork\\DB\\readingSessions.json";
        public LibraryViewModel()
        {
            string jsonBooksDataAll = File.ReadAllText(booksDataFilePath);
            Books = JsonConvert.DeserializeObject<ObservableCollection<Book>>(jsonBooksDataAll);
            FilterBooks();
            Books.CollectionChanged += Books_CollectionChanged;

            BookStats = new Dictionary<string, Statistics>();
            LoadReadingStats();

            DeleteBookCommand = new RelayCommand(DeleteBook);
            StartReadingSessionCommand = new RelayCommand(StartReadingSession);
            StopReadingSessionCommand = new RelayCommand(StopReadingSession);
        }

        private void Books_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            UpdateBooksCollection();
            FilterBooks();
        }
        
        public void UpdateBooksCollection()
        {
            string json = JsonConvert.SerializeObject(Books, Formatting.Indented);
            File.WriteAllText(booksDataFilePath, json);
            Books = JsonConvert.DeserializeObject<ObservableCollection<Book>>(json);
            FilterBooks();
        }

        private void FilterBooks()
        {
            InProgressBooks = new ObservableCollection<Book>(Books.Where(book => book.ReadingStatus == ReadingStatus.InProgress));
            ReadBooks = new ObservableCollection<Book>(Books.Where(book => book.ReadingStatus == ReadingStatus.Read));
        }

        public void DeleteBook()
        {
            if (SelectedBook != null)
            {
                Books.Remove(SelectedBook);
                SelectedBook = null;
                UpdateBooksCollection();
            }
            else
            {
                MessageBox.Show("Ви не обрали книгу!");
            }
        }

        public void SaveReadingStats()
        {
            string json = JsonConvert.SerializeObject(BookStats, Formatting.Indented);
            File.WriteAllText(readingSessionsDataFilePath, json);
        }

        public void LoadReadingStats()
        {
            if (File.Exists(readingSessionsDataFilePath))
            {
                string json = File.ReadAllText(readingSessionsDataFilePath);
                BookStats = JsonConvert.DeserializeObject<Dictionary<string, Statistics>>(json);
            }
            else
            {
                BookStats = new Dictionary<string, Statistics>();
            }
        }

        public void StartReadingSession()
        {
            timeStart = DateTime.Now;
        }

        public void StopReadingSession()
        {
            timeEnd = DateTime.Now;

            if (SelectedBook != null)
            {
                readingSession = new ReadingSession()
                {
                    ReadingTime = timeEnd - timeStart,
                    ReadingDate = DateOnly.FromDateTime(timeEnd),
                    PagesRead = GetPagesReadFromUser()
                };

                if (!BookStats.ContainsKey($"{SelectedBook.Title} {SelectedBook.Author}"))
                {
                    BookStats[$"{SelectedBook.Title} {SelectedBook.Author}"] = new Statistics();
                }

                BookStats[$"{SelectedBook.Title} {SelectedBook.Author}"].ReadingSessions.Add(readingSession);
                SaveReadingStats();
                LoadReadingStats();
            }

            timeStart = DateTime.MinValue;
            timeEnd = DateTime.MinValue;
        }

        private int GetPagesReadFromUser()
        {
            int pagesRead = 0;
            var inputDialog = new PagesReadDialog(); 
            if (inputDialog.ShowDialog() == true)
            {
                pagesRead = inputDialog.PagesRead;
            }
            return pagesRead;
        }
    }
}
