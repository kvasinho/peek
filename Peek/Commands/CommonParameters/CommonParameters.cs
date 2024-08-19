using Cocona;

namespace Peek.Commands.CommonParameters;

public record CommonParameterCollection(
    [Option("header",['h'], Description = "Specifies whether a header exists")]
    bool Header,
    [Option("delimiter", ['d'], Description = "Specifies the delimiter")]
    char Delimiter = ','
) : ICommandParameterSet;
