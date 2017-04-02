using System;

namespace LiveLessons.BLL.DTO
{
    public class MessageDto
    {
        public int Id { get; set; }
        public UserDto Sender { get; set; }
        public UserDto Reciever { get; set; }
        public DateTime DateTime { get; set; }
        public string Text { get; set; }
        public CourseDto Course { get; set; }
    }
}
