using TrafficGuard.Models;

namespace TrafficGuard.Validators
{
    public static class CityValidator
    {
        public static bool IsValid(City city)
        {
            if (city == default) return false;
            else if (String.IsNullOrWhiteSpace(city.Name)) return false;
            else return true;
        }
    }
}
