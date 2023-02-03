namespace TrafficGuard.Models
{
    public class Location
    {
        private int id;
        private double latitude;
        private double longitude;
        private Accident accident;

        public int Id { get => id; set => id = value; }
        public double Latitude { get => latitude; set => latitude = value; }
        public double Longitude { get => longitude; set => longitude = value; }
    }
}
