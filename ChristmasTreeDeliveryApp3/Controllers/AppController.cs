using System.ComponentModel;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Metrics;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using ChristmasTreeDeliveryApp3.Controllers;
using ChristmasTreeDeliveryApp3.DtoData;

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
                    else
                    {
                        continue;
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
                    else
                    {
                        continue;
                    }
                }
            }

            return Ok(trees);
        }
        
        [Route("AddOrderOfTree")]
        [HttpPost]
        public async Task<ActionResult> Add1([Microsoft.AspNetCore.Mvc.FromBody] TreeObjectDtoData data)
        {
            var db = new Database();
            var result = db.SaveTree(data.TreeName, data.TreeType, data.TreeDeliveredTo);

            if (result.Item1 == true)
            {
                return Ok();
            }
            else
            {
                throw new EntryPointNotFoundException("results is not okay");
                return Conflict();
            }
        }
    }
}

