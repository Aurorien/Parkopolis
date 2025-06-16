namespace Parkopolis.Vehicles
{
    internal class Bus : Vehicle
    {
        private readonly bool _needsPassengerPlatform;
        public Bus(string regNum, string color, bool needsElectricalStation, bool needsPassengerPlatform) : base(regNum, color, needsElectricalStation)
        {
            this._needsPassengerPlatform = needsPassengerPlatform;
        }
    }
}
