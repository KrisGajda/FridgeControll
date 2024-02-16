using FridgeControll;

Console.WriteLine("Welcome to FridgeCont");
Console.WriteLine("-----------------------------------------");
Console.WriteLine("Control temperature in laboratory fridge.");
Console.WriteLine("-----------------------------------------");

FridgeInFile fridge_1 = new FridgeInFile("LG", "15-C");
fridge_1.TemperatureAdded += FridgeTemperatureAdded;

while (true)
{
    Console.WriteLine("Podaj temperaturę w lodówce lub zakończ (q): ");
    var input = Console.ReadLine();
    if (input == "q" || input == "Q")
    {
        break;
    }
    try
    {
        fridge_1.AddTemperature(input);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Exception catched: {ex.Message}");
    }
}
void FridgeTemperatureAdded(object sender, EventArgs args)
{
    Console.WriteLine("Dodano nowy pomiar temperatury");
}

var statistics1 = fridge_1.GetStatistics();
Console.WriteLine($"Fridge: {fridge_1.Producer} {fridge_1.Id}");
Console.WriteLine($"Correct temperature: {fridge_1.CorrectTemperature} °C");
Console.WriteLine($"Allowable temperature difference: {fridge_1.AllowableDifference} °C");
Console.WriteLine($"Min. temperature: {statistics1.Min} \x00b0C");
Console.WriteLine($"Max. temperature: {statistics1.Max} °C");
Console.WriteLine($"Average temperature: {statistics1.Average:N1} °C");
Console.WriteLine($"Number of measures: {statistics1.Count}");
Console.WriteLine($"Number of bad measures: {statistics1.CountBad}");
Console.WriteLine($"Fridge technical condition: {statistics1.TechnicalCondition}");