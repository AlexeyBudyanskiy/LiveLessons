using System;

namespace LiveLesson.WEB.ViewModels.Appointment
{
    public class CreateAppointmentViewModel
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public DateTime DateTime { get; set; }
    }
}