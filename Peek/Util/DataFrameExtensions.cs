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
        //var nullCount = column.NullCount;
       // var uniqueVals = column.ValueCounts();
    }

    public static ICollection<string> GetColumnNames(this DataFrame dataFrame)
    {
        return dataFrame.Columns.Select(col => col.Name).ToList();
    }

    public static Dimension GetDimensions(this DataFrame dataFrame)
    {
        return new Dimension()
        {
            rows = dataFrame.Rows.Count(),
            columns = dataFrame.Columns.Count()
        };
    }
}

public struct Dimension
{
    public int rows { get; set; }
    public int columns { get; set; }
}