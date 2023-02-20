using ChristmasTreeDeliveryApp.Contracts.Enitites;

namespace ChristmasTreeDeliveryApp.Contracts.Responses
{
    public class GetAllTreesByTypeResponse
    {
        public List<Tree>? Trees { get; set; }
    }
}