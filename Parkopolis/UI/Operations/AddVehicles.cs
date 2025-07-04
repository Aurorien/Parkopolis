using Parkopolis.Interfaces;
using Parkopolis.UI.Helpers;

namespace Parkopolis.UI.Operations
{
    internal class AddVehicles : IAddVehicles
    {
        private readonly IUI _ui;
        private readonly IHandler _garageHandler;

        public AddVehicles(IUI ui, IHandler garageHandler)
        {
            _ui = ui;
            _garageHandler = garageHandler;
        }

        public void AddVehicle()
        {
            bool shouldContinue = true;
            while (shouldContinue)
            {
                UIHelper.UIHeader(_ui);
                _ui.WriteLine("Add vehicle to garage\n");

                VehicleType? nullableVehicleType = VehicleTypeMenu();

                if (nullableVehicleType == null) // Will be null if user made input 0 to cancel
                {
                    UIHelper.ReturnToMainMenuInfo(_ui);
                    return;
                }

                VehicleType vehicleType = (VehicleType)nullableVehicleType;

                string regNum = InputValidation.GetAddRegNumInput(_ui, _garageHandler);

                string color = InputValidation.GetStringInput("Color: ", _ui);

                bool needsElectrical = InputValidation.GetBooleanInput("Needs electrical station? (y/n): ", _ui);

                string? message = CreateVehicle(vehicleType, regNum, color, needsElectrical);

                if (message == null)
                {
                    return;
                }
                else
                {
                    _ui.WriteLine($"\n{message}\n");
                }

                if (_garageHandler.IsGarageFull)
                {
                    _ui.WriteLine("Garage is full! Not possible to add more vehicles.");
                    UIHelper.ReturnToMainMenuInfo(_ui);
                    shouldContinue = false;
                    return;
                }

                bool addAnotherVehicle = InputValidation.GetBooleanInput("\nAdd another vehicle? (y/n): ", _ui);
                if (!addAnotherVehicle)
                {
                    UIHelper.ReturnToMainMenuInfo(_ui);
                    shouldContinue = false;
                    return;
                }
            }
        }

        private void ShowVehicleTypeMenu()
        {
            _ui.WriteLine("Select vehicle type:");
            _ui.WriteLine($"{MenuHelper.Car}. Car");
            _ui.WriteLine($"{MenuHelper.Motorcycle}. Motorcycle");
            _ui.WriteLine($"{MenuHelper.Truck}. Truck");
            _ui.WriteLine($"{MenuHelper.Bus}. Bus");
            _ui.WriteLine($"{MenuHelper.Hovercraft}. Hovercraft");
        }

        private VehicleType? VehicleTypeMenu()
        {
            while (true)
            {
                UIHelper.UIMenuWrapper(ShowVehicleTypeMenu, _ui, "Cancel");

                string input = _ui.ReadLine();

                if (input == "0")
                {
                    return null; // Cancels to main menu
                }

                switch (input)
                {
                    case MenuHelper.Car:
                        return VehicleType.Car;
                    case MenuHelper.Motorcycle:
                        return VehicleType.Motorcycle;
                    case MenuHelper.Truck:
                        return VehicleType.Truck;
                    case MenuHelper.Bus:
                        return VehicleType.Bus;
                    case MenuHelper.Hovercraft:
                        return VehicleType.Hovercraft;
                    default:
                        InputValidation.InvalidMenuInput(_ui);
                        break;
                }

            }
        }


        private string? CreateVehicle(VehicleType type, string regNum, string color, bool needsElectrical)
        {
            switch (type)
            {
                // ToDo: Extract so that there is only one line in each case ?

                case VehicleType.Car:
                    string fuelType = InputValidation.GetStringInput("Fuel type: ", _ui);
                    return _garageHandler.AddVehicle(type, regNum, color, needsElectrical, fuelType);

                case VehicleType.Motorcycle:
                    bool wallSupport = InputValidation.GetBooleanInput("Needs wall support? (y/n): ", _ui);
                    return _garageHandler.AddVehicle(type, regNum, color, needsElectrical, wallSupport);

                case VehicleType.Truck:
                    bool hasTrailer = InputValidation.GetBooleanInput("Has trailer? (y/n): ", _ui);
                    return _garageHandler.AddVehicle(type, regNum, color, needsElectrical, hasTrailer);

                case VehicleType.Bus:
                    bool needsPlatform = InputValidation.GetBooleanInput("Needs passenger platform? (y/n): ", _ui);
                    return _garageHandler.AddVehicle(type, regNum, color, needsElectrical, needsPlatform);

                case VehicleType.Hovercraft:
                    bool needsInflation = InputValidation.GetBooleanInput("Requires inflation space? (y/n): ", _ui);
                    return _garageHandler.AddVehicle(type, regNum, color, needsElectrical, needsInflation);

                default:
                    InputValidation.InvalidMenuInput(_ui);
                    return null;
            }
        }
    }
}
