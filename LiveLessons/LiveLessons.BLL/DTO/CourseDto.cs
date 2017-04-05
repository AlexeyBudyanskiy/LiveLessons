namespace LiveLessons.BLL.DTO
{
    public class CourseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Rate { get; set; }
        public string Photo { get; set; }
        public UserDto Teacher { get; set; }
        public decimal Price { get; set; }
        public Coord Coord { get; set; }
    }
}
