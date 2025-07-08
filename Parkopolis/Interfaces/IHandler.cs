using Parkopolis.UI.Operations.FilterVehicle;

namespace Parkopolis.Interfaces
{
    public interface IHandler
    {
        int VehicleCount { get; }
        bool IsGarageFull { get; }
        bool IsGarageEmpty { get; }

        bool IsRegNumExists(string regNum);
        void InitializeGarage(int capacity);
        Dictionary<string, int> GetVehicleTypeCounts();

        string AddVehicle(VehicleType type, string regNum, string color, bool needsElectrical, string typeSpecificParam);
        string AddVehicle(VehicleType type, string regNum, string color, bool needsElectrical, bool typeSpecificParam);
        string RemoveVehicle(string regNum);

        List<IVehicle> GetAllVehicles();
        List<IVehicle> FilterVehicles(FilterCriteria criteria);
    }
}
