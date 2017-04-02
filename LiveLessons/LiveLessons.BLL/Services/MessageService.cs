using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using LiveLessons.BLL.DTO;
using LiveLessons.BLL.Interfaces;
using LiveLessons.DAL.Entities;
using LiveLessons.DAL.Interfaces;

namespace LiveLessons.BLL.Services
{
    public class MessageService : IMessageService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MessageService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public MessageDto Get(int id)
        {
            var message = _unitOfWork.Messages.Get(id);
            var messageDto = Mapper.Map<MessageDto>(message);

            return messageDto;
        }

        public List<MessageDto> GetCourseConversation(int courseId, int userId)
        {
            var messages = _unitOfWork.Messages.Find(
                entity => entity.Course.Id == courseId 
                && (entity.Reciever.Id == userId
                || entity.Sender.Id == userId));

            var messageDtos = Mapper.Map<List<MessageDto>>(messages);

            return messageDtos;
        }

        public List<MessageDto> GetCourseConversation(int courseId, string profileId)
        {
            var messages = _unitOfWork.Messages.Find(
                mes => mes?.Course.Id == courseId
                && (mes.Reciever.ProfileId.Equals(profileId)
                || mes.Sender.ProfileId.Equals(profileId)));

            var messageDtos = Mapper.Map<List<MessageDto>>(messages.ToList());

            return messageDtos;
        }

        public void Create(MessageDto messageDto)
        {
            var message = Mapper.Map<Message>(messageDto);
            message.Course = _unitOfWork.Courses.Get(messageDto.Course.Id);
            message.Reciever = _unitOfWork.Users.Get(messageDto.Reciever.Id);
            message.Sender = _unitOfWork.Users.Find(user => user.ProfileId.Equals(message.Sender.ProfileId)).FirstOrDefault();

            _unitOfWork.Messages.Create(message);
            _unitOfWork.Save();
        }

        public void Delete(int id)
        {
            _unitOfWork.Messages.Delete(id);
            _unitOfWork.Save();
        }
    }
}