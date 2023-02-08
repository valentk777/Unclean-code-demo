using ChristmasTreeDeliveryApp.Contracts.Enitites;

namespace ChristmasTreeDeliveryApp.Contracts.Responses
{
    public class GetAllOrdersByTypeResponse
    {
        public List<Order>? Orders { get; set; }
    }
}