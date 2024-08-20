using Microsoft.Data.Analysis;
using Peek.Util;
using Spectre.Console;

namespace Peek.Services;

public interface ITableGeneratorService
{
    public Table CreateDimensionsTable(string fileName, string fileSize, Dimension dimension);
    public Table CreateTableFromDataFrame(DataFrame dataFrame, bool header);
}

public class TableGeneratorService : ITableGeneratorService
{
    /// <summary>Creates a table that contains the dimensions of the csv (Rows, Columns, Filesize). Receives all inputs and returns a table</summary>
    /// <param name="fileName">The name of the file.</param>
    /// <param name="fileSize">The size of the file as string.</param>
    /// <param name="dimension">The row and column count as Dimension struct..</param>

    public Table CreateDimensionsTable(string fileName, string fileSize, Dimension dimension)
    {
        var table = new Table();
        table.AddTableHeader(["Name", "Rows", "Columns", "Size"]);

        table.AddRow([fileName, dimension.Rows.ToString(), dimension.Columns.ToString(), fileSize]);

        return table;
    }

    /// <summary>Generates a table from any dataframe.</summary>
    /// <param name="dataFrame">The dataframe that should be converted.</param>
    /// <param name="header">Whether the dataframe contains a header.</param>
    public Table CreateTableFromDataFrame(DataFrame dataFrame, bool header = true)
    {
            return dataFrame.ToSpectreTable(header);
    }
}