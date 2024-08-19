using Microsoft.Data.Analysis;
namespace Peek.CSV;

public interface ICsvService
{
    public DataFrame ReadCsvSync(string path, char separator, bool header = true);
}

public class CsvService : ICsvService
{
    public  DataFrame ReadCsvSync(string path, char separator, bool header = true)
    {
        if (!File.Exists(path))
        {
            throw new FileNotFoundException("File could not be found");
        }

        return DataFrame.LoadCsv(path, separator, header)!;
    }
}