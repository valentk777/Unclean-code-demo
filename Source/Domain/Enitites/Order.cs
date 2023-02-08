using ChristmasTreeDeliveryApp.Domain.Enitites;

namespace ChristmasTreeDeliveryApp.Api
{
    public class Order
    {
        public Tree Tree { get; set; } 

        public string DeliveryAddress { get; set; }

        public DateTime DeliveryDate { get; set; } = DateTime.UtcNow;

        public Order(Tree tree, string deliveryAddress)
        {
            Tree = tree;
            DeliveryAddress = deliveryAddress;
        }

        public string ToDatabaseFormat() =>
            $"{Name};{Type};{DeliveryAddress};{DeliveryDate.ToString("yyyy-MM-dd HH:mm:ss,fff")};";

        // Note: just an option.
        public override string ToString() =>
            $"{Name};{Type};{DeliveryAddress};{DeliveryDate.ToString("yyyy-MM-dd HH:mm:ss,fff")};";
    }
}
