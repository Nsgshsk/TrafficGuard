namespace TrafficGuard.Models
{
    public class District
    {
        private int id;
        private string? name;
        private List<Location> locations;

        public int Id { get => id; set => id = value; }
        public string? Name { get => name; set => name = value; }
        public List<Location> Locations { get => locations; set => locations = value; }
    }
}
