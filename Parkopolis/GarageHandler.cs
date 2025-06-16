namespace Parkopolis
{
    class GarageHandler<T> where T : Vehicle
    {
        private readonly Garage<T> _garage;

        public GarageHandler(Garage<T> garage)
        {
            _garage = garage;
        }

        public bool RegisterVehicle(T vehicle) // Method to use in UI
        {
            if (_garage.IsRegNumExists(vehicle.RegNum))
            {
                Console.WriteLine($"It is not possible to register the vehicle because RegNum '{vehicle.RegNum}' is already registered.");
                return false;
            }

            Console.WriteLine("Failed to register vehicle due to an unexpected error.");
            return false;
        }

    }
}
