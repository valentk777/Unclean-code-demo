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
        public ActionResult<List<OrderedTree>> GetAllTrees()
        {
            var trees = new List<OrderedTree>();

            foreach (PresentsType type in Enum.GetValues(typeof(PresentsType)))
            {
                trees.AddRange(_database.GetAllTrees(type));
            }

            return Ok(trees);
        }

        [HttpGet]
        [Route("GetAllTreesByType")]
        public ActionResult<List<OrderedTree>> GetAllTreesByType([FromQuery] GetAllTreesByTreeTypeRequest request)
        {
            // TODO: throw exception if type does not exist
            var trees = _database.GetAllTrees((PresentsType)request.Type);

            return Ok(trees);
        }

        [HttpPost]
        [Route("AddOrderOfTree")]
        public async Task<ActionResult> AddOrderOfTree([FromBody] OrderedTree data)
        {
            var result = _database.SaveTree(data.Name, data.Type, data.DeliveryAddress);

            if (result.IsSaveWasSucessful)
            {
                return Ok();
            }

            return Conflict(new EntryPointNotFoundException("results is not okay"));
        }
    }
}
