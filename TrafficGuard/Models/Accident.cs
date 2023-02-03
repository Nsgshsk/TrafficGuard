namespace TrafficGuard.Models
{
    public class Accident
    {
        private int id;
        private DateTime dateTime;
        private int numVehicles;
        private string description;
        private string pathToFiles;
        private int trustWorthyRating;

        public int Id { get => id; set => id = value; }
        public DateTime DateTime { get => dateTime; set => dateTime = value; }
        public int NumVehicles { get => numVehicles; set => numVehicles = value; }
        public string Description { get => description; set => description = value; }
        public string PathToFiles { get => pathToFiles; set => pathToFiles = value; }
        public int TrustWorthyRating { get => trustWorthyRating; set => trustWorthyRating = value; }
    }
}
