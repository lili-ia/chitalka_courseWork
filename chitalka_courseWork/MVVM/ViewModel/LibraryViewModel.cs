using chitalka_courseWork.MVVM.Model;
using chitalka_courseWork.MVVM.View;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

        [ObservableProperty]
        private ObservableCollection<StarRating> _starRatings;

        [ObservableProperty]
        private RelayCommand<int> _rateCommand;

        [ObservableProperty]
        private int _selectedRating;

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
            }

            StarRatings = new ObservableCollection<StarRating>
            {
                new StarRating { Rating = 1 },
                new StarRating { Rating = 2 },
                new StarRating { Rating = 3 },
                new StarRating { Rating = 4 },
                new StarRating { Rating = 5 }
            };
            RateCommand = new RelayCommand<int>(Rate);
        }

        private void LoadBooksData()
        {
            if (File.Exists(booksDataFilePath))
            {
                string jsonBooksDataAll = File.ReadAllText(booksDataFilePath);
                Books = JsonConvert.DeserializeObject<ObservableCollection<Book>>(jsonBooksDataAll) ?? new ObservableCollection<Book>();

                foreach (var book in Books)
                {
                    book.Stats.SetTotalPageCount(book.PagesCount);
                    book.Stats.ReadingSessions.CollectionChanged += (s, e) => book.Stats.UpdateProgress();
                    book.Stats.PropertyChanged += (s, e) =>
                    {
                        if (e.PropertyName == nameof(Statistics.Progress))
                        {
                            book.UpdateReadingStatus();
                            FilterBooks();
                        }
                    };
                }
            }
            else
            {
                Books = new ObservableCollection<Book>();
            }
        }

        private void Books_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            UpdateBooksCollection();
            LoadBooksData();
            FilterBooks();
        }

        public void UpdateBooksCollection()
        {
            string json = JsonConvert.SerializeObject(Books, Formatting.Indented);
            File.WriteAllText(booksDataFilePath, json);
        }

        public void FilterBooks()
        {
            InProgressBooks = new ObservableCollection<Book>(Books.Where(book => book.ReadingStatus == ReadingStatus.InProgress));
            ReadBooks = new ObservableCollection<Book>(Books.Where(book => book.ReadingStatus == ReadingStatus.Read));
        }

        public void Rate(int rating)
        {
            SelectedRating = rating;
            foreach (var star in StarRatings)
            {
                star.IsFilled = star.Rating <= rating;
            }
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
            if (SelectedBook != null)
                timeStart = DateTime.Now;
            else
                MessageBox.Show("Ви не обрали книгу!");
        }

        public void StopReadingSession()
        {
            timeEnd = DateTime.Now;

            if (SelectedBook != null)
            {
                var today = DateOnly.FromDateTime(timeEnd);
                int pagesRead = GetPagesReadFromUser();

                if (pagesRead > 0)
                {
                    ReadingSession = new ReadingSession()
                    {
                        ReadingTime = timeEnd - timeStart,
                        ReadingDate = today,
                        PagesRead = pagesRead,
                    };

                    var currentSelectedBook = SelectedBook;

                    currentSelectedBook.Stats.ReadingSessions.Add(ReadingSession);
                    currentSelectedBook.Stats.UpdateProgress();


                    UpdateBooksCollection();

                    timeStart = DateTime.MinValue;
                    timeEnd = DateTime.MinValue;

                    SelectedBook = null;
                    SelectedBook = currentSelectedBook;
                }
            }
            else
            {
                MessageBox.Show("Ви не обрали книгу!");
            }
        }

        private int GetPagesReadFromUser()
        {
            int pagesRead = 0;
            var inputDialog = new PagesReadDialog();

            if (inputDialog.ShowDialog() == true)
                pagesRead = inputDialog.PagesRead;

            return pagesRead;
        }
    }
}
