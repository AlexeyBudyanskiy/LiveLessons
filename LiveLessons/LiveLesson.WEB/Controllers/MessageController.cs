using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using AutoMapper;
using LiveLesson.WEB.ViewModels;
using LiveLesson.WEB.ViewModels.Message;
using LiveLessons.BLL.DTO;
using LiveLessons.BLL.Interfaces;

namespace LiveLesson.WEB.Controllers
{
    [RoutePrefix("api/messages")]
    public class MessageController : ApiController
    {
        private readonly IMessageService _messageService;
        private readonly IMapper _mapper;

        private string _testProfileId = "profileId";

        public MessageController(IMessageService messageService, IMapper mapper)
        {
            _messageService = messageService;
            _mapper = mapper;
        }

        [HttpGet, Route("{id:int}")]
        public IHttpActionResult Get(int id)
        {
            var messageDto = _messageService.Get(id);
            var messageViewModel = _mapper.Map<MessageViewModel>(messageDto);

            return Ok(messageViewModel);
        }

        [HttpGet, Route("")]
        public IHttpActionResult GetByCourseId(int courseId)
        {
            //var profileId = User.Identity.GetUserId();
            var profileId = _testProfileId;
            var messageDto = _messageService.GetCourseConversation(courseId, profileId);
            var messageViewModel = _mapper.Map<List<MessageViewModel>>(messageDto);

            return Ok(messageViewModel);
        }

        [HttpPost, Route("")]
        public IHttpActionResult Create(CreateMessageViewModel createMessageViewModel)
        {
            if (ModelState.IsValid)
            {
                var messageDto = _mapper.Map<MessageDto>(createMessageViewModel);
                //var profileId = User.Identity.GetUserId();
                var profileId = _testProfileId;
                messageDto.Sender = new UserDto {ProfileId = profileId};
                _messageService.Create(messageDto);

                return StatusCode(HttpStatusCode.Created);
            }

            return BadRequest(ModelState);
        }

        [HttpDelete, Route("{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            _messageService.Delete(id);

            return Ok();
        }
    }
}