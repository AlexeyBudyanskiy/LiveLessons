using System;

namespace LiveLesson.WEB.ViewModels
{
    public class AppointmentViewModel
    {
        public int Id { get; set; }
        public UserViewModel Student { get; set; }
        public CourseViewModel Course { get; set; }
        public DateTime DateTime { get; set; }
    }
}