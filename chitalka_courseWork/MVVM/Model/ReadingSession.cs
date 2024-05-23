using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chitalka_courseWork.MVVM.Model
{
    public class ReadingSession
    {
        public int PagesRead { get; set; }
        public TimeSpan ReadingTime {  get; set; }
        public DateOnly ReadingDate { get; set; }

        public ReadingSession() { }
        
    }
}
