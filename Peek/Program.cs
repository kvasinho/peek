using Microsoft.Extensions.DependencyInjection;
using Peek.Commands.Convert;
using Peek.Commands.Dimensions;
using Peek.Commands.Head;
using Peek.CSV;
using Peek.Services;
using Spectre.Console.Cli;
using Peek.TypeRegistrar;


var services = new ServiceCollection();
services.AddSingleton<ICsvProcessingService, CsvProcessingService>();
services.AddSingleton<ICsvDisplayService, CsvDisplayService>();
services.AddSingleton<ITableGeneratorService, TableGeneratorService>();

var registrar = new TypeRegistrar(services);



var app = new CommandApp(registrar); 

app.Configure(config =>
{
    
    config.AddCommand<HeadCommand>("head")
        .WithDescription("Displays the first rows of a dataframe");

    config.AddCommand<DimensionsCommand>("dimensions")
        .WithDescription("Shows number of rows, columns, as well as file size.");

    config.AddCommand<ConvertCommand>("convert")
        .WithDescription("Converts a csv to json");
});

return app.Run(args);