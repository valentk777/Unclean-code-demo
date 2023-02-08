using ChristmasTreeDeliveryApp3.Enums;
namespace ChristmasTreeDeliveryApp3
{
    public class OrderedTree
    {
        public string Name { get; set; }

        public PresentsType Type { get; set; }

        public string DeliveryAddress { get; set; }

        public DateTime DeliveryDate { get; set; } = DateTime.UtcNow;

        public OrderedTree() 
        {
        }

        public OrderedTree(string name, PresentsType type, string deliveryAddress)
        {
            Name = name;
            Type = type;
            DeliveryAddress = deliveryAddress;
        }
    }
}
