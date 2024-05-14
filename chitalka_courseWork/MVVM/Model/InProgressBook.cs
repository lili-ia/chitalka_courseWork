using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chitalka_courseWork.MVVM.Model
{
    public class InProgressBook : Book
    {
        public InProgressBook(string title, string author, string? description, int? pagesCount) : base(title, author, description, pagesCount) 
        {
            ReadingSessions = [];
        }
        
        public Dictionary<DateOnly, int> ReadingSessions { get; set; }
    }
}
