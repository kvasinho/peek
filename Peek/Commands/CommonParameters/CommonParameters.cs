using System.ComponentModel;

using Spectre.Console.Cli;

namespace Peek.Commands.CommonParameters;


public abstract class CommonParameterSettings : CommandSettings
{
    [CommandArgument(0, "<filepath>")]
    [Description("Specifies the path of where to find the file ")]
    public required string FilePath { get; set; }

    [CommandOption("--delimiter|-d")]
    [Description("Specifies the delimiter of the file. Defaults to ','. ")]
    [DefaultValue(',')]
    public char Delimiter { get; set; }

    [CommandOption("--header")]
    [Description("Denotes an existing header in the file")]
    [DefaultValue(true)]
    public bool Header { get; set; }

    [CommandOption("--nrows")]
    [Description("Specifies the number of rows to read. Defaults to all, if not specified. Defaults to 5 in Head Command")]
    [DefaultValue(0)]
    public int NRows { get; set; }
}