using Microsoft.CodeAnalysis;
using TrafficGuard.Models;

namespace TrafficGuard.Validators
{
    public static class AccidentReportValidator
    {
        public static bool IsValid(AccidentReport accident)
        {
            if (accident == default) return false;
            else if (Double.IsNaN((double)accident.Latitude) && Double.IsNaN((double)accident.Longitude)) return false;
            else if (accident.DateTime.Year < 1900) return false;
            else if (accident.NumVehicles < 1) return false;
            else if (String.IsNullOrWhiteSpace(accident.Description)) return false;
            else if (accident.TrustWorthyRating < 0) return false;
            else return true;
        }
    }
}
