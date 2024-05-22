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
        public DateTime Start {  get; set; }
        public DateTime End { get; set; }

        public ReadingSession() { }
        
    }
}
