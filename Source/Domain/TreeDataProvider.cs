using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChristmasTreeDeliveryApp.Api;
using ChristmasTreeDeliveryApp.Api.Controllers;
using ChristmasTreeDeliveryApp.Api.Integration;

namespace ChristmasTreeDeliveryApp.Domain
{
    public class TreeDataProvider : ITreeDataProvider
    {
        private readonly IDatabase _database;

        public TreeDataProvider(IDatabase database)
        {
            _database = database;
        }

        public GetAllOrdersResponse GetAllOrderedTrees()
        {

        }

        public GetAllOrdersByTypeResponse GetAllOrderedTrees(TreeType type)
        {
            throw new NotImplementedException();
        }

        public GetAllTreesResponse GetAllTrees()
        {
            var result = _database.GetAllTrees();

            return new GetAllOrdersResponse()
            {
                Orders = result
            };
        }

        public GetAllTreesByTypeResponse GetAllTrees(TreeType type)
        {
            throw new NotImplementedException();
        }

        public bool SaveTree(Order order)
        {
            throw new NotImplementedException();
        }
    }
}
