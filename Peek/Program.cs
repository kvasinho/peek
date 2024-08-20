using Microsoft.Extensions.DependencyInjection;
using Peek.Commands.Describe;
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

        config.AddCommand<DescribeCommand>("describe")
            .WithDescription("Shows descriptive stats for csv columns");
       
});

return app.Run(args);