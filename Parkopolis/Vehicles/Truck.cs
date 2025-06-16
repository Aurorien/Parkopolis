namespace Parkopolis.Vehicles
{
    internal class Truck : Vehicle
    {
        private readonly bool _hasTrailer;
        public Truck(string regNum, string color, bool needsElectricalStation, bool hasTrailer) : base(regNum, color, needsElectricalStation)
        {
            this._hasTrailer = hasTrailer;
        }
    }
}