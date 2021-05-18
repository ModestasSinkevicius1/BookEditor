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

            string bookAdress;
            Console.WriteLine("Visma book library \nWhat would you like to do?");
            bookAdress = Console.ReadLine();

            string[] splitBookAdress = bookAdress.Split(" ");

            if (splitBookAdress[0] == "add")
            {
                if (splitBookAdress.Length - 1 == 6)
                {
                    string bookName = splitBookAdress[1];
                    string bookAuthor = splitBookAdress[2];
                    string bookCategory = splitBookAdress[3];
                    string bookLanguage = splitBookAdress[4];
                    DateTime bookDate = Convert.ToDateTime(splitBookAdress[5]);
                    int bookISBN = Convert.ToInt32(splitBookAdress[6]);

                    b = new Book(bookName, bookAuthor, bookCategory, bookLanguage, bookDate, bookISBN);

                    jsonRW.WriteJSON(b, "book.json");
                }
                else
                    Console.WriteLine("Book adress was not complete, aborting operation");
            }
            else if (splitBookAdress[0] == "read")
            {                
                foreach(Book bookRow in jsonRW.ReadJSON("book.json"))
                {
                    Console.WriteLine(bookRow.name);
                }
            }
            else if (splitBookAdress[0] == "take")
            {
                if (splitBookAdress.Length - 1 == 3)
                {                  
                    string bookName = splitBookAdress[1];

                    bool isBookFound = false;

                    foreach(Book bRow in jsonRW.ReadJSON("book.json"))
                    {
                        if(bRow.name == bookName)
                        {
                            isBookFound = true;

                            string user = splitBookAdress[2];
                            int period = Convert.ToInt32(splitBookAdress[3]);

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
            else if(splitBookAdress[0] == "return")
            {
                if (splitBookAdress.Length - 1 == 2)
                {
                    string bookName = splitBookAdress[1];
                    string user = splitBookAdress[2];

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
            else
                Console.WriteLine("Unknown command, check readme file");

            Console.ReadLine();
        }
    }
}
