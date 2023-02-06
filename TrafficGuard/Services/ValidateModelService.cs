using TrafficGuard.Models;
using TrafficGuard.Validators;

namespace TrafficGuard.Services
{
    public static class ValidateModelService
    {
        public static void CheckCity(City city) { if (!CityValidator.IsValid(city)) throw new ArgumentException("City was not in correct format!"); }
        public static void CheckDistrict(District district) { if (!DistrictValidator.IsValid(district)) throw new ArgumentException("District was not in correct format!"); }
        public static void CheckLocation(Location location) { if (!LocationValidator.IsValid(location)) throw new ArgumentException("Location was not in correct format!"); }
        public static void CheckAccident(Accident accident) { if (!AccidentValidator.IsValid(accident)) throw new ArgumentException("Accident was not in correct format!"); }
    }
}
