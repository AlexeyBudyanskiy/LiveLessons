using LiveLessons.BLL.DTO;

namespace LiveLesson.WEB.ViewModels.Course
{
    public class CreateCourseViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Coord Coord { get; set; }
        public string Photo { get; set; }
        public decimal Price { get; set; }
    }
}