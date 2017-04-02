using System;

namespace LiveLessons.DAL.Entities
{
    public class Appointment : BaseType
    {
        public virtual User Student { get; set; }
        public virtual Course Course { get; set; }
        public DateTime DateTime { get; set; }
    }
}
