using Fclp;
using KnapsackProblem.InputGenerator;
using KnapsackProblem.InputGenerator.ConsoleApp;
using System.Globalization;

var cliParser = new FluentCommandLineParser<InputGeneratorArguments>();

cliParser
    .SetupHelp("?", "help", "h")
    .Callback(text => Console.WriteLine(text));

cliParser
    .Setup(arg => arg.ItemsCount)
    .As('i', "itemsCount")
    .Required();

cliParser
    .Setup(arg => arg.MinimumWeight)
    .As("minimumWeight")
    .SetDefault(0.2);

cliParser
    .Setup(arg => arg.MaximumWeight)
    .As("maximumWeight")
    .SetDefault(10.0);

cliParser
    .Setup(arg => arg.MinimumValue)
    .As("minimumValue")
    .SetDefault(1.0);

cliParser
    .Setup(arg => arg.MaximumValue)
    .As("maximumValue")
    .SetDefault(20.0);

var parsingResult = cliParser.Parse(Environment.GetCommandLineArgs());

if (parsingResult.HasErrors == false)
{
    var parameters = new InputGeneratorParameters
    {
        MinimumValue = cliParser.Object.MinimumValue,
        MaximumValue = cliParser.Object.MaximumValue,
        MinimumWeight = cliParser.Object.MinimumWeight,
        MaximumWeight = cliParser.Object.MaximumWeight
    };

    var generator = new KnapsackItemGenerator(parameters);
    var result = generator.Generate(cliParser.Object.ItemsCount);
    var culture = CultureInfo.InvariantCulture;

    foreach (var item in result)
    {
        Console.WriteLine($"{item.Name},{item.Weight.ToString(culture)},{item.Value.ToString(culture)}");
    }
}