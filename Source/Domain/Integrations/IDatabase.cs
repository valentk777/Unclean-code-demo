namespace ChristmasTreeDeliveryApp.Api.Integration
{
    public interface IDatabase
    {
        List<OrderedTree> GetAllTrees(PresentsType type);

        List<OrderedTree> GetAllTrees();

        ResultAfterSave SaveTree(OrderedTree orderedTree);
    }
}