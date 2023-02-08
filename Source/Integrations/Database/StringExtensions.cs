namespace ChristmasTreeDeliveryApp.Integrations.Database
{
    public static class StringExtensions
    {
        public static DateTime ToDateTime(this string text) =>
            DateTime.ParseExact(text, "yyyy-MM-dd HH:mm:ss,fff", System.Globalization.CultureInfo.InvariantCulture);
    }
}
