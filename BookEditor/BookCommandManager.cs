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

        bool state = false;

        public void AddNewBook(string[] commandKeys)
        {
            try
            {
                //command parts are assigned to given variables
                string bookName = commandKeys[1];
                string bookAuthor = commandKeys[2];
                string bookCategory = commandKeys[3];
                string bookLanguage = commandKeys[4];
                DateTime bookDate = Convert.ToDateTime(commandKeys[5]);
                int bookISBN = Convert.ToInt32(commandKeys[6]);


                b = new Book(bookName, bookAuthor, bookCategory, bookLanguage, bookDate, bookISBN);

                //adding object to json file
                jsonRW.WriteObjectToJSON(b, "book.json");

                state = true;

                Console.WriteLine("New book has been added");
                Console.ReadLine();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message + ". Bad input format");
                Console.ReadLine();
            }
        }

        public void ReadBookList(string[] commandKeys)
        {            
            string filterKey = commandKeys[1];
            bool bookListNotFound = false;
            if (filterKey != "taken")
            {
                foreach (Book bRow in f.FilterBookList(jsonRW.ReadBookFromJSON("book.json"), filterKey))
                {
                    bookListNotFound = true;

                    Console.WriteLine(bRow.name + " " + bRow.author + " " + bRow.category
                        + " " + bRow.language + " " + bRow.publicationDate.ToShortDateString() + " " + bRow.isbn);
                }                
            }
            else
            {
                foreach (Book bRow in f.ShowTakenBookList(jsonRW.ReadUserFromJSON("user.json")))
                {
                    bookListNotFound = true;

                    Console.WriteLine(bRow.name + " " + bRow.author + " " + bRow.category
                        + " " + bRow.language + " " + bRow.publicationDate.ToShortDateString() + " " + bRow.isbn);
                }            
            }
            if(!bookListNotFound)
            {
                Console.WriteLine("Library or taken books are empty");
            }
            state = true;
            Console.ReadLine();
        }
        public void TakeBook(string[] commandKeys)
        {
            string bookName = commandKeys[1];

            //used to indicate if book exist in line 123
            bool isBookFound = false;

            //checking if book exist in json file
            foreach (Book bRow in jsonRW.ReadBookFromJSON("book.json"))
            {
                if (bRow.name == bookName)
                {
                    try
                    {
                        isBookFound = true;

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

                                Console.WriteLine("Thank you taking our book");                               
                            }
                            else
                                Console.WriteLine("You took to many books");
                        }
                        else
                            Console.WriteLine("You can't take a book longer than 2 months");
                        break;
                    }
                    catch (Exception exc)
                    {
                        Console.WriteLine(exc.Message + ". Bad input format");                      
                    }
                }
            }

            if (!isBookFound)
            {
                Console.WriteLine("Book not found");                
            }
            Console.ReadLine();
        }

        public void ReturnBook(string[] commandKeys)
        {
            string bookName = commandKeys[1];
            string user = commandKeys[2];

            bool isBookFound = false;

            foreach (User uRow in jsonRW.ReadUserFromJSON("user.json"))
            {
                if (uRow.bookName.name == bookName && uRow.user == user)
                {
                    //used to determine how long does user kept the book before was taken
                    TimeSpan time = DateTime.Now - uRow.dateTaken;

                    /*30.5 is average months per year, dividing days by average moth gives us 
                     * how many months was book borrowed*/
                    if (time.Days / 30.5 > uRow.period)
                        Console.WriteLine("You done goofed");

                    isBookFound = true;
                    jsonRW.DeleteUserFromJSON("user.json", uRow);

                    Console.WriteLine("Book has been returned");                   

                    break;
                }
            }
            if (!isBookFound)
            {
                Console.WriteLine("Book not found");                
            }
            Console.ReadLine();
        }

        public void DeleteBook(string[] commandKeys)
        {
            string keyWord = commandKeys[1];

            bool isBookFound = false;

            foreach (Book bRow in jsonRW.ReadBookFromJSON("book.json"))
            {
                isBookFound = true;

                if (bRow.name == keyWord || bRow.author == keyWord)
                {
                    jsonRW.DeleteBookFromJSON("book.json", bRow);

                    Console.WriteLine("Book has been deleted");                    

                    break;
                }
            }
            if(!isBookFound)
                Console.WriteLine("Book not found");

            Console.ReadLine();
        }

        public bool isFunctionExecutedSuccesfuly()
        {
            return state;
        }
    }
}
