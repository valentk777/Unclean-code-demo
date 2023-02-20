using ChristmasTreeDeliveryApp.Contracts.Enitites;

namespace ChristmasTreeDeliveryApp.Contracts.Responses
{
    public class GetAllOrdersResponse
    {
        public List<Order>? Orders { get; set; }
    }
}