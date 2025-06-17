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
            UIHelper.UIMenuWrapper(ShowInitAddMenu, _ui);

            while (true)
            {
                string input = _ui.ReadLine();

                switch (input)
                {
                    case MenuHelper.Now:
                        _garageHandler.AddVehicle();
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
                _ui.Clear();

                UIHeader();
                _ui.WriteLine("Main Menu\n");
                UIHelper.UIMenuWrapper(ShowMainMenu, _ui);

                string input = _ui.ReadLine();

                switch (input)
                {
                    case MenuHelper.ShowAll:
                        _garageHandler.ShowAllVehicles();
                        break;
                    case MenuHelper.ShowAllTypes:
                        _garageHandler.ShowAllVehicleTypes();
                        break;
                    case MenuHelper.Add:
                        _garageHandler.AddVehicle();
                        break;
                    case MenuHelper.Remove:
                        _garageHandler.RemoveVehicle();
                        break;
                    case MenuHelper.SearchRegNum:
                        _garageHandler.SearchVehicleRegNum();
                        break;
                    case MenuHelper.Search:
                        _garageHandler.SearchVehicle();
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
            _ui.WriteLine($"{MenuHelper.Add}. Add vehicle");

            if (_garageHandler.VehicleCount > 0)
                _ui.WriteLine("2. Remove Vehicle");
            else
                _ui.WriteLine("2. Remove Vehicle (No vehicles in garage)");

            _ui.WriteLine($"{MenuHelper.SearchRegNum}. Search for vehicle by registration number");
            _ui.WriteLine($"{MenuHelper.Search}. Search for vehicle");
        }

        private void UIHeader()
        {
            Console.WriteLine("--------------------------------------------------------------------------------------");
            Console.WriteLine("------------------------------------  PARKOPOLIS  ------------------------------------");
            Console.WriteLine("--------------------------------------------------------------------------------------");
        }
    }
}
