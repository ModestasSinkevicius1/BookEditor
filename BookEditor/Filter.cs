﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookEditor
{
    class Filter
    {
        private List<Book> bTempList = new List<Book>();
        public List<Book> FilterBookList(List<Book> bList, string filterKey)
        {
            bTempList.Clear();

            if (filterKey == "name")
                bTempList = bList.OrderBy(i => i.name).ToList();
            else if(filterKey == "author")
                bTempList = bList.OrderBy(i => i.author).ToList();
            else if (filterKey == "category")
                bTempList = bList.OrderBy(i => i.category).ToList();
            else if (filterKey == "language")
                bTempList = bList.OrderBy(i => i.language).ToList();
            else if (filterKey == "date")
                bTempList = bList.OrderBy(i => i.publicationDate).ToList();
            else if (filterKey == "isbn")
                bTempList = bList.OrderBy(i => i.isbn).ToList();           

            return bTempList;
        }

        public List<Book> ShowTakenBookList(List<User> uList)
        {
            bTempList.Clear();

            foreach(User uRow in uList)
            {
                bTempList.Add(uRow.bookName);
            }

            return bTempList;
        }
    }
}
