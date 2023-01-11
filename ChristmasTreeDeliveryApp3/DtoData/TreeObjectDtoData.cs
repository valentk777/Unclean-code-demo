using ChristmasTreeDeliveryApp3.Enums;
namespace ChristmasTreeDeliveryApp3.DtoData
{
    public class TreeObjectDtoData
    {
        public string TreeName { get; set; }

        public PresentsType TreeType { get; set; }

        public string TreeDeliveredTo { get; set; }

        public DateTime TreeDeliveredDate { get; set; } = DateTime.UtcNow;
    }
}
