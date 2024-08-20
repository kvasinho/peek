using Spectre.Console;

namespace Peek.Util;

public static class SpectreUtils
{
    public static void AddTableHeader(this Table table, ICollection<string> colnames)
    {
        foreach (var colname in colnames)
        {
            table.AddColumn(new TableColumn($"[bold blue]{colname.ToUpper()}[/]").Centered());
        }
    }
    
}