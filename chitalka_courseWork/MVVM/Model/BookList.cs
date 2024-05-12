using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chitalka_courseWork.MVVM.Model
{
    public class BookList :IEnumerable<Book>
    {
        private List<Book> books;
        public BookList()
        {
            books = new List<Book>();
        }

        public void AddBook(Book book) => books.Add(book);
        public void RemoveBook(Book book) => books.Remove(book);
        
        public IEnumerator<Book> GetEnumerator()
        {
            foreach(var book in books)
            {
                yield return book;
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
