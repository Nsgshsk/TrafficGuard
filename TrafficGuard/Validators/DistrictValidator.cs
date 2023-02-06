using TrafficGuard.Models;

namespace TrafficGuard.Validators
{
    public static class DistrictValidator
    {
        public static bool IsValid(District district)
        {
            if (district == default) return false;
            else if (String.IsNullOrWhiteSpace(district.Name)) return false;
            else if (district.City == default) return false;
            else return true;
        }
    }
}
