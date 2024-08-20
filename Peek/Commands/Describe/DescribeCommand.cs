using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Peek.Commands.CommonCommands;
using Peek.CSV;
using Peek.Util;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Peek.Commands.Describe;


public sealed class DescribeCommand : Command<DescribeCommand.Settings>
{
    public sealed class Settings : CommonCommandSettings
    {
        [Description("Specifies whether to show the first or last n rows")]
        [CommandOption("-t|--tail")]
        [DefaultValue(false)]
        public bool Tail { get; init; }
    }
    
    private readonly ICsvProcessingService _csvService;
    private readonly ICsvDisplayService _csvDisplayService;

    public DescribeCommand(ICsvProcessingService csvService, ICsvDisplayService csvDisplayService)
    {
        _csvService = csvService;
        _csvDisplayService = csvDisplayService;
    }
    public override int Execute([NotNull] CommandContext context, [NotNull] Settings settings)
    {
        try
        {
            var subset = _csvService.ReadCsvSync(
                settings.FilePath.Trim(),
                settings.Delimiter,
                !settings.Header,
                settings.NRows == 0 ? 6 : Math.Min(100, settings.NRows) + 1);

            var table = new Table();


            for (var i = 0; i < subset.Rows.Count(); i++)
            {
                if (i == 0)
                {
                    subset.Rows[i].AddDataFrameRowAsSpectreTableHeader(table);
                }
                else
                {
                    subset.Rows[i].AddDataFrameRowToSpectreTable(table);
                }
            }
            
            AnsiConsole.Write(table);
            
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        return 0;
    }
}

public sealed class TestCommand : Command<TestCommand.Settings>
{
    public sealed class Settings : CommandSettings 
    {
        [Description("Specifies whether to show the first or last n rows")]
        [CommandOption("-t|--tail")]
        [DefaultValue(false)]
        public bool Tail { get; init; }
    }
    

    public override int Execute([NotNull] CommandContext context, [NotNull] Settings settings)
    {
        try
        {
            AnsiConsole.Write(new Markup("[bold yellow]Hello[/] [red]World![/]"));

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        return 0;
    }
}