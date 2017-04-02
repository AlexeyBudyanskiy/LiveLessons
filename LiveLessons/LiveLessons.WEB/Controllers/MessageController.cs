using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using LiveLessons.BLL.DTO;
using LiveLessons.BLL.Interfaces;
using LiveLessons.WEB.ViewModels;

namespace LiveLessons.WEB.Controllers
{
    public class MessageController : Controller
    {
        private readonly IMessageService _messageService;

        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            var messagesDto = _messageService.GetAll();
            var messagesViewModel = Mapper.Map<IEnumerable<MessageViewModel>>(messagesDto);

            return View(messagesViewModel);
        }

        [HttpGet]
        public ActionResult Get(int id)
        {
            var messageDto = _messageService.Get(id);
            var messageViewModel = Mapper.Map<MessageViewModel>(messageDto);

            return View(messageViewModel);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(MessageViewModel messageViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(messageViewModel);
            }

            var messageDto = Mapper.Map<MessageDto>(messageViewModel);
            _messageService.Create(messageDto);

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Edit(int id)
        {
            var messageDto = _messageService.Get(id);
            var messageViewModel = Mapper.Map<MessageViewModel>(messageDto);

            return View(messageViewModel);
        }

        [HttpPost]
        public ActionResult Edit(MessageViewModel messageViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(messageViewModel);
            }

            var messageDto = Mapper.Map<MessageDto>(messageViewModel);
            _messageService.Edit(messageDto);

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Delete(int id)
        {
            _messageService.Delete(id);

            return RedirectToAction("Index", "Home");
        }
    }
}