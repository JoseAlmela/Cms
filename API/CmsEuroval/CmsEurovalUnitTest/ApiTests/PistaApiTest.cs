using Castle.Core.Logging;
using CmsEuroval;
using EurovalBusinessLogic.Services;
using EurovalBusinessLogic.Services.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CmsEurovalUnitTest
{
    [TestClass]
    public class PistaApiTest
    {
        private Mock<IEurovalCmsService> mockService;
        private Mock<ILogger<PistasController>> mockLog;

        private PistasController SUT { get; set; }

        [TestInitialize]
        public void Setup()
        {
            mockService = new Mock<IEurovalCmsService>();
            mockLog = new Mock<ILogger<PistasController>>();
            SUT = new PistasController(mockService.Object, mockLog.Object);
        }

        private List<PistaViewModel> GetTestPistas()
        {
            return new List<PistaViewModel>
            {
                new PistaViewModel{ Id=1, Nombre="paddington"},
                new PistaViewModel { Id=2, Nombre="futbito"},
            };
        }

        [TestMethod]
        public async Task GetPistas_WhenCalled_ReturnsA200Ok()
        {
            // Arrange
            mockService.Setup(serv => serv.GetPistasAsync())
                       .ReturnsAsync(GetTestPistas());

            // Act
            var result = await SUT.GetPistas();
          
            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task GetPistas_WhenCalled_ReturnsAllPistaViewModel()
        {

            // Arrange
            mockService.Setup(serv =>  serv.GetPistasAsync())
                .ReturnsAsync(GetTestPistas());

            // Act
            var result = await SUT.GetPistas();
            var resPistas = ((OkObjectResult)(result.Result)).Value as List<PistaViewModel>;
            
            // Assert
            Assert.IsTrue(((OkObjectResult)(result.Result)).Value.GetType().IsAssignableFrom(typeof(List<PistaViewModel>)));
            Assert.AreEqual(GetTestPistas().Count, resPistas.Count);
                       
        }

        [TestMethod]
        public async Task GetPistas_WhenCalled_ReturnsA400BadRequest()
        {

            // Arrange
            mockService.Setup(serv => serv.GetPistasAsync())
                .Throws(new System.Exception());

            // Act
            var result = await SUT.GetPistas();

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task PutPista_WhenCalled_ReturnsA400BadRequestWithModelError()
        {

            // Arrange
            int id = 3;
            SUT.ModelState.AddModelError("Required", "Name is missing");

            // Act
            var result = await SUT.PutPista(id, new PistaViewModel { Id = id });

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult));
        }
    }
}
