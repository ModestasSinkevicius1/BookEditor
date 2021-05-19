using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookEditor
{
    //class used to handle user object and his properties
    class User
    {
        //object user stores these properties
        public Book bookName { get; set; }
        public string user { get; set; }        
        public int period { get; set; }      
        public DateTime dateTaken { get; set; }

        public User(Book bookName, string user, int period, DateTime dateTaken)
        {
            this.bookName = bookName;           
            this.user = user;
            this.period = period;
            this.dateTaken = dateTaken;          
        }       
    }
}
