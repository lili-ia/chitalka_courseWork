using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections;
using System.Collections.ObjectModel;

namespace chitalka_courseWork.MVVM.Model
{
    public partial class BookList : ObservableObject, IEnumerable<Book>
    {
        [ObservableProperty]
        private ObservableCollection<Book> _books;

        public BookList() => Books = [];

        public IEnumerator<Book> GetEnumerator()
        {
            foreach (Book book in Books)
                yield return book;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
