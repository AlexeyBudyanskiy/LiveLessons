using System.Collections.Generic;

namespace LiveLessons.DAL.Entities
{
    public class Course : BaseType
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Rate { get; set; }
        public string Photo { get; set; }
        public decimal Price { get; set; }

        public double CoordX { get; set; }
        public double CoordY { get; set; }

        public virtual User Teacher { get; set; }
        public virtual List<Appointment> Appointments { get; set; }
        public virtual List<User> Students { get; set; }
        public virtual List<Message> Messages { get; set; }
    }
}
