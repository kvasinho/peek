using Microsoft.Data.Analysis;
namespace Peek.CSV;

public interface ICsvProcessingService
{
    public DataFrame ReadCsvSync(string path, char separator, bool header = true, Int32 nRows = 0);
}

public class CsvProcessingService : ICsvProcessingService
{
    public  DataFrame ReadCsvSync(string path, char separator, bool header = true, Int32  nRows = -1)
    {
        if (!File.Exists(path))
        {
            throw new FileNotFoundException("File could not be found");
        }
        return DataFrame.LoadCsv(path, separator, header, default, default, nRows)!;
    }
}