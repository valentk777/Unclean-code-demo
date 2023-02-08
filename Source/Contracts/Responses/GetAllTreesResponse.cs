using ChristmasTreeDeliveryApp.Contracts.Enitites;

namespace ChristmasTreeDeliveryApp.Contracts.Responses
{
    public class GetAllTreesResponse
    {
        public List<Tree>? Trees { get; set; }
    }
}