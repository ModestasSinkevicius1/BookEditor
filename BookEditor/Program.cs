using System;

namespace BookEditor
{
    //main class handling command executions
    class Program
    {
        static public BookCommandManager bcm = new BookCommandManager();

        static void Main(string[] args)
        {          
            //variable where command inputs given
            string commandAddress;        

            //used to never end application until command "quit" is executed
            while (true)
            {
                //erasing variable for avoiding errors
                commandAddress = "";

                Console.WriteLine("Visma book library \n" +
                    "What would you like to do?");
                commandAddress = Console.ReadLine();

                //seperating command values into parts
                string[] commandKeys = commandAddress.Split(" ");               

                //checking if first command word equals given command key
                //add command adds new book to json file
                if (commandKeys[0] == "add")
                {
                    //checking if commands parts matches required total command keys
                    if (commandKeys.Length - 1 == 6)
                    {
                        bcm.AddNewBook(commandKeys);
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
                        bcm.ReadBookList(commandKeys);
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
                        bcm.TakeBook(commandKeys);
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
                        bcm.ReadBookList(commandKeys);
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
                        bcm.DeleteBook(commandKeys);
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
