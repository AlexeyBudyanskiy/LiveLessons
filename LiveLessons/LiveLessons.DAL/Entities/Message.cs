using System;

namespace LiveLessons.DAL.Entities
{
    public class Message : BaseType
    {
        public string Text { get; set; }
        public DateTime DateTime { get; set; }
        public virtual User Sender { get; set; }
        public virtual User Reciever { get; set; }
        public virtual Course Course { get; set; }
    }
}
