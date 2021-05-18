using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookEditor
{
    class BookTaken
    {
        public string period { get; set; }
        public Book book { get; set; }
        public DateTime dateTaken { get; set; }

        public BookTaken(string period, Book book, DateTime dateTaken)
        {
            this.period = period;
            this.book = book;
            this.dateTaken = dateTaken;
        }
    }
}
