using System;
using LiveLesson.WEB.ViewModels.Course;
using LiveLesson.WEB.ViewModels.User;

namespace LiveLesson.WEB.ViewModels.Appointment
{
    public class AppointmentViewModel
    {
        public int Id { get; set; }
        public UserViewModel Student { get; set; }
        public CourseViewModel Course { get; set; }
        public DateTime DateTime { get; set; }
    }
}