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

        public List<Book> ReadJSON(string path)
        {
            string jsonText = File.ReadAllText(path);
            bList = JsonSerializer.Deserialize<List<Book>>(jsonText);        

            return bList;
        }

        public List<User> ReadUserJSON(string path)
        {
            string jsonText = File.ReadAllText(path);
            uList = JsonSerializer.Deserialize<List<User>>(jsonText);          

            return uList;
        }

        public void WriteJSON(object obj, string path)
        {
            if (obj.GetType() == typeof(Book))
            {
                Book b = (Book)obj;
    
                bList = ReadJSON(path);

                bList.Add(b);

                string jsonFormat = JsonSerializer.Serialize(bList);

                File.WriteAllText(path, jsonFormat);
            }
            else if(obj.GetType() == typeof(User))
            {
                User u = (User)obj;

                uList = ReadUserJSON(path);
                
                uList.Add(u);
                
                string jsonFormat = JsonSerializer.Serialize(uList);

                File.WriteAllText(path, jsonFormat);
               
            }
        }
        
        public void UpdateJSON(string path, User u)
        {
            uList.Remove(u);

            string jsonFormat = JsonSerializer.Serialize(uList);

            File.WriteAllText(path, jsonFormat);
        }
    }
}
