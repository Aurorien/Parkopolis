namespace Parkopolis.UI
{
    public class SearchCriteria
    {
        public SearchScope Scope { get; set; } = SearchScope.AllVehicles;
        public VehicleType? SpecificType { get; set; }
        public string? RegNum { get; set; }
        public string? Color { get; set; }
        public bool? NeedsElectricalStation { get; set; }
    }

}
