namespace ChristmasTreeDeliveryApp.Contracts.Enitites
{
    public class Order
    {
        public Tree Tree { get; set; }

        public string? DeliveryAddress { get; set; }

        public DateTime DeliveryDate { get; set; } = DateTime.UtcNow;

        public Order()
        {
        }

        public Order(Tree tree, string deliveryAddress)
        {
            Tree = tree;
            DeliveryAddress = deliveryAddress;
        }
    }
}
