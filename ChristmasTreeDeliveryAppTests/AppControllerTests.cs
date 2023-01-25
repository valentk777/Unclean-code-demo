using ChristmasTreeDeliveryApp3;
using ChristmasTreeDeliveryApp3.Controllers;
using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace ChristmasTreeDeliveryAppTests
{
    public class AppControllerTests
    {
        [Fact]
        public void controllerGetAllTrees1_empty_null()
        {
            var logger = new Mock<ILogger<AppController>>();
            var database = new Mock<IDatabase>();
            File.Delete("treeRecord.txt");

            var controller = new AppController(logger.Object, database.Object);

            var result = controller.GetAllTrees().Result as OkObjectResult;

            result.Should().NotBeNull();
            using (new AssertionScope())
            {
                Assert.NotEqual(null, result.Value);
            }
            
            // TODO: fix later
            //logger.Verify(x => x.LogError(It.IsAny<string>()), Times.Never);
        }
    }
}