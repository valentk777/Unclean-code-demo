namespace ChristmasTreeDeliveryApp.Contracts.Enitites
{
    public class Tree
    {
        public string? Name { get; set; }

        public TreeType Type { get; set; }

        public Tree()
        {
        }

        public Tree(string? name, TreeType type)
        {
            Name = name;
            Type = type;
        }
    }
}
