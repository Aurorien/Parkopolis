namespace Parkopolis.Vehicles
{
    internal class Car : Vehicle
    {
        private readonly string _fuelType;
        public string FuelType => _fuelType;

        public Car(string regNum, string color, bool needsElectricalStation, string fuelType)
            : base(regNum, color, needsElectricalStation)
        {
            this._fuelType = fuelType;
        }
    }
}
