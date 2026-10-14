namespace WebApplication3.Converters
{
    public class WeightConverter
    {
        public double Convert(double value, string from, string to)
        {
            string cleanFrom = (from ?? "").Trim().ToLowerInvariant();
            string cleanTo = (to ?? "").Trim().ToLowerInvariant();

            // Step 1: Base conversion to Kilograms
            double kilograms = cleanFrom switch
            {
                "kg" or "kilogram" or "kilograms" => value,
                "g" or "gram" or "grams" => value / 1000,
                "lb" or "lbs" or "pound" or "pounds" => value * 0.45359237,
                "oz" or "ounce" or "ounces" => value * 0.028349523125,
                _ => throw new ArgumentException($"Unsupported source weight unit: {from}")
            };

            // Step 2: Convert from Kilograms to Target
            double result = cleanTo switch
            {
                "kg" or "kilogram" or "kilograms" => kilograms,
                "g" or "gram" or "grams" => kilograms * 1000,
                "lb" or "lbs" or "pound" or "pounds" => kilograms / 0.45359237,
                "oz" or "ounce" or "ounces" => kilograms / 0.028349523125,
                _ => throw new ArgumentException($"Unsupported target weight unit: {to}")
            };

            if (!double.IsFinite(result))
            {
                throw new ArgumentException("Conversion produced an invalid or infinite result.");
            }

            return Math.Round(result, 6);
        }
    }
}
