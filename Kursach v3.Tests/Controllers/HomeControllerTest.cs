using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Kursach_v3;
using Kursach_v3.Controllers;
using Kursach_v3.Models;

namespace Kursach_v3.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void TestIndexView()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.AreEqual("Index", result.ViewName);
        }

        [TestMethod]
        public void TestInstantiation()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;
            var obj = (Text)result.ViewData.Model;
            // Assert
            Assert.AreNotEqual(null, obj);
        }

        [TestMethod]
        public void TestNoFileMessage()
        {
            //Arrange
            HomeController controller = new HomeController();
            System.Web.HttpPostedFileBase file = null;
            var Shift = 3;
            var text = new Text();
            var ExpectedMessage = "Вы что-то сделали не так! У меня все работало как надо! Попробуйте загрузить файл .txt или .docx формата с текстом!";
            //Act
            ViewResult result = controller.Index(file,text,Shift) as ViewResult;

            //Assert
            Assert.AreEqual(result.ViewBag.Message, ExpectedMessage);
        }
        
        
    }
}
