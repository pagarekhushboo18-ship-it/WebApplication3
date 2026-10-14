namespace WebApplication3.Converters
{
    public class LengthConverter
    {
        public double Convert(double value, string from, string to)
        {
            string cleanFrom = (from ?? "").Trim().ToLowerInvariant();
            string cleanTo = (to ?? "").Trim().ToLowerInvariant();

            // Step 1: Base conversion to Meters
            double meters = cleanFrom switch
            {
                "m" or "meter" or "meters" => value,
                "km" or "kilometer" or "kilometers" => value * 1000,
                "cm" or "centimeter" or "centimeters" => value / 100,
                "mm" or "millimeter" or "millimeters" => value / 1000,
                "ft" or "foot" or "feet" => value * 0.3048,
                "in" or "inch" or "inches" => value * 0.0254,
                _ => throw new ArgumentException($"Unsupported source length unit: {from}")
            };

            // Step 2: Convert from Meters to Target
            double result = cleanTo switch
            {
                "m" or "meter" or "meters" => meters,
                "km" or "kilometer" or "kilometers" => meters / 1000,
                "cm" or "centimeter" or "centimeters" => meters * 100,
                "mm" or "millimeter" or "millimeters" => meters * 1000,
                "ft" or "foot" or "feet" => meters / 0.3048,
                "in" or "inch" or "inches" => meters / 0.0254,
                _ => throw new ArgumentException($"Unsupported target length unit: {to}")
            };

            if (!double.IsFinite(result))
            {
                throw new ArgumentException("Conversion produced an invalid or infinite result.");
            }

            return Math.Round(result, 6);
        }
    }
}
