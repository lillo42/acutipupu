using Acutipupu;
using Acutipupu.Extensions;
using CreditCardForm;

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
