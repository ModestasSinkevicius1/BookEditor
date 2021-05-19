using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;

namespace BookEditor
{
    //class used to handle JSON files and update list
    class JSONReaderWriter
    {
        List<Book> bList = new List<Book>();
        List<User> uList = new List<User>();

        //constructor used to create necessary files if not exist
        public JSONReaderWriter()
        {
            if (!File.Exists("user.json"))
            {
                File.WriteAllText("user.json", "[]");
            }
            if (!File.Exists("book.json"))
            {
                File.WriteAllText("book.json", "[]");
            }
        }

        //method meant to read "book.json" and add to list
        public List<Book> ReadBookFromJSON(string path)
        {
            try
            {
                string jsonText;
                if (File.Exists(path))
                {
                    jsonText = File.ReadAllText(path);              
                }
                else
                {
                    File.WriteAllText(path, "[]");
                    jsonText = File.ReadAllText(path);
                }

                bList = JsonSerializer.Deserialize<List<Book>>(jsonText);
                return bList;
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
                Console.ReadLine();

                return null;
            }
        }

        //method meant to read "user.json" and add to list
        public List<User> ReadUserFromJSON(string path)
        {
            try
            {
                string jsonText;
                if (File.Exists(path))
                {
                    jsonText = File.ReadAllText(path);
                    
                }
                else
                {
                    File.WriteAllText(path, "[]");
                    jsonText = File.ReadAllText(path);
                }

                uList = JsonSerializer.Deserialize<List<User>>(jsonText);
                return uList;
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
                Console.ReadLine();

                return null;
            }
        }

        //method used to serialize and write objects to json file
        public void WriteObjectToJSON(object obj, string path)
        {
            try
            {
                if (obj.GetType() == typeof(Book))
                {
                    Book b = (Book)obj;

                    bList = ReadBookFromJSON(path);

                    bList.Add(b);

                    string jsonFormat = JsonSerializer.Serialize(bList);

                    File.WriteAllText(path, jsonFormat);
                }
                else if (obj.GetType() == typeof(User))
                {
                    User u = (User)obj;

                    uList = ReadUserFromJSON(path);

                    uList.Add(u);

                    string jsonFormat = JsonSerializer.Serialize(uList);

                    File.WriteAllText(path, jsonFormat);
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
                Console.ReadLine();
            }
        }
        
        //method used to delete given object from "user.json"
        public void DeleteUserFromJSON(string path, User u)
        {
            try
            {
                uList.Remove(u);

                string jsonFormat = JsonSerializer.Serialize(uList);

                File.WriteAllText(path, jsonFormat);
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
                Console.ReadLine();
            }
        }

        //method used to delete given object from "book.json"
        public void DeleteBookFromJSON(string path, Book b)
        {
            try
            {
                bList.Remove(b);

                string jsonFormat = JsonSerializer.Serialize(bList);

                File.WriteAllText(path, jsonFormat);
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
                Console.ReadLine();
            }
        }
    }
}
