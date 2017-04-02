using System.Collections.Generic;
using LiveLessons.BLL.DTO;

namespace LiveLessons.BLL.Interfaces
{
    public interface IMessageService
    {
        MessageDto Get(int id);
        List<MessageDto> GetCourseConversation(int courseId, int userId);
        List<MessageDto> GetCourseConversation(int courseId, string profileId);
        void Create(MessageDto messageDto);
        void Delete(int id);
    }
}
