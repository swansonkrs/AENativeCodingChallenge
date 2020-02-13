using Microsoft.VisualStudio.TestTools.UnitTesting;
using AEWebApp;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAppTests
{
    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        public void TimeGet_ShouldGetTime()
        {
            // Arrange
            var httpContext = new DefaultHttpContext();
            var contContext = new ControllerContext() { HttpContext = httpContext };
            var timeController = new TimeController() { ControllerContext = contContext };


            // Act
            var result = timeController.GET();

            // Assert
            Assert.AreEqual(((OkObjectResult)result).Value, DateTime.Now.ToString("H:mm:ss tt"));
        }
    }
}
