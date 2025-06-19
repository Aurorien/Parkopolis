using Parkopolis.Interfaces;
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
                    string specialInfo = GetVehicleSpecialInfo(vehicle);
                    string vehicleInfo = $"RegNum: {vehicle.RegNum}, Color: {vehicle.Color}, " +
                                       $"Type: {vehicle.GetType().Name}, " +
                                       $"NeedsElectricalStation: {(vehicle.NeedsElectricalStation ? "Yes" : "No")}, " +
                                       $"{specialInfo}";
                    vehicleStrings.Add(vehicleInfo);
                }
            }
            return vehicleStrings;
        }

        private string GetVehicleSpecialInfo(IVehicle vehicle)
        {
            return vehicle switch
            {
                Car car => $"FuelType: {car.FuelType}", // You'll need to expose this property
                Motorcycle motorcycle => $"NeedsWallSupport: {(motorcycle.NeedsWallSupport ? "Yes" : "No")}", // You'll need to expose this property
                Bus bus => $"NeedsPassengerPlatform: {(bus.NeedsPassengerPlatform ? "Yes" : "No")}", // You'll need to expose this property
                Truck truck => $"HasTrailer: {(truck.HasTrailer ? "Yes" : "No")}", // You'll need to expose this property
                Hovercraft hovercraft => $"RequiresInflationSpace: {(hovercraft.RequiresInflationSpace ? "Yes" : "No")}", // You'll need to expose this property
                _ => "Unknown vehicle type"
            };
        }


    }
}

