using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookEditor
{
    public class BookCommandManager
    {
        //initializing objects for later use
        Book b;
        User u;

        JSONReaderWriter jsonRW = new JSONReaderWriter();
        Filter f = new Filter();        

        public string AddNewBook(string[] commandKeys)
        {
            try
            {
                //command parts are assigned to given variables
                string bookName = commandKeys[1];
                string bookAuthor = commandKeys[2];
                string bookCategory = commandKeys[3];
                string bookLanguage = commandKeys[4];
                DateTime bookDate = Convert.ToDateTime(commandKeys[5]);
                long bookISBN = Convert.ToInt64(commandKeys[6]);


                b = new Book(bookName, bookAuthor, bookCategory, bookLanguage, bookDate, bookISBN);

                //adding object to json file
                jsonRW.WriteObjectToJSON(b, "book.json");               

                return "New book has been added";                
            }
            catch (Exception exc)
            {
                return exc.Message + ". Bad input format";               
            }
        }

        public List<Book> ReadBookList(string[] commandKeys)
        {
            string filterKey = commandKeys[1];
            List<Book> libraryList;

            if (filterKey != "taken")
            {
                libraryList = f.FilterBookList(jsonRW.ReadBookFromJSON("book.json"), filterKey);                
            }
            else
            {
                libraryList = f.ShowTakenBookList(jsonRW.ReadUserFromJSON("user.json"));              
            }
            
            if (libraryList.Count > 0)
                return libraryList;
            else
                return null;          
        }       
        public string TakeBook(string[] commandKeys)
        {
            string bookName = commandKeys[1];            

            //checking if book exist in json file
            foreach (Book bRow in jsonRW.ReadBookFromJSON("book.json"))
            {
                if (bRow.name == bookName)
                {
                    try
                    {                        
                        string user = commandKeys[2];
                        int period = Convert.ToInt32(commandKeys[3]);

                        if (period < 3)
                        {
                            int count = 0;

                            //checking how many books does same user has taken
                            foreach (User uRow in jsonRW.ReadUserFromJSON("user.json"))
                            {
                                if (uRow.user == user)
                                {
                                    count++;
                                }
                            }
                            if (count <= 3)
                            {
                                u = new User(bRow, user, period, DateTime.Now);

                                jsonRW.WriteObjectToJSON(u, "user.json");

                                return "Thank you taking our book";                               
                            }
                            else
                                return "You took to many books";
                        }
                        else
                            return "You can't take a book longer than 2 months";                      
                    }
                    catch (Exception exc)
                    {
                        return exc.Message + ". Bad input format";                      
                    }
                }
            }
            
            return "Book not found";                            
        }

        public string ReturnBook(string[] commandKeys)
        {
            string bookName = commandKeys[1];
            string user = commandKeys[2];           

            foreach (User uRow in jsonRW.ReadUserFromJSON("user.json"))
            {
                if (uRow.bookName.name == bookName && uRow.user == user)
                {
                    //used to determine how long does user kept the book before was taken
                    TimeSpan time = DateTime.Now - uRow.dateTaken;                    

                    /*30.5 is average months per year, dividing days by average moth gives us 
                     * how many months was book borrowed*/
                    if (Math.Abs(time.Days) / 30.5 > uRow.period)
                    {
                        jsonRW.DeleteUserFromJSON("user.json", uRow);
                        return "You done goofed";
                    }
                    
                    jsonRW.DeleteUserFromJSON("user.json", uRow);

                    return "Book has been returned";                                     
                }
            }    
            
            return "Book not found";                           
        }

        public string DeleteBook(string[] commandKeys)
        {
            string keyWord = commandKeys[1];
           
            foreach (Book bRow in jsonRW.ReadBookFromJSON("book.json"))
            {               
                if (bRow.name == keyWord || bRow.author == keyWord)
                {
                    jsonRW.DeleteBookFromJSON("book.json", bRow);

                    return "Book has been deleted";                                        
                }
            }

            return "Book not found";           
        }       
    }
}
