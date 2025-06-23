using Parkopolis.Interfaces;
using Parkopolis.UI;
using Parkopolis.Vehicles;

namespace Parkopolis
{
    class GarageHandler : IHandler
    {
        private Garage<IVehicle>? _garageInstance;


        // Use property in methods
        private Garage<IVehicle> GarageInstance
        {
            get
            {
                return _garageInstance ?? throw new InvalidOperationException(
                    "Garage has not been initialized. Call InitializeGarage() first.");
            }
        }

        public int VehicleCount => GarageInstance.Count;
        public bool IsGarageFull => GarageInstance.IsFull;
        public bool IsGarageEmpty => GarageInstance.IsEmpty;

        public bool IsRegNumExists(string regNum)
        {
            return GarageInstance.IsRegNumExists(regNum);
        }

        // Methods to use in UI

        public void InitializeGarage(int capacity)
        {
            if (capacity <= 0)
            {
                throw new ArgumentException("Garage capacity must be greater than zero.", nameof(capacity));
            }

            _garageInstance = new Garage<IVehicle>(capacity);
        }

        public Dictionary<string, int> GetVehicleTypeCounts()
        {
            return GarageInstance
                .Where(vehicle => vehicle != null)
                .GroupBy(vehicle => vehicle.GetType().Name)
                .ToDictionary(group => group.Key, group => group.Count());
        }

        public string AddVehicle(VehicleType type, string regNum, string color, bool needsElectrical, string typeSpecificParam)
        {
            IVehicle? vehicle = null;

            switch (type)
            {
                case VehicleType.Car:
                    vehicle = new Car(regNum, color, needsElectrical, typeSpecificParam);
                    break;
                default:
                    return $"Vehicle type {type} does not accept string parameter";
            }

            if (vehicle == null)
                return "Failed to create vehicle";

            string message = GarageInstance.Add(vehicle);
            return message;
        }

        // AddVehicle overload
        public string AddVehicle(VehicleType type, string regNum, string color, bool needsElectrical, bool typeSpecificParam)
        {
            IVehicle? vehicle = null;

            switch (type)
            {
                case VehicleType.Motorcycle:
                    vehicle = new Motorcycle(regNum, color, needsElectrical, typeSpecificParam);
                    break;
                case VehicleType.Bus:
                    vehicle = new Bus(regNum, color, needsElectrical, typeSpecificParam);
                    break;
                case VehicleType.Truck:
                    vehicle = new Truck(regNum, color, needsElectrical, typeSpecificParam);
                    break;
                case VehicleType.Hovercraft:
                    vehicle = new Hovercraft(regNum, color, needsElectrical, typeSpecificParam);
                    break;
                default:
                    return $"Vehicle type {type} does not accept boolean parameter";
            }

            if (vehicle == null)
                return "Failed to create vehicle";

            string message = GarageInstance.Add(vehicle);
            return message;
        }

        public string RemoveVehicle(string regNum)
        {
            if (string.IsNullOrWhiteSpace(regNum))
            {
                return "Registration number cannot be empty.";
            }
            else if (!IsRegNumExists(regNum))
            {
                return "Registration number does not exist.";
            }

            return GarageInstance.Remove(regNum);
        }

        public List<string> GetAllVehicles()
        {
            var vehicleStrings = new List<string>();
            foreach (var vehicle in GarageInstance)
            {
                if (vehicle != null)
                {
                    string vehicleInfo = FormatVehicleInfo(vehicle);
                    vehicleStrings.Add(vehicleInfo);
                }
            }
            return vehicleStrings;
        }

        private string GetVehicleSpecialInfo(IVehicle vehicle)
        {
            return vehicle switch
            {
                Car car => $"Fuel type: {car.FuelType}",

                Motorcycle motorcycle => $"Needs wall support: {(motorcycle.NeedsWallSupport ? "Yes" : "No")}",
                Bus bus => $"Needs passenger platform: {(bus.NeedsPassengerPlatform ? "Yes" : "No")}",
                Truck truck => $"Has trailer: {(truck.HasTrailer ? "Yes" : "No")}",
                Hovercraft hovercraft => $"Requires inflation space: {(hovercraft.RequiresInflationSpace ? "Yes" : "No")}",
                _ => "Unknown vehicle type"
            };
        }

        public List<string> SearchVehicles(SearchCriteria criteria)
        {
            IEnumerable<IVehicle> allRegVehicles = GarageInstance.Where(v => v != null);
            var query = ApplySearchFilters(allRegVehicles, criteria);

            return query.Select(FormatVehicleInfo).ToList();
        }

        private IEnumerable<IVehicle> ApplySearchFilters(IEnumerable<IVehicle> vehicles, SearchCriteria criteria)
        {
            if (!string.IsNullOrEmpty(criteria.RegNum))
                vehicles = vehicles.Where(v => v.RegNum.Equals(criteria.RegNum, StringComparison.OrdinalIgnoreCase));

            if (criteria.Scope == SearchScope.SpecificType && criteria.SpecificType.HasValue)
                vehicles = vehicles.Where(v => v.GetType().Name.Equals(criteria.SpecificType.Value.ToString(), StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrEmpty(criteria.Color))
                vehicles = vehicles.Where(v => v.Color.Equals(criteria.Color, StringComparison.OrdinalIgnoreCase));

            if (criteria.NeedsElectricalStation.HasValue)
                vehicles = vehicles.Where(v => v.NeedsElectricalStation == criteria.NeedsElectricalStation.Value);

            return vehicles;
        }

        private string FormatVehicleInfo(IVehicle vehicle)
        {
            string specialInfo = GetVehicleSpecialInfo(vehicle);
            return $"\nType: {vehicle.GetType().Name} \n" +
                   $"Registration number: {vehicle.RegNum} \n" +
                   $"Color: {vehicle.Color} \n" +
                   $"Needs electrical station: {(vehicle.NeedsElectricalStation ? "Yes" : "No")} \n" +
                   $"{specialInfo}";
        }

    }
}

