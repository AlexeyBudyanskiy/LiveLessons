using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using AutoMapper;
using LiveLesson.WEB.ViewModels.Message;
using LiveLessons.BLL.DTO;
using LiveLessons.BLL.Interfaces;
using Microsoft.AspNet.Identity;

namespace LiveLesson.WEB.Controllers
{
    /// <summary>
    /// Controller for messages manipulations
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    [RoutePrefix("api/messages")]
    public class MessageController : ApiController
    {
        private readonly IMessageService messageService;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageController"/> class.
        /// </summary>
        /// <param name="messageService">The message service.</param>
        /// <param name="mapper">The mapper.</param>
        public MessageController(IMessageService messageService, IMapper mapper)
        {
            this.messageService = messageService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Gets the specified message by id.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet, Route("{id:int}")]
        [Authorize]
        public IHttpActionResult Get(int id)
        {
            var messageDto = messageService.Get(id);
            var messageViewModel = mapper.Map<MessageViewModel>(messageDto);

            return Ok(messageViewModel);
        }

        /// <summary>
        /// Gets messages by course identifier.
        /// </summary>
        /// <param name="courseId">The course identifier.</param>
        /// <returns></returns>
        [HttpGet, Route("")]
        [Authorize]
        public IHttpActionResult GetByCourseId(int courseId)
        {
            var profileId = User.Identity.GetUserId();
            var messageDto = messageService.GetCourseConversation(courseId, profileId);
            var messageViewModel = mapper.Map<List<MessageViewModel>>(messageDto);

            return Ok(messageViewModel);
        }

        /// <summary>
        /// Creates the specified message.
        /// </summary>
        /// <param name="createMessageViewModel">The create message view model.</param>
        /// <returns></returns>
        [HttpPost, Route("")]
        [Authorize]
        public IHttpActionResult Create(CreateMessageViewModel createMessageViewModel)
        {
            if (ModelState.IsValid)
            {
                var messageDto = mapper.Map<MessageDto>(createMessageViewModel);
                var profileId = User.Identity.GetUserId();
                messageDto.Sender = new UserDto { ProfileId = profileId };
                messageService.Create(messageDto);

                return StatusCode(HttpStatusCode.Created);
            }

            return BadRequest(ModelState);
        }
    }
}