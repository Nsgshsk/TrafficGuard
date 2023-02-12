namespace TrafficGuard.Models
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class AddressJson
    {
        public string? Match_addr { get; set; }
        public string? LongLabel { get; set; }
        public string? ShortLabel { get; set; }
        public string? Addr_type { get; set; }
        public string? Type { get; set; }
        public string? PlaceName { get; set; }
        public string? AddNum { get; set; }
        public string? Address { get; set; }
        public string? Block { get; set; }
        public string? Sector { get; set; }
        public string? Neighborhood { get; set; }
        public string? District { get; set; }
        public string? City { get; set; }
        public string? MetroArea { get; set; }
        public string? Subregion { get; set; }
        public string? Region { get; set; }
        public string? RegionAbbr { get; set; }
        public string? Territory { get; set; }
        public string? Postal { get; set; }
        public string? PostalExt { get; set; }
        public string? CntryName { get; set; }
        public string? CountryCode { get; set; }
    }

    public class LocationNull
    {
        public double x { get; set; }
        public double y { get; set; }
        public SpatialReference? spatialReference { get; set; }
    }

    public class Root
    {
        public AddressJson? address { get; set; }
        public LocationNull? location { get; set; }
    }

    public class SpatialReference
    {
        public int wkid { get; set; }
        public int latestWkid { get; set; }
    }
}
