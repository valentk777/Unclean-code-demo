using ChristmasTreeDeliveryApp.Domain;
using ChristmasTreeDeliveryApp.Domain.Contracts.Requests;
using Microsoft.AspNetCore.Mvc;

namespace ChristmasTreeDeliveryApp.Api.Controllers
{
    [ApiController]
    public class AppController : ControllerBase
    {
        private readonly ILogger<AppController> _logger;
        private readonly ITreeDataProvider _treeDataProvider;

        // ORDER AND TREE CAN BE DIFFERENT CONTROLLERS
        public AppController(ILogger<AppController> logger, ITreeDataProvider treeDataProvider)
        {
            _logger = logger;
            _treeDataProvider = treeDataProvider;
        }

        [HttpGet]
        //TODO: constants
        [Route("tree")]
        public ActionResult<GetAllTreesResponse> GetAllTrees()
        {
            var result = _treeDataProvider.GetAllTrees();
            return Ok(result);
        }

        [HttpGet]
        [Route("tree/type")]
        public ActionResult<GetAllTreesByTypeResponse> GetAllTreesByType([FromQuery] GetAllTreesByTypeRequest request)
        {
            var result = _treeDataProvider.GetAllTrees((TreeType)request.TreeType);
            return Ok(result);
        }

        [HttpGet]
        //TODO: constants
        [Route("order")]
        public ActionResult<GetAllOrdersResponse> GetAllOrderedTrees()
        {
            var result = _treeDataProvider.GetAllOrderedTrees();
            return Ok(result);
        }

        [HttpGet]
        [Route("order/type")]
        public ActionResult<GetAllOrdersByTypeResponse> GetAllOrdersByType([FromQuery] GetAllOrdersByTypeRequest request)
        {
            var result = _treeDataProvider.GetAllOrderedTrees((TreeType)request.Type);
            return Ok(result);
        }

        [HttpPost]
        [Route("order")]
        public async Task<ActionResult> OrderTree([FromBody] OrderTreeRequest request)
        {
            var result = _treeDataProvider.SaveTree(request.OrderedTree);

            if (result)
            {
                return Ok();
            }

            return Conflict(new EntryPointNotFoundException("results is not okay"));
        }
    }
}
