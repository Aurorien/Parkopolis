using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Parkopolis;
using Parkopolis.Interfaces;
using Parkopolis.UI;

var host = Host.CreateDefaultBuilder(args)
               .ConfigureServices(services =>
               {
                   services.AddSingleton<IUI, ConsoleUI>();
                   services.AddSingleton<IHandler, GarageHandler>();
                   services.AddSingleton<Main>();

               })
               .UseConsoleLifetime()
               .Build();

host.Services.GetRequiredService<Main>().Run();
