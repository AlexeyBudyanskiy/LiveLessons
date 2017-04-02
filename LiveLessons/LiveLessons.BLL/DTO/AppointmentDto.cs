using System;

namespace LiveLessons.BLL.DTO
{
    public class AppointmentDto
    {
        public int Id { get; set; }
        public UserDto Student { get; set; }
        public CourseDto Course { get; set; }
        public DateTime DateTime { get; set; }
    }
}
