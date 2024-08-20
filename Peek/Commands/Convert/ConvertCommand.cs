using System.ComponentModel;
using Peek.Commands.CommonParameters;
using Peek.CSV;
using Peek.Util;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Peek.Commands.Convert;


public sealed class ConvertCommand : Command<ConvertCommand.Settings>
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

    public ConvertCommand(ICsvProcessingService csvService, ICsvDisplayService csvDisplayService)
    {
        _csvService = csvService;
        _csvDisplayService = csvDisplayService;
    }
    public override int Execute(CommandContext context, Settings settings)
    {
        try
        {
            var df = _csvService.ReadCsvSync(
                settings.FilePath.Trim(),
                settings.Delimiter,
                settings.Header,
                settings.NRows == 0 ? 6 : Math.Min(100, settings.NRows) + 1);

            var json = df.ToJsonRowWise();


            if (settings.Save)
            {
                var pathState = FileUtils.ValidateFilePath(settings.SavePath);
                
                
            }
            
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        return 0;
    }
}