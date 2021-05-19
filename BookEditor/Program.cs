using System;

namespace BookEditor
{
    //main class handling command executions
    class Program
    {
        static void Main(string[] args)
        {
            //initializing objects for later use
            Book b;
            User u;            

            JSONReaderWriter jsonRW = new JSONReaderWriter();
            Filter f = new Filter();

            //variable where command inputs given
            string commandAdress;        

            //used to never end application until command "quit" is executed
            while (true)
            {
                //erasing variable for avoiding errors
                commandAdress = "";

                Console.WriteLine("Visma book library \n" +
                    "What would you like to do?");
                commandAdress = Console.ReadLine();

                //seperating command values into parts
                string[] commandKeys = commandAdress.Split(" ");               

                //checking if first command word equals given command key
                //add command adds new book to json file
                if (commandKeys[0] == "add")
                {
                    //checking if commands parts matches required total command keys
                    if (commandKeys.Length - 1 == 6)
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
                //readall command reads all books by given order
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

                //take command creates user and given book period
                else if (commandKeys[0] == "take")
                {
                    if (commandKeys.Length - 1 == 3)
                    {
                        string bookName = commandKeys[1];

                        //used to indicate if book exist in line 156
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
                //return command returns book from given user
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
                                //used to determine how long does user kept the book before was taken
                                TimeSpan time = DateTime.Now - uRow.dateTaken;

                                /*30.5 is average months per year, dividing days by average moth gives us 
                                 * how many months was book borrowed*/
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
                //delete command deletes book from "book.json"
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
                //quit commands ends application
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
        }
    }
}
