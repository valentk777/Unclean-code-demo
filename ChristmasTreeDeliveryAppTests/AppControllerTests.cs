using ChristmasTreeDeliveryApp3.Controllers;
using ChristmasTreeDeliveryApp3.Enums;
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
        public void controllerGet1_empty_null()
        {
            var logger = new Mock<ILogger<AppController>>();
            File.Delete("treeRecord.txt");

            var controller = new AppController(logger.Object);

            var result = controller.Get1().Result as OkObjectResult;

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