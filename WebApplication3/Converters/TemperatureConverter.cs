namespace WebApplication3.Converters
{
    public class TemperatureConverter
    {
        public double Convert(double value, string from, string to)
        {
            string cleanFrom = (from ?? "").Trim().ToLowerInvariant();
            string cleanTo = (to ?? "").Trim().ToLowerInvariant();

            // Step 1: Base conversion to Celsius
            double celsius = cleanFrom switch
            {
                "c" or "celsius" => value,
                "f" or "fahrenheit" => (value - 32) * 5 / 9,
                "k" or "kelvin" => value - 273.15,
                _ => throw new ArgumentException($"Unsupported source temperature unit: {from}")
            };

            // Step 2: Convert from Celsius to Target
            double result = cleanTo switch
            {
                "c" or "celsius" => celsius,
                "f" or "fahrenheit" => (celsius * 9 / 5) + 32,
                "k" or "kelvin" => celsius + 273.15,
                _ => throw new ArgumentException($"Unsupported target temperature unit: {to}")
            };

            if (!double.IsFinite(result))
            {
                throw new ArgumentException("Conversion produced an invalid or infinite result.");
            }

            return Math.Round(result, 2);
        }
    }
}
