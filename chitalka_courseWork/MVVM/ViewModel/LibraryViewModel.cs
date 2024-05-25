using chitalka_courseWork.MVVM.Model;
using chitalka_courseWork.MVVM.View;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
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
        private ReadingSession? _selectedSession;

        [ObservableProperty]
        private ObservableCollection<Book> _books;

        [ObservableProperty]
        private ObservableCollection<Book> _inProgressBooks;

        [ObservableProperty]
        private ObservableCollection<Book> _readBooks;

        [ObservableProperty]
        private int _selectedBookProgress;

        [ObservableProperty]
        private ReadingSession _readingSession;

        private DateTime timeStart;
        private DateTime timeEnd;

        private readonly string booksDataFilePath = "C:\\Users\\Liliia\\source\\repos\\ogurtsy_new\\chitalka_courseWork\\DB\\data.json";

        public LibraryViewModel()
        {
            LoadBooksData();
            FilterBooks();
            Books.CollectionChanged += Books_CollectionChanged;

            DeleteBookCommand = new RelayCommand(DeleteBook);
            StartReadingSessionCommand = new RelayCommand(StartReadingSession);
            StopReadingSessionCommand = new RelayCommand(StopReadingSession);

            foreach (Book book in Books) 
            {
                book.Stats.UpdateProgress();
                //MessageBox.Show($"Progress is {book.Stats.Progress}");
            }
        }

        private void LoadBooksData()
        {
            if (File.Exists(booksDataFilePath))
            {
                string jsonBooksDataAll = File.ReadAllText(booksDataFilePath);
                Books = JsonConvert.DeserializeObject<ObservableCollection<Book>>(jsonBooksDataAll) ?? new ObservableCollection<Book>();
            }
            else
            {
                Books = new ObservableCollection<Book>();
            }
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

        public void StartReadingSession()
        {
            timeStart = DateTime.Now;
        }

        public void StopReadingSession()
        {
            timeEnd = DateTime.Now;

            if (SelectedBook != null)
            {
                ReadingSession = new ReadingSession()
                {
                    ReadingTime = timeEnd - timeStart,
                    ReadingDate = DateOnly.FromDateTime(timeEnd),
                    PagesRead = GetPagesReadFromUser()
                };
                SelectedBook.Stats.ReadingSessions.Add(ReadingSession);
                SelectedBook.Stats.PagesRead += ReadingSession.PagesRead;
                SelectedBook.Stats.UpdateProgress();
                MessageBox.Show($"{SelectedBook.Stats.ReadingSessions.Count} -> count");
                MessageBox.Show($"{SelectedBook.Stats.PagesRead} -> pages read");
                MessageBox.Show($"{SelectedBook.Stats.Progress} -> progress");
                
                
                UpdateBooksCollection();
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
