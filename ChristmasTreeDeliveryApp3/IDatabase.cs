using ChristmasTreeDeliveryApp3.Enums;
namespace ChristmasTreeDeliveryApp3
{
    public interface IDatabase
    {
        List<OrderedTree> GetAllTrees(PresentsType type);

        ResultAfterSave SaveTree(string name, PresentsType type, string to);
    }
}