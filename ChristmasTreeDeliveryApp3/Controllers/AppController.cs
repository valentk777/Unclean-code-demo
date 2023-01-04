using Microsoft.AspNetCore.Mvc;
using ChristmasTreeDeliveryApp3.DtoData;
using Microsoft.AspNetCore.SignalR;

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
        public ActionResult<List<TreeObjectDtoData>> Get1()//name is bad
        {
            var trees = new List<TreeObjectDtoData>();

            foreach (var type in new List<PresentsType>()
            {
                PresentsType.RedcedarTree, 
                PresentsType.CedarTree, 
                PresentsType.ConiferTree,
                PresentsType.CypressTree, 
                PresentsType.FirTree
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

        public class Get2Request //bad name
        {
            public int type { get; set; }
        }

        [HttpGet]
        [Route("GetAllTreesByType")]
        public ActionResult<List<TreeObjectDtoData>> Get2([FromQuery] Get2Request request) // bad name
        {
            var trees = new List<TreeObjectDtoData>();

            foreach (var new_type in new List<PresentsType>()
            {
                PresentsType.RedcedarTree,
                PresentsType.CedarTree,
                PresentsType.ConiferTree,
                PresentsType.CypressTree,
                PresentsType.FirTree
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

        // not finished yet
        private TreeObjectDtoData MethodName(List<TreeObjectDtoData> trees) 
        {
            foreach (var type in new List<PresentsType>()
            {
                PresentsType.RedcedarTree,
                PresentsType.CedarTree,
                PresentsType.ConiferTree,
                PresentsType.CypressTree,
                PresentsType.FirTree
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
        }
        [Route("AddOrderOfTree")]
        [HttpPost]
        public async Task<ActionResult> Add1([Microsoft.AspNetCore.Mvc.FromBody] TreeObjectDtoData data) // bad name
        {
            var db = new Database();
            var result = db.SaveTree(data.TreeName, data.TreeType, data.TreeDeliveredTo);

            if (result.Item1 != true)
            {
                return Conflict( new EntryPointNotFoundException("results is not okay"));
            }

            return Ok();
        }
    }
}

