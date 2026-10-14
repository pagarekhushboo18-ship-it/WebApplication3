
namespace WebApplication3.Models
{
    public class ConversionResponse
    {
        public double OriginalValue { get; set; }
        public string FromUnit { get; set; } = string.Empty;
        public string ToUnit { get; set; } = string.Empty;
        public double ConvertedValue { get; set; }
    }
}
