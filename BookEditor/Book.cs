using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookEditor
{
    //class used to handle book object and his properties
    public class Book
    {
        //object book stores these properties
        public string name { get; set; }
        public string author { get; set; }
        public string category { get; set; }
        public string language { get; set; }
        public DateTime publicationDate { get; set; }
        public int isbn { get; set; }
        

        public Book(string name, string author, string category, string language, DateTime publicationDate, int isbn)
        {
            this.name = name;
            this.author = author;
            this.category = category;
            this.language = language;
            this.publicationDate = publicationDate;
            this.isbn = isbn;
        }
    }
}
