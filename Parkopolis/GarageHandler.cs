using Parkopolis.Interfaces;

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

        // Methods to use in UI

        public void AddVehicle()
        {
            throw new NotImplementedException();
        }

        public void RemoveVehicle()
        {
            throw new NotImplementedException();
        }

        public void InitializeGarage(int capacity)
        {
            if (capacity <= 0)
            {
                throw new ArgumentException("Garage capacity must be greater than zero.", nameof(capacity));
            }

            _garageInstance = new Garage<IVehicle>(capacity);
        }

        public void SearchVehicle()
        {
            throw new NotImplementedException();
        }

        public void SearchVehicleRegNum()
        {
            throw new NotImplementedException();
        }

        public void ShowAllVehicleTypes()
        {
            throw new NotImplementedException();
        }

        public void ShowAllVehicles()
        {
            throw new NotImplementedException();
        }

    }
}

