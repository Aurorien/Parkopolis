using Parkopolis.Interfaces;
using Parkopolis.UI.Helpers;

namespace Parkopolis.UI
{

    // ToDo: Simplify: create a method that prints out a list, review printouts and extract similar functionality into methods

    internal class Main
    {
        private IUI _ui;
        private IHandler _garageHandler;
        private IFilterVehicles _filterVehicles;
        private IShowVehicles _showVehicles;
        private IAddVehicles _addVehicles;
        private IRemoveVehicles _removeVehicles;

        public Main(IUI ui, IHandler garageHandler, IFilterVehicles filterVehicles, IShowVehicles showVehicles, IAddVehicles addVehicles, IRemoveVehicles removeVehicles)
        {
            _ui = ui;
            _garageHandler = garageHandler;
            _filterVehicles = filterVehicles;
            _showVehicles = showVehicles;
            _addVehicles = addVehicles;
            _removeVehicles = removeVehicles;
        }


        internal void Run()
        {
            UIHelper.UIHeader(_ui);
            Init();

        }

        private void Init()
        {
            _ui.WriteLine("Welcome to Parkopolis");
            _ui.WriteLine("Start with setting the capacity for your garage by entering a number and press Enter.");
            int capacity = InputValidation.GetConvertedIntInput("Capacity: ", _ui);
            _garageHandler.InitializeGarage(capacity);

            _ui.WriteLine("\nDo you want to start adding vehicles to the garage now or later? ");
            UIHelper.UIMenuWrapper(ShowInitAddMenu, _ui, "Close Program");

            bool shouldContinue = true;
            while (shouldContinue)
            {
                string input = _ui.ReadLine();

                switch (input)
                {
                    case MenuHelper.Now:
                        _addVehicles.AddVehicle();
                        shouldContinue = false;
                        break;
                    case MenuHelper.Later:
                        shouldContinue = false;
                        break;
                    case MenuHelper.Close:
                        UIHelper.CloseProgram(_ui);
                        return;
                    default:
                        InputValidation.InvalidMenuInput(_ui);
                        break;
                }
            }

            MainMenu();
        }


        public void MainMenu()
        {
            do
            {
                UIHelper.UIHeader(_ui);
                _ui.WriteLine("Main Menu\n");
                UIHelper.UIMenuWrapper(ShowMainMenu, _ui, "Close Program");

                string input = _ui.ReadLine();

                switch (input)
                {
                    case MenuHelper.ShowAll:
                        _showVehicles.ShowAllVehicles();
                        break;
                    case MenuHelper.ShowAllTypes:
                        _showVehicles.ShowAllVehicleTypes();
                        break;
                    case MenuHelper.Add:
                        if (_garageHandler.IsGarageFull)
                        {
                            InputValidation.NotAvailableInput(_ui);
                            break;
                        }
                        else
                            _addVehicles.AddVehicle();
                        break;
                    case MenuHelper.Remove:
                        if (_garageHandler.IsGarageEmpty)
                        {
                            InputValidation.NotAvailableInput(_ui);
                            break;
                        }
                        else
                            _removeVehicles.RemoveVehicle();
                        break;
                    case MenuHelper.SearchRegNum:
                        _filterVehicles.SearchVehicleRegNum();
                        break;
                    case MenuHelper.Filter:
                        _filterVehicles.FilterVehicle();
                        break;
                    case MenuHelper.Close:
                        UIHelper.CloseProgram(_ui);
                        return;
                    default:
                        InputValidation.InvalidMenuInput(_ui);
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
            _ui.WriteLine($"{MenuHelper.Filter}. Filter vehicles");
        }
    }
}
