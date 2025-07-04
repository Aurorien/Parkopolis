using Parkopolis.Enum;
using Parkopolis.Interfaces;
using Parkopolis.UI.Helpers;


namespace Parkopolis.UI.Operations.FilterVehicle
{
    public class FilterVehicles : IFilterVehicles
    {
        private readonly IUI _ui;
        private readonly IHandler _garageHandler;

        public FilterVehicles(IUI ui, IHandler garageHandler)
        {
            _ui = ui;
            _garageHandler = garageHandler;
        }


        public void FilterVehicle()
        {
            UIHelper.UIHeader(_ui);
            _ui.WriteLine("Filter for vehicles\n");

            if (_garageHandler.VehicleCount < 1)
            {
                _ui.WriteLine("No vehicles in the garage to Filter.");
                UIHelper.ReturnToMainMenuInfo(_ui);
                return;
            }

            var filterCriteria = GetFilterCriteriaFromUser();

            if (filterCriteria == null) // Will be null if user made input 0 to cancel
            {
                UIHelper.ReturnToMainMenuInfo(_ui);
                return;
            }

            DisplayFilterSearchResults(filterCriteria);
        }

        public void SearchVehicleRegNum()
        {
            UIHelper.UIHeader(_ui);
            _ui.WriteLine("Search for vehicle by registration number\n");

            if (_garageHandler.VehicleCount < 1)
            {
                _ui.WriteLine("No vehicles in the garage to search.");
                UIHelper.ReturnToMainMenuInfo(_ui);
                return;
            }

            string regNum = InputValidation.GetStringInput("Enter registration number to search for: ", _ui).Trim();

            var FilterCriteria = new FilterCriteria { RegNum = regNum };
            DisplayFilterSearchResults(FilterSearchType.Search, FilterCriteria, $"Registration Number: {regNum}");
        }

        private FilterCriteria? GetFilterCriteriaFromUser()
        {
            var criteria = new FilterCriteria();

            var (scope, vehicleType) = GetVehicleTypeChoice();

            if (scope == FilterScope.Cancelled)
                return null;

            criteria.Scope = scope;
            criteria.SpecificType = vehicleType;

            GetBasicFilterCriteria(criteria);

            return criteria;
        }

        private (FilterScope scope, VehicleType? vehicleType) GetVehicleTypeChoice()
        {
            while (true)
            {
                _ui.WriteLine("Filter by vehicle (press Enter to skip and include all vehicles):");
                _ui.WriteLine($"{MenuHelper.Car}. Car");
                _ui.WriteLine($"{MenuHelper.Motorcycle}. Motorcycle");
                _ui.WriteLine($"{MenuHelper.Truck}. Truck");
                _ui.WriteLine($"{MenuHelper.Bus}. Bus");
                _ui.WriteLine($"{MenuHelper.Hovercraft}. Hovercraft");
                _ui.WriteLine($"{MenuHelper.Close}. Cancel");
                _ui.Write("\nMenu choice: ");

                string input = _ui.ReadLine().Trim();

                if (string.IsNullOrEmpty(input))
                {
                    return (FilterScope.AllVehicles, null);
                }

                switch (input)
                {
                    case MenuHelper.Car:
                        return (FilterScope.SpecificType, VehicleType.Car);
                    case MenuHelper.Motorcycle:
                        return (FilterScope.SpecificType, VehicleType.Motorcycle);
                    case MenuHelper.Truck:
                        return (FilterScope.SpecificType, VehicleType.Truck);
                    case MenuHelper.Bus:
                        return (FilterScope.SpecificType, VehicleType.Bus);
                    case MenuHelper.Hovercraft:
                        return (FilterScope.SpecificType, VehicleType.Hovercraft);
                    case MenuHelper.Close:
                        return (FilterScope.Cancelled, null);
                    default:
                        InputValidation.InvalidMenuInput(_ui);
                        break;
                }
            }
        }

        private void GetBasicFilterCriteria(FilterCriteria criteria)
        {
            _ui.WriteLine("\nFilter by Color (press Enter to skip):");
            _ui.Write("Color: ");
            string color = _ui.ReadLine().Trim();
            if (!string.IsNullOrEmpty(color))
                criteria.Color = color;

            _ui.WriteLine("\nFilter by electrical station requirement (press Enter to skip):");
            _ui.WriteLine("y = Needs electrical station");
            _ui.WriteLine("n = Doesn't need electrical station");
            _ui.Write("Choice: ");
            string electrical = _ui.ReadLine().Trim().ToLower();

            if (electrical == "y")
                criteria.NeedsElectricalStation = true;
            else if (electrical == "n")
                criteria.NeedsElectricalStation = false;
        }

        private void DisplayFilterSearchResults(FilterCriteria criteria)
        {
            var results = _garageHandler.FilterVehicles(criteria);

            UIHelper.UIHeader(_ui);
            _ui.WriteLine($"Filter Results:\n\n");

            if (results.Count == 0)
            {
                _ui.WriteLine("No vehicles found matching the Filter criteria.");
            }
            else
            {
                _ui.WriteLine($"Found {results.Count} vehicle{(results.Count == 1 ? "" : "s")}:\n");
                foreach (var vehicle in results)
                {
                    _ui.WriteLine(vehicle);
                }
            }

            UIHelper.ReturnToMainMenuInfo(_ui);
        }


        private void DisplayFilterSearchResults(FilterSearchType filterOrSearch, FilterCriteria criteria, string FilterDescription)
        {
            var results = _garageHandler.FilterVehicles(criteria);

            UIHelper.UIHeader(_ui);
            _ui.WriteLine($"{filterOrSearch} results for {FilterDescription}:\n");

            if (results.Count == 0)
            {
                _ui.WriteLine($"No vehicles found matching the {filterOrSearch.ToString().ToLower()} criteria.");
            }
            else
            {
                _ui.WriteLine($"Found {results.Count} vehicle{(results.Count == 1 ? "" : "s")}:\n");
                foreach (var vehicle in results)
                {
                    _ui.WriteLine(vehicle);
                }
            }

            UIHelper.ReturnToMainMenuInfo(_ui);
        }

    }
}
