using System.Collections.Generic;

namespace LiveLessons.DAL.Entities
{
    public class User : BaseType
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string ProfileId { get; set; }

        public virtual List<Course> Courses { get; set; }
        public virtual List<Message> Messages { get; set; }
        public virtual List<Appointment> Appointments { get; set; }
    }
}
