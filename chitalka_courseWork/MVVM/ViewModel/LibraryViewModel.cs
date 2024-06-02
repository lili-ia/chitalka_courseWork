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
        private RelayCommand _sortCommand;
        [ObservableProperty]
        private RelayCommand _clearFiltersSortingCommand;
        [ObservableProperty]
        private RelayCommand _filterByRatingCommand;
        [ObservableProperty]
        private RelayCommand _filterByReadingStatusCommand;
        [ObservableProperty]
        private RelayCommand _addNoteCommand;
        [ObservableProperty]
        private RelayCommand _editModeCommand;

        [ObservableProperty]
        private Book? _selectedBook;
        [ObservableProperty]
        private ReadingSession? _selectedSession;

        [ObservableProperty]
        private ObservableCollection<Book> _books;

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

        [ObservableProperty]
        private ObservableCollection<Book> _filteredCollection;

        [ObservableProperty]
        private string _selectedSortCriterion;
        [ObservableProperty]
        private string _selectedReadingStatusFilterCriterion;
        [ObservableProperty]
        private string _selectedRatingFilterCriterion;

        [ObservableProperty]
        private string _query;
        [ObservableProperty]
        private string _noteContent;

        private readonly string booksDataFilePath;

        public LibraryViewModel()
        {
            string projectDirectory = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName;
            booksDataFilePath = Path.Combine(projectDirectory, "DB", "data.json");
            
            LoadBooksData();
            Books.CollectionChanged += Books_CollectionChanged;
            FilteredCollection = new ObservableCollection<Book>(Books);

            DeleteBookCommand = new RelayCommand(DeleteBook);
            StartReadingSessionCommand = new RelayCommand(StartReadingSession);
            StopReadingSessionCommand = new RelayCommand(StopReadingSession);
            SortCommand = new RelayCommand(SortBooks);
            ClearFiltersSortingCommand = new RelayCommand(ClearFilters);
            FilterByReadingStatusCommand = new RelayCommand(FilterBooksByReadingStatus);
            FilterByRatingCommand = new RelayCommand(FilterBooksByRating);
            AddNoteCommand = new RelayCommand(AddNote);
            EditModeCommand = new RelayCommand(EditBook);
            RateCommand = new RelayCommand<int>(Rate);
            

            StarRatings =
            [
                new() { Rating = 1 },
                new() { Rating = 2 },
                new() { Rating = 3 },
                new() { Rating = 4 },
                new() { Rating = 5 }
            ]; 
            
        }

        private void AddNote()
        {
            if (NoteContent != null && SelectedBook != null)
            {
                SelectedBook.Stats.Notes.Add(NoteContent);
                UpdateBooksCollection();
            }
            else
                MessageBox.Show("Note is empty!");
            
        }

        public void ClearFilters()
        {
            UpdateBooksCollection();
            FilteredCollection = new ObservableCollection<Book>(Books);
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
                        }
                    };
                    book.Stats.PropertyChanged += (s, e) =>
                    {
                        if (e.PropertyName == nameof(Statistics.Rating))
                        {
                            UpdateBooksCollection();
                            FilteredCollection = new ObservableCollection<Book>(Books);
                        }
                    };
                }
            }
            else
            {
                Books = [];
            }
        }

        partial void OnQueryChanged(string value)
        {
            SearchByQueryInBooklist(); 
        }
        private void Books_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            UpdateBooksCollection();
            FilteredCollection = new ObservableCollection<Book>(Books);
        }

        partial void OnBooksChanged(ObservableCollection<Book> value)
        {
            UpdateBooksCollection();
            FilteredCollection = new ObservableCollection<Book>(Books);
        }

        public void UpdateBooksCollection()
        {
            string json = JsonConvert.SerializeObject(Books, Formatting.Indented);
            File.WriteAllText(booksDataFilePath, json);
        }
        

        public void FilterBooksByReadingStatus()
        {
            if (SelectedReadingStatusFilterCriterion != null)
            {
                var filteredBooks = Books.AsEnumerable();

                if (SelectedReadingStatusFilterCriterion.Contains("NotStarted"))
                    filteredBooks = filteredBooks.Where(book => book.ReadingStatus == ReadingStatus.NotStarted);

                else if (SelectedReadingStatusFilterCriterion.Contains("InProgress"))
                    filteredBooks = filteredBooks.Where(book => book.ReadingStatus == ReadingStatus.InProgress);

                else
                    filteredBooks = filteredBooks.Where(book => book.ReadingStatus == ReadingStatus.Read);

                FilteredCollection = new ObservableCollection<Book>(filteredBooks);
            }

        }

        public void SearchByQueryInBooklist()
        {
            var filteredBooks = Books.AsEnumerable();
            filteredBooks = filteredBooks.Where(book => (book.Title.Contains(Query, StringComparison.CurrentCultureIgnoreCase) || book.Author.Contains(Query, StringComparison.CurrentCultureIgnoreCase)));
            FilteredCollection = new ObservableCollection<Book>(filteredBooks);
        }

        public void FilterBooksByRating()
        {
            if (SelectedRatingFilterCriterion != null)
            {
                var filteredBooks = Books.AsEnumerable();

                if (SelectedRatingFilterCriterion.Contains('1'))
                    filteredBooks = filteredBooks.Where(book => book.Stats.Rating == 1);

                else if (SelectedRatingFilterCriterion.Contains('2'))
                    filteredBooks = filteredBooks.Where(book => book.Stats.Rating == 2);

                else if (SelectedRatingFilterCriterion.Contains('3'))
                    filteredBooks = filteredBooks.Where(book => book.Stats.Rating == 3);

                else if (SelectedRatingFilterCriterion.Contains('4'))
                    filteredBooks = filteredBooks.Where(book => book.Stats.Rating == 4);

                else if (SelectedRatingFilterCriterion.Contains('5'))
                    filteredBooks = filteredBooks.Where(book => book.Stats.Rating == 5);
                
                else 
                    filteredBooks = filteredBooks.Where(book => book.Stats.Rating == 0);

                FilteredCollection = new ObservableCollection<Book>(filteredBooks);
            }

        }

        partial void OnSelectedSortCriterionChanged(string value)
        {
            SortBooks();
        }

        partial void OnSelectedRatingFilterCriterionChanged(string value)
        {
            FilterBooksByRating();
        }

        partial void OnSelectedReadingStatusFilterCriterionChanged(string value)
        {
            FilterBooksByReadingStatus();
        }

        public void SortBooks()
        {
            if (SelectedSortCriterion != null)
            {
                var sortedBooks = FilteredCollection.AsEnumerable();

                if (SelectedSortCriterion.Contains("Title"))
                    sortedBooks = sortedBooks.OrderBy(book => book.Title);

                else if (SelectedSortCriterion.Contains("Author"))
                    sortedBooks = sortedBooks.OrderBy(book => book.Author);

                else if (SelectedSortCriterion.Contains("Pages"))
                    sortedBooks = sortedBooks.OrderBy(book => book.PagesCount);

                else
                    sortedBooks = sortedBooks.OrderBy(book => book.Stats.Rating);

                FilteredCollection = new ObservableCollection<Book>(sortedBooks);
               
            }
            
        }

        public void Rate(int rating)
        {
            if (SelectedBook != null)
            {
                SelectedRating = rating;
                foreach (var star in StarRatings)
                    star.IsFilled = star.Rating <= rating;

                if (SelectedBook != null)
                    SelectedBook.Stats.Rating = rating;

                OnPropertyChanged(nameof(SelectedBook.Stats.Rating));
            }

        }

        private void EditBook() 
        {
            var editingDialog = new EditingBookDialog(SelectedBook);

            if (editingDialog.ShowDialog() == true)
            {
                if (!string.IsNullOrWhiteSpace(editingDialog.BookTitle))
                    SelectedBook!.Title = editingDialog.BookTitle;

                if (!string.IsNullOrWhiteSpace(editingDialog.Author))
                    SelectedBook!.Author = editingDialog.Author;

                if (!string.IsNullOrWhiteSpace(editingDialog.Description))
                    SelectedBook!.Description = editingDialog.Description;

                if (editingDialog.PagesCount != 0)
                {
                    SelectedBook!.PagesCount = editingDialog.PagesCount;
                    SelectedBook.Stats.SetTotalPageCount(editingDialog.PagesCount);
                }

                if (!string.IsNullOrWhiteSpace(editingDialog.CoverImagePath))
                    SelectedBook!.CoverImageUrl = editingDialog.CoverImagePath;


            }
                
            OnPropertyChanged(nameof(SelectedBook.Stats));
            UpdateBooksCollection();
        }


        partial void OnSelectedBookChanged(Book? value)
        {
            UpdateBooksCollection();
            UpdateStarRatings();
        }

        public void DeleteBook()
        {
            if (SelectedBook != null)
            {
                Books.Remove(SelectedBook);
                FilteredCollection.Remove(SelectedBook);
                SelectedBook = null;
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

        private static int GetPagesReadFromUser()
        {
            int pagesRead = 0;
            var inputDialog = new PagesReadDialog();

            if (inputDialog.ShowDialog() == true)
                pagesRead = inputDialog.PagesRead;

            return pagesRead;
        }


        private void UpdateStarRatings()
        {
            if (SelectedBook != null)
            {
                foreach (var star in StarRatings)
                {
                    star.IsFilled = star.Rating <= SelectedBook.Stats.Rating;
                }
            }
        }
    }
}
