using ChristmasTreeDeliveryApp.Contracts.Enitites;
using ChristmasTreeDeliveryApp.Contracts.Responses;

namespace ChristmasTreeDeliveryApp.Domain
{
    public interface ITreeDataProvider
    {
        GetAllTreesResponse GetAllTrees();

        GetAllTreesByTypeResponse GetAllTrees(TreeType type);

        GetAllOrdersResponse GetAllOrderedTrees();

        GetAllOrdersByTypeResponse GetAllOrderedTrees(TreeType type);

        bool SaveTree(Order order);
    }
}