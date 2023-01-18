using API;
using Microsoft.AspNetCore.Mvc;

namespace ChristmasTreeDeliveryApp3.Controllers
{
    [ApiController]
    public class AppController : ControllerBase
    {
        private readonly ILogger<AppController> _logger;
        private readonly IDatabase _database;

        public AppController(ILogger<AppController> logger, IDatabase database)
        {
            _logger = logger;
            _database = database;
        }

        [HttpGet]
        [Route("GetAllTrees")]
        public ActionResult<List<TreeObjectDtoData>> GetAllTrees()
        {
            var trees = new List<TreeObjectDtoData>();

            foreach (PresentsType type in Enum.GetValues(typeof(PresentsType)))
            {
                trees.AddRange(_database.GetAllTrees(type));
            }

            return Ok(trees);
        }

        [HttpGet]
        [Route("GetAllTreesByType")]
        public ActionResult<List<TreeObjectDtoData>> GetAllTreesByType([FromQuery] GetAllTreesByTreeTypeRequest request)
        {
            var trees = _database.GetAllTrees((PresentsType)request.Type);
            
            return Ok(trees);
        }

        [HttpPost]
        [Route("AddOrderOfTree")]
        public async Task<ActionResult> AddOrderOfTree([FromBody] TreeObjectDtoData data)
        {
            var result = _database.SaveTree(data.TreeName, data.TreeType, data.TreeDeliveredTo);

            if (result.Item1)
            {
                return Ok();
            }

            return Conflict(new EntryPointNotFoundException("results is not okay"));
        }
    }
}

