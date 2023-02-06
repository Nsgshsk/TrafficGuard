using Microsoft.CodeAnalysis;

namespace TrafficGuard.Validators
{
    public static class LocationValidator
    {
        public static bool IsValid(Models.Location location)
        {
            if (location == default) return false;
            else if (Double.IsNaN((double)location.Latitude) && Double.IsNaN((double)location.Latitude)) return false;
            else if (location.District == default) return false;
            else return true;
        }
    }
}
