using Parkopolis.Interfaces;
using Parkopolis.UI.Helpers;

namespace Parkopolis.UI.Operations
{
    public class ShowVehicles : IShowVehicles
    {
        private readonly IUI _ui;
        private readonly IHandler _garageHandler;

        public ShowVehicles(IUI ui, IHandler garageHandler)
        {
            _ui = ui;
            _garageHandler = garageHandler;
        }


        public void ShowAllVehicles()
        {
            UIHelper.UIHeader(_ui);

            if (_garageHandler.VehicleCount < 1)
            {
                _ui.WriteLine("No vehicles in the garage.");
            }
            else
            {
                int count = _garageHandler.VehicleCount;
                _ui.WriteLine($"\nAll Vehicles in Garage ({count} {(count == 1 ? "vehicle" : "vehicles")}):\n");
                var vehicles = _garageHandler.GetAllVehicles();

                foreach (var vehicle in vehicles)
                {
                    _ui.WriteLine(vehicle);
                }
            }

            UIHelper.ReturnToMainMenuInfo(_ui);
        }

        public void ShowAllVehicleTypes()
        {
            UIHelper.UIHeader(_ui);
            _ui.WriteLine("Vehicle Types in Garage:\n");

            var typeCounts = _garageHandler.GetVehicleTypeCounts();

            var allTypes = System.Enum.GetNames<VehicleType>();

            foreach (string vehicleType in allTypes)
            {
                int count = typeCounts.GetValueOrDefault(vehicleType, 0);
                var color = count > 0 ? "White" : "DarkGray";

                _ui.WriteLineColored($"{vehicleType}: {count}", color);
            }

            UIHelper.ReturnToMainMenuInfo(_ui);
        }
    }
}
