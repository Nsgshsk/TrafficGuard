using TrafficGuard.Models;
using TrafficGuard.Validators;

namespace TrafficGuard.Services
{
    public static class ValidateModelService
    {
        public static void CheckModel(City city) { if (!CityValidator.IsValid(city)) throw new ArgumentException("City was not in correct format!"); }
        public static void CheckModel(District district) { if (!DistrictValidator.IsValid(district)) throw new ArgumentException("District was not in correct format!"); }
        public static void CheckModel(Location location) { if (!LocationValidator.IsValid(location)) throw new ArgumentException("Location was not in correct format!"); }
        public static void CheckModel(Accident accident) { if (!AccidentValidator.IsValid(accident)) throw new ArgumentException("Accident was not in correct format!"); }
        public static void CheckModel(AccidentReport accident) { if (!AccidentReportValidator.IsValid(accident)) throw new ArgumentException("Report was not in correct format!"); }
    }
}
