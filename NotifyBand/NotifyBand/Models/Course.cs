namespace NotifyBand.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Rate { get; set; }
        public decimal Price { get; set; }
        public Coord Coords { get; set; }
    }
}
