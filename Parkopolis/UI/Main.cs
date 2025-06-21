using Parkopolis.Interfaces;

namespace Parkopolis.UI
{

    internal class Main
    {
        private IUI _ui;
        private GarageHandler _garageHandler;

        public Main(IUI ui)
        {
            _ui = ui;
            _garageHandler = new GarageHandler();
        }


        internal void Run()
        {
            UIHeader();
            Init();

        }

        private void Init()
        {
            _ui.WriteLine("Welcome to Parkopolis");
            _ui.WriteLine("Start with setting the capacity for your garage by entering a number and press Enter.");
            int capacity = UIHelper.GetConvertedIntInput("Capacity: ", _ui);
            _garageHandler.InitializeGarage(capacity);

            _ui.WriteLine("Do you want to start adding vehicles to the garage now or later? ");
            UIHelper.UIMenuWrapper(ShowInitAddMenu, _ui, "Close Program");

            while (true)
            {
                string input = _ui.ReadLine();

                switch (input)
                {
                    case MenuHelper.Now:
                        AddVehicle();
                        break;
                    case MenuHelper.Later:
                        MainMenu();
                        break;
                    case MenuHelper.Close:
                        UIHelper.CloseProgram(_ui);
                        return;
                    default:
                        UIHelper.InvalidMenuInput(_ui);
                        break;
                }
            }
        }


        private void MainMenu()
        {
            do
            {
                UIHeader();
                _ui.WriteLine("Main Menu\n");
                UIHelper.UIMenuWrapper(ShowMainMenu, _ui, "Close Program");

                string input = _ui.ReadLine();

                switch (input)
                {
                    case MenuHelper.ShowAll:
                        ShowAllVehicles();
                        break;
                    case MenuHelper.ShowAllTypes:
                        ShowAllVehicleTypes();
                        break;
                    case MenuHelper.Add:
                        if (_garageHandler.IsGarageFull)
                        {
                            UIHelper.NotAvailableInput(_ui);
                            break;
                        }
                        else
                            AddVehicle();
                        break;
                    case MenuHelper.Remove:
                        if (_garageHandler.IsGarageEmpty)
                        {
                            UIHelper.NotAvailableInput(_ui);
                            break;
                        }
                        else
                            RemoveVehicle();
                        break;
                    case MenuHelper.SearchRegNum:
                        SearchVehicleRegNum();
                        break;
                    case MenuHelper.Search:
                        SearchVehicle();
                        break;
                    case MenuHelper.Close:
                        UIHelper.CloseProgram(_ui);
                        return;
                    default:
                        UIHelper.InvalidMenuInput(_ui);
                        break;
                }

            }
            while (true);
        }

        private void ShowInitAddMenu()
        {
            _ui.WriteLine($"{MenuHelper.Now}. Yes, now.");
            _ui.WriteLine($"{MenuHelper.Later}. No, later.");
        }

