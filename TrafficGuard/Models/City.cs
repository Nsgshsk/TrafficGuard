namespace TrafficGuard.Models
{
    public class City
    {
        private int id;
        private string? name;
        private List<District> districts;

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public List<District> Districts { get => districts; set => districts = value; }
    }
}
