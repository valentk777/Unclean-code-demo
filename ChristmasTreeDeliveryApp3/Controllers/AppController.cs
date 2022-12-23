using System.Net;
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

                foreach (var result in db.GetAll(type))
                {
                    trees.Add(result);
                }
            }

            return Ok(trees);
        }

        [HttpGet]
        [Route("GetAllTreesByType")]
        public ActionResult<List<TreeObjectDtoData>> Get2([FromBody] PresentsType type)
        {
            var trees = new List<TreeObjectDtoData>();
            var db = new Database();

            foreach (var result in db.GetAll(type))
            {
                trees.Add(result);
            }

            return Ok(trees);
        }
        
        [Route("AddOrderOfTree")]
        [HttpPost]
        public async Task<ActionResult> Add1([Microsoft.AspNetCore.Mvc.FromBody] TreeObjectDtoData data)
        {
            var db = new Database();
            db.SaveTree(data.TreeName, data.TreeType, data.TreeDeliveredTo);
            return Ok();
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

