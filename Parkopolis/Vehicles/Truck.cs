namespace Parkopolis.Vehicles
{
    internal class Truck : Vehicle
    {
        private readonly bool _hasTrailer;
        public bool HasTrailer => _hasTrailer;

        public Truck(string regNum, string color, bool needsElectricalStation, bool hasTrailer) : base(regNum, color, needsElectricalStation)
        {
            this._hasTrailer = hasTrailer;
        }

        public override string ToString()
        {
            return base.ToString() + $"Has trailer: {(HasTrailer ? "Yes" : "No")}";
        }
    }
}