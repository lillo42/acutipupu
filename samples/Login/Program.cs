using System.Text;

//
// try
// {
//     var builder = AcutipupuApp.CreateBuilder();
//     builder.Services.UseAcutipupu<MainPage, MainPageViewModel>();
//     await builder.Build()
//         .RunAsync();
// }
// catch (Exception ex)
// {
//     Console.WriteLine(ex.Message);
// }

Console.OutputEncoding = Encoding.UTF8;

// Console.Write("\tteste-tab");
// Console.SetCursorPosition(0, 0);
// await Task.Delay(1_000);
// Console.SetCursorPosition(1, 0);
// Console.WriteLine();
// Console.Write("new line\n");

Console.WriteLine($"\\a is control: {char.IsControl('\a')}");
Console.WriteLine($"\\v is control: {char.IsControl('\v')}");
Console.WriteLine($"\\t is control: {char.IsControl('\t')}");
Console.WriteLine($"\\r is control: {char.IsControl('\r')}");
Console.WriteLine($"\\ is control: {char.IsControl('\\')}");
Console.WriteLine($"\\b is control: {char.IsControl('\b')}");
Console.WriteLine($"\\f is control: {char.IsControl('\f')}");
Console.WriteLine($"\\n is control: {char.IsControl('\n')}");

Console.WriteLine("Trying backspace\b");

Console.WriteLine("\vvertical");
Console.WriteLine("\tvertical");


Console.ReadLine();
