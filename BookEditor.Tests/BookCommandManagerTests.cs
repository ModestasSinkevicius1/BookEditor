using Microsoft.VisualStudio.TestTools.UnitTesting;
using BookEditor;
using System;
using System.Collections.Generic;

namespace BookEditor.Tests
{
    [TestClass]
    public class BookCommandManagerTests
    {
        //successfull commands
        [TestMethod]
        public void AddNewBook_AddBookToJSONFile_ExpectedSuccessfullAddedBookMessage()
        {
            BookCommandManager bcm = new BookCommandManager();

            string expected = "New book has been added";
            string actual = "";

            string testCommandAddress = "add test_name test_author test_category test_language 1000-01-01 00000000";
            string[] testCommandKeys = testCommandAddress.Split(" ");
          
            actual = bcm.AddNewBook(testCommandKeys);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ReadBookList_ReadBooksFromJSONFileNotExist_ExpectedReturnNull()
        {
            BookCommandManager bcm = new BookCommandManager();
           
            List<Book> expected = null;
            List<Book> actual;

            //intentionaly made a spelling mistake to get wanted result
            string testCommandAddress = "readall isbns";
            string[] testCommandKeys = testCommandAddress.Split(" ");

            actual = bcm.ReadBookList(testCommandKeys);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TakeBook_TakeBookFromLibrary_ExpectedSuccessfullyTakeABookMessage()
        {
            BookCommandManager bcm = new BookCommandManager();            
            
            string expected = "Thank you taking our book";
            string actual = "";

            string testCommandAddress = "take test_name test_anonymous 1";
            string[] testCommandKeys = testCommandAddress.Split(" ");

            actual = bcm.TakeBook(testCommandKeys);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TakeBook_TakeBookFromLibraryWithPeriodThreeMonths_ExpectedNotAllowBookMessage()
        {
            BookCommandManager bcm = new BookCommandManager();

            string expected = "You can't take a book longer than 2 months";
            string actual = "";

            string testCommandAddress = "take test_name test_anonymous 3";
            string[] testCommandKeys = testCommandAddress.Split(" ");

            actual = bcm.TakeBook(testCommandKeys);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TakeBook_TakeBookFromLibraryWithMoreThanThreeBooks_ExpectedNotAllowBookMessage()
        {
            BookCommandManager bcm = new BookCommandManager();

            string expected = "You took to many books";
            string actual = "";
                  
            string testCommandAddress = "take test_name test_anonymous_2 1";           

            string[] testCommandKeys = testCommandAddress.Split(" ");

            //doing it four times since "user.json" can be modified from other tests
            for(int i = 0; i < 4; i++)
                actual = bcm.TakeBook(testCommandKeys);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TakeBook_TakeBookFromLibraryButBookNotExist_ExpectedBookNotFoundMessage()
        {
            BookCommandManager bcm = new BookCommandManager();

            string expected = "Book not found";
            string actual = "";

            string testCommandAddress = "take test_name_not test_anonymous_2 1";

            string[] testCommandKeys = testCommandAddress.Split(" ");
            
            actual = bcm.TakeBook(testCommandKeys);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ReturnBook_ReturnBookToLibrary_ExpectedReturnedBookMessage()
        {
            BookCommandManager bcm = new BookCommandManager();

            string expected = "Book has been returned";
            string actual = "";

            string testCommandAddress = "return test_name test_anonymous";
            string[] testCommandKeys = testCommandAddress.Split(" ");
         
            actual = bcm.ReturnBook(testCommandKeys);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ReturnBook_ReturnBookToLibraryLate_ExpectedFunnyMessage()
        {
            BookCommandManager bcm = new BookCommandManager();

            string expected = "You done goofed";
            string actual = "";

            string testCommandAddress = "return test_name test_anonymous";
            string[] testCommandKeys = testCommandAddress.Split(" ");

            actual = bcm.ReturnBook(testCommandKeys);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ReturnBook_ReturnBookToLibraryButNotExist_ExpectedBookNotFoundMessage()
        {
            BookCommandManager bcm = new BookCommandManager();

            string expected = "Book not found";
            string actual = "";

            string testCommandAddress = "return test_name_not test_anonymous";
            string[] testCommandKeys = testCommandAddress.Split(" ");

            actual = bcm.ReturnBook(testCommandKeys);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DeleteBook_DeleteBookFromLibrary_ExpectedSuccessfullDeletedBookMessage()
        {
            BookCommandManager bcm = new BookCommandManager();

            string expected = "Book has been deleted";
            string actual = "";

            string testCommandAddress = "delete test_name";
            string[] testCommandKeys = testCommandAddress.Split(" ");

            actual = bcm.DeleteBook(testCommandKeys);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DeleteBook_DeleteBookFromLibraryButNotExist_ExpectedBookNotFoundMessage()
        {
            BookCommandManager bcm = new BookCommandManager();

            string expected = "Book not found";
            string actual = "";

            string testCommandAddress = "delete test_name_not";
            string[] testCommandKeys = testCommandAddress.Split(" ");

            actual = bcm.DeleteBook(testCommandKeys);

            Assert.AreEqual(expected, actual);
        }                       
    }
}
