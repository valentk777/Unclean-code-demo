using Microsoft.AspNetCore.Mvc;

namespace ChristmasTreeDeliveryApp2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AppController : ControllerBase
    {
        private readonly ILogger<AppController> _logger;

        public AppController(ILogger<AppController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetAllPresents")]
        public List<Presents> Get1()
        {

        }
    }

    public class Presents
    {

    }
}

