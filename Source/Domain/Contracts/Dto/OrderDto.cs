namespace ChristmasTreeDeliveryApp.Api
{
    public class OrderDto
    {
        public Tree Tree { get; set; } 

        public string DeliveryAddress { get; set; }

        public DateTime DeliveryDate { get; set; } = DateTime.UtcNow;

        public OrderDto(Tree tree, string deliveryAddress)
        {
            Tree = tree;
            DeliveryAddress = deliveryAddress;
        }

        public string ToDatabaseFormat() =>
            $"{Name};{Type};{DeliveryAddress};{DeliveryDate.ToString("yyyy-MM-dd HH:mm:ss,fff")};";
    }
}