        private void ShowMainMenu()
        {
            _ui.WriteLine($"{MenuHelper.ShowAll}. Show all vehichles");
            _ui.WriteLine($"{MenuHelper.ShowAllTypes}. Show all types of vehicles");

            if (!_garageHandler.IsGarageFull)
                _ui.WriteLine($"{MenuHelper.Add}. Add Vehicle");
            else
                _ui.WriteLineColored($"{MenuHelper.Add}. Add Vehicle (Garage is full)", "DarkGray");

            if (!_garageHandler.IsGarageEmpty)
                _ui.WriteLine($"{MenuHelper.Remove}. Remove Vehicle");
            else
                _ui.WriteLineColored($"{MenuHelper.Remove}. Remove Vehicle (No vehicles in garage)", "DarkGray");

            _ui.WriteLine($"{MenuHelper.SearchRegNum}. Search for vehicle by registration number");
            _ui.WriteLine($"{MenuHelper.Search}. Search for vehicle");
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

        private void UIHeader()
        {
            _ui.Clear();
            Console.WriteLine("--------------------------------------------------------------------------------------");
            Console.WriteLine("------------------------------------  PARKOPOLIS  ------------------------------------");
            Console.WriteLine("--------------------------------------------------------------------------------------\n\n");
        }

        public void ShowAllVehicles()
        {
            UIHeader();

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

            UIHelper.ReturnToMenu(_ui);
        }

        public void ShowAllVehicleTypes()
        {
            UIHeader();
            _ui.WriteLine("Vehicle Types in Garage:\n");

            var typeCounts = _garageHandler.GetVehicleTypeCounts();

            var allTypes = Enum.GetNames<VehicleType>();

            foreach (string vehicleType in allTypes)
            {
                int count = typeCounts.GetValueOrDefault(vehicleType, 0);
                var color = count > 0 ? "White" : "DarkGray";

                _ui.WriteLineColored($"{vehicleType}: {count}", color);
            }

            UIHelper.ReturnToMenu(_ui);
        }

        public void AddVehicle()
        {
            while (true)
            {
                UIHeader();
                _ui.WriteLine("Add vehicle to garage\n\n");

                VehicleType? nullableVehicleType = AddVehicleTypeMenu();

                if (nullableVehicleType == null)
                {
                    UIHelper.ReturnToMenu(_ui);
                    return;
                }

                VehicleType vehicleType = (VehicleType)nullableVehicleType;

                string regNum = GetRegNumAddInput();

                if (regNum == null)
                {
                    UIHelper.ReturnToMenu(_ui);
                    return;
                }

                _ui.Write("Color: ");
                string color = _ui.ReadLine();

                bool needsElectrical = UIHelper.GetBooleanInput("Needs electrical station? (y/n): ", _ui);

                string? message = CreateVehicle(vehicleType, regNum, color, needsElectrical);

                if (message == null)
                {
                    return;
                }
                else
                {
                    _ui.WriteLine(message);
                }

                if (_garageHandler.IsGarageFull)
                {
                    _ui.WriteLine("Garage is full! Not possible to add more vehicles.");
                    UIHelper.ReturnToMenu(_ui);
                    return;
                }

                bool addAnotherVehicle = UIHelper.GetBooleanInput("\nAdd another vehicle? (y/n): ", _ui);
                if (!addAnotherVehicle)
                {
                    UIHelper.ReturnToMenu(_ui);
                    MainMenu();
                    return;
                }
            }
        }

        public void RemoveVehicle()
        {
            UIHeader();
            _ui.WriteLine("\nRemove vehicle from garage\n");

            while (true)
            {
                _ui.Write("Enter registration number to remove (or '0' to cancel): ");
                string input = _ui.ReadLine().Trim();

                if (input == "0")
                {
                    UIHelper.ReturnToMenu(_ui);
                    return;
                }

                if (string.IsNullOrWhiteSpace(input))
                {
                    _ui.WriteLine("Please enter a valid registration number.");
                    continue;
                }

                if (!_garageHandler.IsRegNumExists(input))
                {
                    _ui.WriteLine($"No vehicle found with registration number '{input}'. Try again or enter '0' to cancel.");
                    continue;
                }

                bool confirmRemoval = UIHelper.GetBooleanInput($"Are you sure you want to remove vehicle '{input}'? (y/n): ", _ui);

                if (confirmRemoval)
                {
                    string result = _garageHandler.RemoveVehicle(input);
                    _ui.WriteLine(result);
                }
                else
                {
                    _ui.WriteLine("Removal cancelled.");
                }

                if (_garageHandler.IsGarageEmpty)
                {
                    _ui.WriteLine("Garage is empty! Not possible to remove any vehicles.");
                    UIHelper.ReturnToMenu(_ui);
                    return;
                }

                bool removeAnother = UIHelper.GetBooleanInput("Remove another vehicle? (y/n): ", _ui);
                if (!removeAnother)
                {
                    UIHelper.ReturnToMenu(_ui);
                    return;
                }
            }
        }

        private string GetRegNumAddInput()
        {
            while (true)
            {
                _ui.Write("Registration number: ");
                string input = _ui.ReadLine().ToLower();

                if (_garageHandler.IsRegNumExists(input))
                {
                    _ui.WriteLine("Registration number already exists! Try another one or enter 0 to return to main menu.");
                }
                else
                {
                    return input;
                }
            }
        }

        private VehicleType? AddVehicleTypeMenu()
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
                        UIHelper.InvalidMenuInput(_ui);
                        break;
                }

            }
        }

        public void SearchVehicleRegNum()
        {
            throw new NotImplementedException();
        }

        public void SearchVehicle()
        {
            throw new NotImplementedException();
        }

        private string? CreateVehicle(VehicleType type, string regNum, string color, bool needsElectrical)
        {
            switch (type)
            {
                case VehicleType.Car:
                    _ui.Write("Fuel type: ");
                    string fuelType = _ui.ReadLine();
                    string message = _garageHandler.AddVehicle(type, regNum, color, needsElectrical, fuelType);
                    return message;

                case VehicleType.Motorcycle:
                    bool wallSupport = UIHelper.GetBooleanInput("Needs wall support? (y/n): ", _ui);
                    return _garageHandler.AddVehicle(type, regNum, color, needsElectrical, wallSupport);

                case VehicleType.Truck:
                    bool hasTrailer = UIHelper.GetBooleanInput("Has trailer? (y/n): ", _ui);
                    return _garageHandler.AddVehicle(type, regNum, color, needsElectrical, hasTrailer);

                case VehicleType.Bus:
                    bool needsPlatform = UIHelper.GetBooleanInput("Needs passenger platform? (y/n): ", _ui);
                    return _garageHandler.AddVehicle(type, regNum, color, needsElectrical, needsPlatform);

                case VehicleType.Hovercraft:
                    bool needsInflation = UIHelper.GetBooleanInput("Requires inflation space? (y/n): ", _ui);
                    return _garageHandler.AddVehicle(type, regNum, color, needsElectrical, needsInflation);

                default:
                    UIHelper.InvalidMenuInput(_ui);
                    return null;
            }
        }
    }
}
