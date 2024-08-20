using Microsoft.Extensions.DependencyInjection;
using Peek.Commands.Head;
using Peek.CSV;
using Spectre.Console.Cli;


var services = new ServiceCollection();
services.AddSingleton<ICsvProcessingService, CsvProcessingService>();
services.AddSingleton<ICsvDisplayService, CsvDisplayService>();

var registrar = new TypeRegistrar(services);



var app = new CommandApp(registrar); 

app.Configure(config =>
{
    
    config.AddCommand<HeadCommand>("head")
        .WithDescription("Displays the first rows of a dataframe")
        .WithAlias("peek");
    

    config.AddCommand<TestCommand>("test");
});

return app.Run(args);