using Microsoft.Data.Analysis;
using Spectre.Console;

namespace Peek.Util;

public static class DataFrameExtensions
{
    public static void AddDataFrameRowToSpectreTable(this DataFrameRow row, Table table)
    {
        var formattedRow = new string[row.Count()];
        for (int i = 0; i < row.Count(); i++)
        {
            formattedRow[i] = row[i].ToString()!;
        }

        table.AddRow(formattedRow);
    }
    public static void AddDataFrameRowAsSpectreTableHeader(this DataFrameRow row, Table table)
    {
        for (int i = 0; i < row.Count(); i++)
        {
            table.AddColumn(new TableColumn($"[bold blue]{row[i].ToString()!.ToUpper()}[/]").Centered());
        }                
    }
    
    public static void GetMetaData(this DataFrameColumn column, Table table)
    {
        var nullCount = column.NullCount;
        var uniqueVals = column.ValueCounts();
        
    }
}