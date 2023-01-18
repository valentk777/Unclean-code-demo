namespace ChristmasTreeDeliveryApp3
{
    public interface IDatabase
    {
        List<TreeObjectDtoData> GetAllTrees(PresentsType type);

        ResultAfterSave SaveTree(string name, PresentsType type, string to);
    }
}