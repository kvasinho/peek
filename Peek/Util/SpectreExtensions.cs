using System.Collections;
using Microsoft.Data.Analysis;
using Spectre.Console;

namespace Peek.Util;

public static class SpectreTableExtensions
{
    /// <summary>Extension method for the Table class: Adds an ICollection of Strings  as header to the table.</summary>
    /// <param name="table">The table to add the header to.</param>
    /// <param name="colNames">The Collection of primitives that should be added as header</param>
    public static void AddTableHeader(this Table table, IList<string> colNames)
    {
        foreach (var colName in colNames)
        {
            if (colName != null) 
            {
                table.AddColumn(new TableColumn($"[bold blue]{colName.ToUpper()}[/]").Centered());
            }
            else
            {
                throw new ArgumentException("Can only use strings as table headers");
            }
        }
    }
    /// <summary>Extension method for the Table class: Adds a dataframe row as header to the table.</summary>
    /// <param name="table">The table to add the header to.</param>
    /// <param name="row">The dataframe row that should be added as header</param>
    public static void AddDataFrameHeader(this Table table,  DataFrameRow row)
    {
        for (int i = 0; i < row.Count(); i++)
        {
            table.AddColumn(new TableColumn($"[bold blue]{row[i].ToString()!.ToUpper()}[/]").Centered());
        }                
    }
    
    /// <summary>Extension method for the Table class: Adds a dataframe row  to the table.</summary>
    /// <param name="table">The table to add the row to.</param>
    /// <param name="row">The dataframe row that should be added.</param>
    public static void AddDataFrameRow(this Table table, DataFrameRow row)
    {
        var formattedRow = new string[row.Count()];
        for (int i = 0; i < row.Count(); i++)
        {
            formattedRow[i] = row[i].ToString()!;
        }

        table.AddRow(formattedRow);
    }
    
}