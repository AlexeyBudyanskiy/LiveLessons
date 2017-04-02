using System;

namespace LiveLesson.WEB.ViewModels
{
    public class MessageViewModel
    {
        public int Id { get; set; }
        public UserViewModel Sender { get; set; }
        public UserViewModel Reciever { get; set; }
        public DateTime DateTime { get; set; }
        public string Text { get; set; }
        public CourseViewModel Course { get; set; }
    }
}