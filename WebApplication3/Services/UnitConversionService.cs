
using WebApplication3.Converters;

namespace WebApplication3.Services;

public class UnitConversionService
{
    private readonly LengthConverter _lengthConverter;
    private readonly TemperatureConverter _temperatureConverter;
    private readonly WeightConverter _weightConverter;


    public UnitConversionService(
        LengthConverter lengthConverter,
        TemperatureConverter temperatureConverter,
        WeightConverter weightConverter)
    {
        _lengthConverter = lengthConverter;
        _temperatureConverter = temperatureConverter;
        _weightConverter = weightConverter;
    }


    public double Convert(double value, string from, string to)
    {
        if (!double.IsFinite(value))
        {
            throw new ArgumentException(
                "Value must be a finite number.");
        }

        if (string.IsNullOrWhiteSpace(from) ||
            string.IsNullOrWhiteSpace(to))
        {
            throw new ArgumentException(
                "Source and target units are required.");
        }

        from = from.Trim();
        to = to.Trim();

        if (IsTemperature(from) && IsTemperature(to))
        {
            return _temperatureConverter.Convert(value, from, to);
        }

        if (IsTemperature(from) || IsTemperature(to))
        {
            throw new ArgumentException(
                "Cannot convert between temperature and another category.");
        }

        if (IsLength(from) && IsLength(to))
        {
            return _lengthConverter.Convert(value, from, to);
        }

        if (IsWeight(from) && IsWeight(to))
        {
            return _weightConverter.Convert(value, from, to);
        }

        if (IsLength(from) || IsWeight(from))
        {
            if (IsLength(to) || IsWeight(to))
            {
                throw new ArgumentException(
                    "Cannot convert between different measurement categories.");
            }
        }

        throw new ArgumentException(
            $"Unsupported source or target unit: {from}, {to}");
    }

    private static bool IsTemperature(string unit) =>
        unit.Equals("c", StringComparison.OrdinalIgnoreCase) ||
        unit.Equals("celsius", StringComparison.OrdinalIgnoreCase) ||
        unit.Equals("f", StringComparison.OrdinalIgnoreCase) ||
        unit.Equals("fahrenheit", StringComparison.OrdinalIgnoreCase) ||
        unit.Equals("k", StringComparison.OrdinalIgnoreCase) ||
        unit.Equals("kelvin", StringComparison.OrdinalIgnoreCase);

    private static bool IsLength(string unit) =>
        unit.Equals("m", StringComparison.OrdinalIgnoreCase) ||
        unit.Equals("meter", StringComparison.OrdinalIgnoreCase) ||
        unit.Equals("meters", StringComparison.OrdinalIgnoreCase) ||
        unit.Equals("km", StringComparison.OrdinalIgnoreCase) ||
        unit.Equals("kilometer", StringComparison.OrdinalIgnoreCase) ||
        unit.Equals("kilometers", StringComparison.OrdinalIgnoreCase) ||
        unit.Equals("cm", StringComparison.OrdinalIgnoreCase) ||
        unit.Equals("centimeter", StringComparison.OrdinalIgnoreCase) ||
        unit.Equals("centimeters", StringComparison.OrdinalIgnoreCase) ||
        unit.Equals("mm", StringComparison.OrdinalIgnoreCase) ||
        unit.Equals("millimeter", StringComparison.OrdinalIgnoreCase) ||
        unit.Equals("millimeters", StringComparison.OrdinalIgnoreCase) ||
        unit.Equals("ft", StringComparison.OrdinalIgnoreCase) ||
        unit.Equals("foot", StringComparison.OrdinalIgnoreCase) ||
        unit.Equals("feet", StringComparison.OrdinalIgnoreCase) ||
        unit.Equals("in", StringComparison.OrdinalIgnoreCase) ||
        unit.Equals("inch", StringComparison.OrdinalIgnoreCase) ||
        unit.Equals("inches", StringComparison.OrdinalIgnoreCase);

    private static bool IsWeight(string unit) =>
        unit.Equals("kg", StringComparison.OrdinalIgnoreCase) ||
        unit.Equals("kilogram", StringComparison.OrdinalIgnoreCase) ||
        unit.Equals("kilograms", StringComparison.OrdinalIgnoreCase) ||
        unit.Equals("g", StringComparison.OrdinalIgnoreCase) ||
        unit.Equals("gram", StringComparison.OrdinalIgnoreCase) ||
        unit.Equals("grams", StringComparison.OrdinalIgnoreCase) ||
        unit.Equals("lb", StringComparison.OrdinalIgnoreCase) ||
        unit.Equals("lbs", StringComparison.OrdinalIgnoreCase) ||
        unit.Equals("pound", StringComparison.OrdinalIgnoreCase) ||
        unit.Equals("pounds", StringComparison.OrdinalIgnoreCase) ||
        unit.Equals("oz", StringComparison.OrdinalIgnoreCase) ||
        unit.Equals("ounce", StringComparison.OrdinalIgnoreCase) ||
        unit.Equals("ounces", StringComparison.OrdinalIgnoreCase);
}
