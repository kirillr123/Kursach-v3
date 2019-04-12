using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kursach_v3.Models;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kursach_v3.Tests
{
    [TestClass]
    public class TextTests
    {
        [TestMethod]
        public void Decipher_PositiveShift_ReturnsTrue()
        {
            //Arrange
            var text = new Text();
            text.AllText = new string []{ "эюя" };
            text.Shift = 1;

            //Act
            text.Decipher();

            //Assert
            Assert.IsTrue(text.ReadableText[0] == "юяа");
        }
        [TestMethod]
        public void Decipher_NegativeShift_ReturnsTrue()
        {
            //Arrange
            var text = new Text();
            text.AllText = new string[] { "абвг"};
            text.Shift = -1;

            //Act
            text.Decipher();

            //Assert
            Assert.IsTrue(text.ReadableText[0] == "яабв");
        }
        [TestMethod]
        public void Decipher_ZeroShift_ReturnsTrue()
        {
            //Arrange
            var text = new Text();
            text.AllText = new string[] { "абвг" };
            text.Shift = 0;

            //Act
            text.Decipher();

            //Assert
            Assert.IsTrue(text.ReadableText[0] == "абвг");
        }
    }
}
