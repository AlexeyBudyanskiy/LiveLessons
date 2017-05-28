using System;

namespace LiveLesson.WEB.ViewModels.Message
{
    public class CreateMessageViewModel
    {
        public int Id { get; set; }
        public int RecieverId { get; set; }
        public int SenderId { get; set; }
        public DateTime DateTime { get; set; }
        public string Text { get; set; }
    }
}