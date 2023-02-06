using TrafficGuard.Models;

namespace TrafficGuard.Validators
{
    public static class AccidentValidator
    {
        public static bool IsValid(Accident accident)
        {
            if (accident == default) return false;
            else if (accident.Location == default) return false;
            else if (accident.DateTime.Year < 1900) return false;
            else if (accident.NumVehicles < 1) return false;
            else if (String.IsNullOrWhiteSpace(accident.Description)) return false;
            else if (accident.TrustWorthyRating < 1) return false;
            else return true;
        }
    }
}
