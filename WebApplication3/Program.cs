
using WebApplication3.Converters;
using WebApplication3.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

// Register converter classes
builder.Services.AddSingleton<LengthConverter>();
builder.Services.AddSingleton<TemperatureConverter>();
builder.Services.AddSingleton<WeightConverter>();

// Register the conversion service
builder.Services.AddSingleton<UnitConversionService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
