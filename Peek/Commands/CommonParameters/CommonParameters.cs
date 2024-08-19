using Cocona;

namespace Peek.Commands.CommonParameters;

public record CommonParameterCollection(
    [Option("header",['h'], Description = "Specifies whether a header exists")]
    bool Header,
    [Option("nrows", ['n'], Description = "Specifies the number of rows to read. Reads all rows if none specified")]
    Int32 NRows = 0,
    [Option("delimiter", ['d'], Description = "Specifies the delimiter")]
    char Delimiter = ','
) : ICommandParameterSet;
