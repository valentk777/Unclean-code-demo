using Microsoft.AspNetCore.Mvc;

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

        public class GetAllTreesByTypeRequest
        {
            public int type { get; set; }
        }

        [HttpGet]
        [Route("GetAllTreesByTreeType")]
        public ActionResult<List<TreeObjectDtoData>> GetAllTreesByType([FromQuery] GetAllTreesByTypeRequest request)
        {
            var trees = new List<TreeObjectDtoData>();

            foreach (PresentsType new_type in Enum.GetValues(typeof(PresentsType)))
            { 
                var db = new Database();

                foreach (var result in db.AllTrees(new_type))
                {
                    if (result != null)
                    {
                        if ((int)result.TreeType == request.type)
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
        public async Task<ActionResult> Add1([FromBody] TreeObjectDtoData data)
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

    public class TreeObjectDtoData
    {
        public string TreeName { get; set; }

        public PresentsType TreeType { get; set; }

        public string TreeDeliveredTo { get; set; }

        public DateTime TreeDeliveredDate { get; set; } = DateTime.UtcNow;
    }
}

