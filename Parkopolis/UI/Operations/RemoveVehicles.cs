using Parkopolis.Interfaces;
using Parkopolis.UI.Helpers;

namespace Parkopolis.UI.Operations
{
    internal class RemoveVehicles : IRemoveVehicles
    {
        private readonly IUI _ui;
        private readonly IHandler _garageHandler;

        public RemoveVehicles(IUI ui, IHandler garageHandler)
        {
            _ui = ui;
            _garageHandler = garageHandler;
        }

        public void RemoveVehicle()
        {
            UIHelper.UIHeader(_ui);
            _ui.WriteLine("\nRemove vehicle from garage\n");

            while (true)
            {
                _ui.Write("Enter registration number to remove (or '0' to cancel): ");
                string input = _ui.ReadLine().Trim();

                if (input == "0")
                {
                    UIHelper.ReturnToMainMenuInfo(_ui);
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

                bool confirmRemoval = InputValidation.GetBooleanInput($"Are you sure you want to remove vehicle '{input}'? (y/n): ", _ui);

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
                    UIHelper.ReturnToMainMenuInfo(_ui);
                    return;
                }

                bool removeAnother = InputValidation.GetBooleanInput("\nRemove another vehicle? (y/n): ", _ui);
                if (!removeAnother)
                {
                    UIHelper.ReturnToMainMenuInfo(_ui);
                    return;
                }
            }
        }
    }
}
