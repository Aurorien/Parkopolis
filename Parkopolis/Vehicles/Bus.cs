namespace Parkopolis.Vehicles
{
    internal class Bus : Vehicle
    {
        private readonly bool _needsPassengerPlatform;
        public bool NeedsPassengerPlatform => _needsPassengerPlatform;

        public Bus(string regNum, string color, bool needsElectricalStation, bool needsPassengerPlatform) : base(regNum, color, needsElectricalStation)
        {
            this._needsPassengerPlatform = needsPassengerPlatform;
        }

        public override string ToString()
        {
            return base.ToString() + $"Needs passenger platform: {(NeedsPassengerPlatform ? "Yes" : "No")}";
        }
    }
}
