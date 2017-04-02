using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using AutoMapper;
using LiveLessons.BLL.DTO;
using LiveLessons.BLL.Interfaces;
using LiveLessons.WEB.ViewModels;

namespace LiveLessons.WEB.ApiControllers
{
    [RoutePrefix("api/messages")]
    public class ApiMessageController : ApiController
    {
        private readonly IMessageService _messageService;

        public ApiMessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpGet, Route("")]
        public IHttpActionResult GetAll()
        {
            var messagesDto = _messageService.GetAll();
            var messagesViewModel = Mapper.Map<IEnumerable<MessageViewModel>>(messagesDto).ToList();

            return Ok(messagesViewModel);
        }

        [HttpGet, Route("{id:int}")]
        public IHttpActionResult Get(int id)
        {
                var messageDto = _messageService.Get(id);
                var messageViewModel = Mapper.Map<MessageViewModel>(messageDto);

                return Ok(messageViewModel);
        }

        [HttpPost, Route("")]
        public IHttpActionResult Create(MessageViewModel messageViewModel)
        {
            if (ModelState.IsValid)
            {
                var messageDto = Mapper.Map<MessageDto>(messageViewModel);
                _messageService.Create(messageDto);

                return StatusCode(HttpStatusCode.Created);
            }

            return BadRequest(ModelState);
        }

        [HttpPut, Route("")]
        public IHttpActionResult Edit(MessageViewModel messageViewModel)
        {
            if (ModelState.IsValid)
            {
                var messageDto = Mapper.Map<MessageDto>(messageViewModel);
                _messageService.Edit(messageDto);

                return Ok();
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