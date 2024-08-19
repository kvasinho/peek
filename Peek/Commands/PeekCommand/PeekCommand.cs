using Cocona;
using Peek.Commands.CommonParameters;
using Peek.CSV;

namespace Peek.Commands.PeekCommand;

public class PeekCommand
{
    
    private readonly ICsvService _csvService;
    private readonly ICsvDisplayService _csvDisplayService;

    public PeekCommand(ICsvService csvService, ICsvDisplayService csvDisplayService)
    {
        _csvService = csvService;
        _csvDisplayService = csvDisplayService;
    }
    
    [Command("Head")]
    public void Head(
        CommonParameterCollection commonParameterCollection,
        [Argument(Name = "filepath")] string filepath,
        [Option("rows", ['r'])] int rows = 5,
        [Option("tail", ['t'])] bool head = true

    )
    {
        try
        {
            var df = _csvService.ReadCsvSync(filepath.Trim(), commonParameterCollection.Delimiter,
                commonParameterCollection.Header);
            var subset = head ? df.Head(rows) : df.Tail(rows);

            foreach (var row  in subset.Rows)
            {
                
                foreach (var column in row)
                {
                    Console.Write($"{column}\t");
                }
                Console.WriteLine("\n");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("SOmething went wrong");
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