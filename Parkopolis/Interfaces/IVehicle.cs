namespace Parkopolis.Interfaces
{
    public interface IVehicle
    {
        string RegNum { get; }
        string Color { get; }
        bool NeedsElectricalStation { get; }
    }
}
