using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using LiveLessons.BLL.DTO;
using LiveLessons.BLL.Interfaces;
using LiveLessons.DAL.Entities;
using LiveLessons.DAL.Interfaces;

namespace LiveLessons.BLL.Services
{
    public class MessageService : IMessageService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public MessageService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        
        public MessageDto Get(int id)
        {
            var message = unitOfWork.Messages.Get(id);
            var messageDto = mapper.Map<MessageDto>(message);

            return messageDto;
        }

        public List<MessageDto> GetCourseConversation(int courseId, int userId)
        {
            var messages = unitOfWork.Messages.Find(
                entity => entity.Course.Id == courseId 
                && (entity.Reciever.Id == userId
                || entity.Sender.Id == userId));

            var messageDtos = mapper.Map<List<MessageDto>>(messages);

            return messageDtos;
        }

        public List<MessageDto> GetCourseConversation(int courseId, string profileId)
        {
            var messages = unitOfWork.Messages.Find(
                mes => mes?.Course.Id == courseId
                && (mes.Reciever.ProfileId.Equals(profileId)
                || mes.Sender.ProfileId.Equals(profileId)));

            var messageDtos = mapper.Map<List<MessageDto>>(messages.ToList());

            return messageDtos;
        }

        public void Create(MessageDto messageDto)
        {
            var message = mapper.Map<Message>(messageDto);
            message.Course = unitOfWork.Courses.Get(messageDto.Course.Id);
            message.Reciever = unitOfWork.Users.Get(messageDto.Reciever.Id);
            message.Sender = unitOfWork.Users.Find(user => user.ProfileId.Equals(message.Sender.ProfileId)).FirstOrDefault();

            unitOfWork.Messages.Create(message);
            unitOfWork.Save();
        }

        public void Delete(int id)
        {
            unitOfWork.Messages.Delete(id);
            unitOfWork.Save();
        }
    }
}