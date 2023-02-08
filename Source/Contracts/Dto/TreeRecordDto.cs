namespace ChristmasTreeDeliveryApp.Contracts.Dto
{
    public class TreeRecordDto
    {
        public string? Name { get; set; }

        public int Type { get; set; }

        public string? DeliveryAddress { get; set; }

        public DateTime DeliveryDate { get; set; }

        public string ToDatabaseFormat() =>
            $"{Name};{Type};{DeliveryAddress};{DeliveryDate.ToString("yyyy-MM-dd HH:mm:ss,fff")};";
    }
}
