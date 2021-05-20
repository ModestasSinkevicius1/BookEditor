using Microsoft.VisualStudio.TestTools.UnitTesting;
using BookEditor;
using System;

namespace BookEditor.Tests
{
    [TestClass]
    public class BookTests
    {
        [TestMethod]
        public void Book_GetObjectProperties_AreEqual()
        {
            Book bExpected, bActual;

            bExpected = new Book("test_name", "test_author", "test_category", "test_language", new DateTime(1999 - 01 - 01), 00000000);

            bActual = bExpected;

            Assert.AreEqual(bExpected, bActual);
        }
    }
}