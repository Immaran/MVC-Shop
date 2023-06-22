using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MVCProject.Controllers;
using Newtonsoft.Json.Linq;

namespace MVCProject.Tests
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void TestDetailsView()
        {
            var controller = new HomeController();
            var result = controller.Details(2);
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
    }
}