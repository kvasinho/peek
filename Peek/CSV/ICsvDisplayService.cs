using System.Data;
using Microsoft.Data.Analysis;

namespace Peek.CSV;

public interface ICsvDisplayService
{
    public DataFrame GetTopNRows(DataFrame dataFrame, int rows = 5, bool head = true);
}

public class CsvDisplayService: ICsvDisplayService
{
    public DataFrame GetTopNRows(DataFrame dataFrame, int rows = 5, bool head = true)
    {
        if (rows <= 0)
        {
            throw new InvalidConstraintException("Rows has to be larger than 0 ");
        }

        return head ? dataFrame.Head(rows) : dataFrame.Tail(rows);

    }
}