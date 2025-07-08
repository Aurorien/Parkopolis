namespace Parkopolis.Vehicles
{
    internal class Motorcycle : Vehicle
    {
        private readonly bool _needsWallSupport;
        public bool NeedsWallSupport => _needsWallSupport;

        public Motorcycle(string regNum, string color, bool needsElectricalStation, bool needsWallSupport) : base(regNum, color, needsElectricalStation)
        {
            this._needsWallSupport = needsWallSupport;
        }
        public override string ToString()
        {
            return base.ToString() + $"Needs wall support: {(NeedsWallSupport ? "Yes" : "No")}";
        }
    }
}
