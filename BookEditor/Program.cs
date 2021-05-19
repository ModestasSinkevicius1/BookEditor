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

            string commandAdress;        

            while (true)
            {
                commandAdress = "";

                Console.WriteLine("Visma book library \nWhat would you like to do?");
                commandAdress = Console.ReadLine();

                string[] commandKeys = commandAdress.Split(" ");               

                if (commandKeys[0] == "add")
                {
                    if (commandKeys.Length - 1 == 6)
                    {
                        try
                        {
                            string bookName = commandKeys[1];
                            string bookAuthor = commandKeys[2];
                            string bookCategory = commandKeys[3];
                            string bookLanguage = commandKeys[4];
                            DateTime bookDate = Convert.ToDateTime(commandKeys[5]);
                            int bookISBN = Convert.ToInt32(commandKeys[6]);


                            b = new Book(bookName, bookAuthor, bookCategory, bookLanguage, bookDate, bookISBN);

                            jsonRW.WriteObjectToJSON(b, "book.json");

                            Console.WriteLine("New book has been added");
                            Console.ReadLine();
                        }
                        catch(Exception exc)
                        {
                            Console.WriteLine(exc.Message + ". Bad input format");
                            Console.ReadLine();
                        }
                    }
                    else
                    {
                        Console.WriteLine("Command adress was not complete or inocorrect check readme file, aborting operation");
                        Console.ReadLine();
                    }
                }
                else if (commandKeys[0] == "readall")
                {
                    if (commandKeys.Length - 1 == 1)
                    {
                        string filterKey = commandKeys[1];
                        if (filterKey != "taken")
                        {
                            foreach (Book bRow in f.FilterBookList(jsonRW.ReadBookFromJSON("book.json"), filterKey))
                            {
                                Console.WriteLine(bRow.name + " " + bRow.author + " " + bRow.category
                                    + " " + bRow.language + " " + bRow.publicationDate.ToShortDateString() + " " + bRow.isbn);
                            }
                            Console.ReadLine();
                        }
                        else
                        {
                            foreach (Book bRow in f.ShowTakenBookList(jsonRW.ReadUserFromJSON("user.json")))
                            {
                                Console.WriteLine(bRow.name + " " + bRow.author + " " + bRow.category
                                    + " " + bRow.language + " " + bRow.publicationDate.ToShortDateString() + " " + bRow.isbn);
                            }
                            Console.ReadLine();
                        }
                    }
                    else
                    {
                        Console.WriteLine("Command adress was not complete or inocorrect check readme file, aborting operation");
                        Console.ReadLine();
                    }
                }
                else if (commandKeys[0] == "take")
                {
                    if (commandKeys.Length - 1 == 3)
                    {
                        string bookName = commandKeys[1];

                        bool isBookFound = false;

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
                                            Console.ReadLine();
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
                                    Console.ReadLine();
                                }
                            }
                        }

                        if (!isBookFound)
                        {
                            Console.WriteLine("Book not found");
                            Console.ReadLine();
                        }
                    }
                    else
                    {
                        Console.WriteLine("Command adress was not complete or inocorrect check readme file, aborting operation");
                        Console.ReadLine();
                    }
                }
                else if (commandKeys[0] == "return")
                {
                    if (commandKeys.Length - 1 == 2)
                    {
                        string bookName = commandKeys[1];
                        string user = commandKeys[2];

                        bool isBookFound = false;

                        foreach (User uRow in jsonRW.ReadUserFromJSON("user.json"))
                        {
                            if (uRow.bookName.name == bookName && uRow.user == user)
                            {
                                TimeSpan time = DateTime.Now - uRow.dateTaken;
                                if (time.Days / 30.5 > uRow.period)
                                    Console.WriteLine("You done goofed");
                                isBookFound = true;
                                jsonRW.DeleteUserFromJSON("user.json", uRow);
                                Console.WriteLine("Book has been returned");
                                Console.ReadLine();
                                break;
                            }
                        }
                        if (!isBookFound)
                        {
                            Console.WriteLine("Book not found");
                            Console.ReadLine();
                        }
                    }
                    else
                    {
                        Console.WriteLine("Command adress was not complete or inocorrect check readme file, aborting operation");
                        Console.ReadLine();
                    }
                }
                else if (commandKeys[0] == "delete")
                {
                    if (commandKeys.Length - 1 == 1)
                    {
                        string keyWord = commandKeys[1];
                        foreach (Book bRow in jsonRW.ReadBookFromJSON("book.json"))
                        {
                            if (bRow.name == keyWord || bRow.author == keyWord)
                            {
                                jsonRW.DeleteBookFromJSON("book.json", bRow);

                                Console.WriteLine("Book has been deleted");
                                Console.ReadLine();

                                break;
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Command adress was not complete or inocorrect check readme file, aborting operation");
                        Console.ReadLine();
                    }
                }
                else if (commandKeys[0] == "quit")
                {
                    Console.WriteLine("Thank you for using our application");
                    Console.ReadLine();
                    Environment.Exit(0);
                }
                else
                {
                    Console.WriteLine("Unknown command, check readme file");
                    Console.ReadLine();
                }
                Console.Clear();
            }
            //Console.ReadLine();
        }
    }
}
