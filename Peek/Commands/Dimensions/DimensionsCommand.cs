using System.ComponentModel;
using Microsoft.Data.Analysis;
using Peek.Commands.CommonParameters;
using Peek.Services;
using Peek.Util;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Peek.Commands.Dimensions;


public sealed class DimensionsCommand : Command<DimensionsCommand.Settings>
//Commands are just the formatters and error checks. 
//Tables are created in the TableGeneratorService
//Utils and Extensions are used to generate the numbers
{
    public sealed class Settings : CommonParameterSettings
    {
        [Description("Specifies whether to show the first or last n rows")]
        [CommandOption("-t|--tail")]
        [DefaultValue(false)]
        public bool Tail { get; init; }
    }
    
    private readonly ITableGeneratorService _tableGeneratorService;

    public DimensionsCommand(ITableGeneratorService tableGeneratorService)
    {
        _tableGeneratorService = tableGeneratorService;
    }
    
    public override int Execute(CommandContext context, Settings settings)
    {
        Console.WriteLine("test");
        try
        {

            var df = DataFrame.LoadCsv(
                settings.FilePath,
                settings.Delimiter,
                settings.Header
                )!;


            var table = _tableGeneratorService.CreateDimensionsTable(
                settings.FilePath.GetFileName(),
                FileUtils.GetFileSize(settings.FilePath), 
                df.GetDimensions()
                );
            

            AnsiConsole.Write(table);
            Console.WriteLine(df.Info());

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        return 0;
    }
}
