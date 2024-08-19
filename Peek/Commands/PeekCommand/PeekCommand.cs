using Cocona;
using Peek.Commands.CommonParameters;
using Peek.CSV;
using Peek.Util;
using Spectre.Console;

namespace Peek.Commands.PeekCommand;

public class PeekCommand
{
    
    private readonly ICsvProcessingService _csvService;
    private readonly ICsvDisplayService _csvDisplayService;

    public PeekCommand(ICsvProcessingService csvService, ICsvDisplayService csvDisplayService)
    {
        _csvService = csvService;
        _csvDisplayService = csvDisplayService;
    }
    
    [Command("Head")]
    public void Head(
        CommonParameterCollection commonParameterCollection,
        [Argument(Name = "filepath")] string filepath,
        //[Option("topn")] Int32 topn = 5,
        [Option("tail", ['t'])] bool head = true
    )
    {

        try
        {
            var subset = _csvService.ReadCsvSync(
                filepath.Trim(),
                commonParameterCollection.Delimiter,
                commonParameterCollection.Header,
                commonParameterCollection.NRows == 0 ? 6 : Math.Min(100, commonParameterCollection.NRows) + 1);

            //AnsiConsole.Write(new Markup("[bold yellow]Hello[/] [red]World![/]\n"));

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
    }
    
    [Command("peek")]
    public void Peek(
        [Argument(Name = "filepath")] string filepath,
        [Option("rows", ['r'])] int rows = 5,
        [Option("separator", ['s'])] char separator = ',',
        [Option("header", ['h'])] bool header = true
        )
    {
        var df = _csvService.ReadCsvSync(filepath.Trim(), separator, header);
        
    }
    
    
}