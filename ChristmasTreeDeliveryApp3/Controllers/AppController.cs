using API;
using ChristmasTreeDeliveryApp3.DtoData;
using Microsoft.AspNetCore.Mvc;
using ChristmasTreeDeliveryApp3.Enums;

namespace ChristmasTreeDeliveryApp3.Controllers
{
    [ApiController]
    public class AppController : ControllerBase
    {
        private readonly ILogger<AppController> _logger;

        public AppController(ILogger<AppController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("GetAllTrees")]
        public ActionResult<List<TreeObjectDtoData>> GetAllTrees()
        {
            var trees = new List<TreeObjectDtoData>();

            foreach (PresentsType type in Enum.GetValues(typeof(PresentsType)))
            {
                var db = new Database();

                foreach (var result in db.AllTrees(type))
                {
                    if (result != null)
                    {
                        trees.Add(result);
                    }
                }
            }

            return Ok(trees);
        }

        [HttpGet]
        [Route("GetAllTreesByType")]
        public ActionResult<List<TreeObjectDtoData>> GetAllTreesByType([FromQuery] GetAllTreesByTreeTypeRequest request)
        {
            var trees = new List<TreeObjectDtoData>();

            foreach (PresentsType new_type in Enum.GetValues(typeof(PresentsType)))
            {
                var db = new Database();

                foreach (var result in db.AllTrees(new_type))
                {
                    if (result != null)
                    {
                        if ((int)result.TreeType == request.Type)
                        {
                            trees.Add(result);
                        }
                        else
                        {
                            _logger.LogError("nothing to log");
                        }
                    }
                }
            }

            return Ok(trees);
        }

        [Route("AddOrderOfTree")]
        [HttpPost]
        public async Task<ActionResult> AddOrderOfTree([FromBody] TreeObjectDtoData data)
        {
            var db = new Database();
            var result = db.SaveTree(data.TreeName, data.TreeType, data.TreeDeliveredTo);

            if (result.Item1)
            {
                return Ok();
            }

            return Conflict(new EntryPointNotFoundException("results is not okay"));
        }
    }
}
