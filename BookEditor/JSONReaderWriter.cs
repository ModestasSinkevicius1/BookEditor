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
    class JSONReaderWriter
    {
        List<Book> bList = new List<Book>();
        List<User> uList = new List<User>();

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
