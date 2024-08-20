using System.ComponentModel;
using Peek.Commands.CommonParameters;
using Peek.CSV;
using Peek.Util;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Peek.Commands.Head;


public sealed class HeadCommand : Command<HeadCommand.Settings>
{
    public sealed class Settings : CommonParameterSettings // Inherit from CommonCommandSettings
    {
        [Description("Specifies whether to show the first or last n rows")]
        [CommandOption("-t|--tail")]
        [DefaultValue(false)]
        public bool Tail { get; init; }
    }
    
    private readonly ICsvProcessingService _csvService;
    private readonly ICsvDisplayService _csvDisplayService;

    public HeadCommand(ICsvProcessingService csvService, ICsvDisplayService csvDisplayService)
    {
        _csvService = csvService;
        _csvDisplayService = csvDisplayService;
    }
    public override int Execute(CommandContext context, Settings settings)
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
                    table.AddDataFrameHeader(subset.Rows[i]);
                }
                else
                {
                    table.AddDataFrameRow(subset.Rows[i]);
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
