using Microsoft.VisualStudio.TestTools.UnitTesting;
using BookEditor;
using System;

namespace BookEditor.Tests
{
    [TestClass]
    public class BookCommandManagerTests
    {
        [TestMethod]
        public void AddNewBook_AddBookToJSONFile_ExpectedNoException()
        {
            BookCommandManager bcm = new BookCommandManager();

            string testCommandAddress = "add test_name test_author test_category test_language 1000-01-01 00000000";
            string[] testCommandKeys = testCommandAddress.Split(" ");
          
            //bcm.AddNewBook(testCommandKeys);

            //Assert.IsTrue(bcm.isFunctionExecutedSuccesfuly());
        }
    }
}
