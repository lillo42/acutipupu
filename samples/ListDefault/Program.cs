using Acutipupu;
using Acutipupu.Extensions;
using ListDefault;

try
{
    var builder = AcutipupuApp.CreateBuilder();
    builder.Services.UseAcutipupu<MainPage, MainPageViewModel>();
    await builder.Build()
        .RunAsync();
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}
