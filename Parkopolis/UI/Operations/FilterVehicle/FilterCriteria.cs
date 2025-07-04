namespace Parkopolis.UI.Operations.FilterVehicle
{
    public class FilterCriteria
    {
        public FilterScope Scope { get; set; } = FilterScope.AllVehicles;
        public VehicleType? SpecificType { get; set; }
        public string? RegNum { get; set; }
        public string? Color { get; set; }
        public bool? NeedsElectricalStation { get; set; }
    }

}
