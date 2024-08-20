using System.Text.Json;
using Microsoft.Data.Analysis;
using Spectre.Console;

namespace Peek.Util;

public static class DataFrameExtensions
{
    /// <summary>Extension method for the DataFrame class: Adds a dataframe row as header to the table.</summary>
    /// <param name="table">The table to add the header to.</param>
    /// <param name="row">The dataframe row that should be added as header</param>
    public static void AddTableHeader(this  DataFrameRow row, Table table)
    {
        for (int i = 0; i < row.Count(); i++)
        {
            table.AddColumn(new TableColumn($"[bold blue]{row[i].ToString()!.ToUpper()}[/]").Centered());
        }                
    }
    
    /// <summary>Extension method for the Table class: Adds a dataframe row  to the table.</summary>
    /// <param name="table">The table to add the row to.</param>
    /// <param name="row">The dataframe row that should be added.</param>
    public static void AddTableRow(this DataFrameRow row, Table table)
    {
        var formattedRow = new string[row.Count()];
        for (int i = 0; i < row.Count(); i++)
        {
            formattedRow[i] = row[i].ToString()!;
        }
        table.AddRow(formattedRow);
    }
    /// <summary>Extension method for the Table class: Creates a Table from the existing Dataframe</summary>
    /// <param name="dataFrame">The dataFrame to convert</param>
    /// <param name="header">Whether the dataFrame contains a header.</param>
    public static Table ToSpectreTable(this DataFrame dataFrame, bool header = true)
    {
        var table = new Table();
        
        for (var i = 0; i < dataFrame.Rows.Count(); i++)
        {
            if (i == 0 && header)
            {
                dataFrame.Rows[i].AddTableHeader(table);
            }
            else
            {
                dataFrame.Rows[i].AddTableRow(table);
            }
        }

        return table;
    }
    /// <summary>
    /// Extension method for the DataFrameRow class: Converts the DataFrameRow to Dictionary.
    /// </summary>
    /// <param name="row">The Row that should be converted.</param>
    /// <param name="colNames">The list of column names. Has to have the same length as row</param>
    public static Dictionary<string, object> ToDictionary(this DataFrameRow row, IList<string> colNames)
    {
        if (colNames.Count != row.Count())
        {
            throw new ArgumentException("ColNames & Row Entries have to be of equal length.");
        }
        var dict = new Dictionary<string, object>();
        for (int i = 0; i < row.Count(); i++)
        {
            dict[colNames[i]] = row[i];
        }

        return dict;
    }

    /// <summary>
    /// Extension method for the DataFrame class: Converts the DataFrame to a JSON array of objects. One object per row.
    /// </summary>
    /// <param name="dataFrame">The DataFrame that should be converted.</param>
    /// <param name="header">If true, the first row is assumed to be a header and is skipped in the JSON output.</param>
    public static string ToJsonRowWise(this DataFrame dataFrame, bool header = true)
    {
        var rows = new List<Dictionary<string, object>>();
        var colNames = dataFrame.GetColumnNames();

        for (int i = 0; i < dataFrame.Rows.Count(); i++)
        {
            if (i > 0 || !header)
            {
                rows.Add(dataFrame.Rows[i].ToDictionary(colNames));
            }
        }

        string json = JsonSerializer.Serialize(rows, new JsonSerializerOptions { WriteIndented = true });

        return json;
    }
    
    
    public static void GetMetaData(this DataFrameColumn column, Table table)
    {
        //var nullCount = column.NullCount;
       // var uniqueVals = column.ValueCounts();
    }

    public static List<string> GetColumnNames(this DataFrame dataFrame)
    {
        return dataFrame.Columns.Select(col => col.Name.ToString()).ToList();
    }

    public static Dimension GetDimensions(this DataFrame dataFrame)
    {
        return new Dimension()
        {
            Rows = dataFrame.Rows.Count(),
            Columns = dataFrame.Columns.Count()
        };
    }

    public static void DescribeStringColumn(this DataFrameColumn column)
    {
        
    }
}

public struct Dimension
{
    public int Rows { get; set; }
    public int Columns { get; set; }
}


