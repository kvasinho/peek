using Peek.Util;
using Spectre.Console;

namespace Peek.Services;

public interface ITableGeneratorService
{
    public Table CreateDimensionsTable(string fileName, string fileSize, Dimension dimension);

}

public class TableGeneratorService : ITableGeneratorService
{
    public Table CreateDimensionsTable(string fileName, string fileSize, Dimension dimension)
    {
        var table = new Table();
        table.AddTableHeader(["Name", "Rows", "Columns", "Size"]);
        table.AddRow([fileName, dimension.rows.ToString(), dimension.columns.ToString(), fileSize]);

        return table;
    }
}