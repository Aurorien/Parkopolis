namespace Parkopolis.Vehicles
{
    internal class Hovercraft : Vehicle
    {
        private readonly bool _requiresInflationSpace;
        public bool RequiresInflationSpace => _requiresInflationSpace;

        public Hovercraft(string regNum, string color, bool needsElectricalStation, bool requiresInflationSpace)
            : base(regNum, color, needsElectricalStation)
        {
            this._requiresInflationSpace = requiresInflationSpace;
        }
    }
}
