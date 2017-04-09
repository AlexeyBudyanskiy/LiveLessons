using System.Collections.Generic;

namespace NotifyBand.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Course> Courses { get; set; }
        public List<Appointment> Appointments { get; set; }
        public string AccessToken { get; set; }
    }
}
