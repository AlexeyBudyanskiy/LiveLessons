using LiveLessons.BLL.DTO;

namespace LiveLesson.WEB.ViewModels
{
    public class CourseViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Coord Coord { get; set; }
        public int Rate { get; set; }
        public string Photo { get; set; }
        public UserViewModel Teacher { get; set; }
        public decimal Price { get; set; }
    }
}