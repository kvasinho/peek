using System.Text.Json;
using Cocona;
using Microsoft.Extensions.DependencyInjection;
using Peek;
using Peek.Commands.PeekCommand;
using Peek.CSV;

var builder = CoconaApp.CreateBuilder();

//DI CONTAINERS
builder.Services.AddSingleton<ICsvProcessingService, CsvProcessingService>();
builder.Services.AddSingleton<ICsvDisplayService, CsvDisplayService>();

var app = builder.Build();
app.AddCommands<PeekCommand>();


app.Run();