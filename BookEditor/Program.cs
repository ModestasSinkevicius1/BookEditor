using System;

namespace BookEditor
{
    class Program
    {
        static void Main(string[] args)
        {
            Book b;
            User u;            

            JSONReaderWriter jsonRW = new JSONReaderWriter();
            Filter f = new Filter();

            string bookAdress;
            Console.WriteLine("Visma book library \nWhat would you like to do?");
            bookAdress = Console.ReadLine();

            string[] commandAddress = bookAdress.Split(" ");

            if (commandAddress[0] == "add")
            {
                if (commandAddress.Length - 1 == 6)
                {
                    string bookName = commandAddress[1];
                    string bookAuthor = commandAddress[2];
                    string bookCategory = commandAddress[3];
                    string bookLanguage = commandAddress[4];
                    DateTime bookDate = Convert.ToDateTime(commandAddress[5]);
                    int bookISBN = Convert.ToInt32(commandAddress[6]);

                    b = new Book(bookName, bookAuthor, bookCategory, bookLanguage, bookDate, bookISBN);

                    jsonRW.WriteJSON(b, "book.json");
                }
                else
                    Console.WriteLine("Book adress was not complete, aborting operation");
            }
            else if (commandAddress[0] == "readall")
            {           
                if (commandAddress.Length - 1 == 1)
                {
                    string filterKey = commandAddress[1];
                    if (filterKey != "taken")
                    {
                        foreach (Book bRow in f.FilterBookList(jsonRW.ReadJSON("book.json"), filterKey))
                        {
                            Console.WriteLine(bRow.name + " " + bRow.author + " " + bRow.category
                                + " " + bRow.language + " " + bRow.publicationDate.ToShortDateString() + " " + bRow.isbn);
                        }
                    }
                    else
                    {
                        foreach (Book bRow in f.ShowTakenBookList(jsonRW.ReadUserJSON("user.json")))
                        {
                            Console.WriteLine(bRow.name + " " + bRow.author + " " + bRow.category
                                + " " + bRow.language + " " + bRow.publicationDate.ToShortDateString() + " " + bRow.isbn);
                        }
                    }
                }
            }
            else if (commandAddress[0] == "take")
            {
                if (commandAddress.Length - 1 == 3)
                {                  
                    string bookName = commandAddress[1];

                    bool isBookFound = false;

                    foreach(Book bRow in jsonRW.ReadJSON("book.json"))
                    {
                        if(bRow.name == bookName)
                        {
                            isBookFound = true;

                            string user = commandAddress[2];
                            int period = Convert.ToInt32(commandAddress[3]);

                            if (period < 3)
                            {
                                int count = 0;

                                foreach (User uRow in jsonRW.ReadUserJSON("user.json"))
                                {
                                    if (uRow.user == user)
                                    {
                                        count++;
                                    }
                                }
                                if (count <= 3)
                                {
                                    u = new User(bRow, user, period, DateTime.Now);

                                    jsonRW.WriteJSON(u, "user.json");

                                    Console.WriteLine("Thank you taking our book");
                                }
                                else
                                    Console.WriteLine("You took to many books");
                            }
                            else
                                Console.WriteLine("You can't take a book longer than 2 months");
                            break;
                        }
                    }

                    if(!isBookFound)
                        Console.WriteLine("Book not found");

                    
                }
            }
            else if(commandAddress[0] == "return")
            {
                if (commandAddress.Length - 1 == 2)
                {
                    string bookName = commandAddress[1];
                    string user = commandAddress[2];

                    bool isBookFound = false;

                    foreach (User uRow in jsonRW.ReadUserJSON("user.json"))
                    {                       
                        if (uRow.bookName.name == bookName && uRow.user == user)
                        {
                            TimeSpan time = DateTime.Now - uRow.dateTaken;
                            if (time.Days / 30.5 > uRow.period)
                                Console.WriteLine("You done goofed");
                            isBookFound = true;
                            jsonRW.UpdateJSON("user.json", uRow);
                            break;
                        }
                    }
                    if(!isBookFound)
                        Console.WriteLine("Book not found");
                }
            }
            else if(commandAddress[0] == "delete")
            {
                if(commandAddress.Length - 1 == 1)
                {
                    string bookName = commandAddress[1];
                    foreach(Book bRow in jsonRW.ReadJSON("book.json"))
                    {
                        if(bRow.name == bookName)
                        {
                            jsonRW.DeleteBookFromJSON("book.json", bRow);
                            break;
                        }
                    }

                }
            }
            else
                Console.WriteLine("Unknown command, check readme file");

            Console.ReadLine();
        }
    }
}
