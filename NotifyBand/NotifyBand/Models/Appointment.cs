using System;

namespace NotifyBand.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public Course Course { get; set; }
        public DateTime DateTime { get; set; }
    }
}
