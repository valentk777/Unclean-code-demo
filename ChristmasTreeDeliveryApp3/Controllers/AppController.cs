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
        public ActionResult<List<TreeObjectDtoData>> Get1()
        {
            var trees = new List<TreeObjectDtoData>();

            foreach (var type in new List<PresentsType>()
            {
                PresentsType.RedcedarTree, PresentsType.CedarTree, PresentsType.ConiferTree, PresentsType.CypressTree, PresentsType.FirTree
            })
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

        public class Get2Request
        {
            public int type { get; set; }
        }

        [HttpGet]
        [Route("GetAllTreesByType")]
        public ActionResult<List<TreeObjectDtoData>> Get2([FromQuery] Get2Request request)
        {
            var trees = new List<TreeObjectDtoData>();

            foreach (var new_type in new List<PresentsType>()
            {
                PresentsType.RedcedarTree, PresentsType.CedarTree, PresentsType.ConiferTree, PresentsType.CypressTree, PresentsType.FirTree
            })
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
                            continue;
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

