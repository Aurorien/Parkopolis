using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Parkopolis;
using Parkopolis.Interfaces;
using Parkopolis.UI;
using Parkopolis.UI.Operations;
using Parkopolis.UI.Operations.FilterVehicle;

var host = Host.CreateDefaultBuilder(args)
               .ConfigureServices(services =>
               {
                   services.AddSingleton<IUI, ConsoleUI>();
                   services.AddSingleton<IHandler, GarageHandler>();
                   services.AddSingleton<IFilterVehicles, FilterVehicles>();
                   services.AddSingleton<IShowVehicles, ShowVehicles>();
                   services.AddSingleton<IAddVehicles, AddVehicles>();
                   services.AddSingleton<IRemoveVehicles, RemoveVehicles>();
                   services.AddSingleton<Main>();

               })
               .UseConsoleLifetime()
               .Build();

host.Services.GetRequiredService<Main>().Run();
