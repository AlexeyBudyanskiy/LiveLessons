using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using AutoMapper;
using LiveLesson.WEB.ViewModels;
using LiveLessons.BLL.DTO;
using LiveLessons.BLL.Interfaces;
using Microsoft.AspNet.Identity;

namespace LiveLesson.WEB.Controllers
{
    [RoutePrefix("api/messages")]
    public class MessageController : ApiController
    {
        private readonly IMessageService _messageService;
        private readonly ICourseService _courseService;
        private readonly IUserService _userService;

        private string _testProfileId = "profileId";

        public MessageController(IMessageService messageService, ICourseService courseService, IUserService userService)
        {
            _messageService = messageService;
            _courseService = courseService;
            _userService = userService;
        }

        [HttpGet, Route("{id:int}")]
        public IHttpActionResult Get(int id)
        {
            var messageDto = _messageService.Get(id);
            var messageViewModel = Mapper.Map<MessageViewModel>(messageDto);

            return Ok(messageViewModel);
        }

        [HttpGet, Route("")]
        public IHttpActionResult GetByCourseId(int courseId)
        {
            //var profileId = User.Identity.GetUserId();
            var profileId = _testProfileId;
            var messageDto = _messageService.GetCourseConversation(courseId, profileId);
            var messageViewModel = Mapper.Map<List<MessageViewModel>>(messageDto);

            return Ok(messageViewModel);
        }

        [HttpPost, Route("")]
        public IHttpActionResult Create(CreateMessageViewModel createMessageViewModel)
        {
            if (ModelState.IsValid)
            {
                var messageDto = Mapper.Map<MessageDto>(createMessageViewModel);
                //var profileId = User.Identity.GetUserId();
                var profileId = _testProfileId;
                messageDto.Sender = new UserDto {ProfileId = _testProfileId};
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