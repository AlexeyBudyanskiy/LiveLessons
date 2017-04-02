using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using LiveLessons.BLL.DTO;
using LiveLessons.BLL.Interfaces;
using LiveLessons.WEB.ViewModels;

namespace LiveLessons.WEB.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            var appointmentsDto = _appointmentService.GetAll();
            var appointmentsViewModel = Mapper.Map<IEnumerable<AppointmentViewModel>>(appointmentsDto);

            return View(appointmentsViewModel);
        }

        [HttpGet]
        public ActionResult Get(int id)
        {
            var appointmentDto = _appointmentService.Get(id);
            var appointmentViewModel = Mapper.Map<AppointmentViewModel>(appointmentDto);

            return View(appointmentViewModel);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(AppointmentViewModel appointmentViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(appointmentViewModel);
            }

            var appointmentDto = Mapper.Map<AppointmentDto>(appointmentViewModel);
            _appointmentService.Create(appointmentDto);

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Edit(int id)
        {
            var appointmentDto = _appointmentService.Get(id);
            var appointmentViewModel = Mapper.Map<AppointmentViewModel>(appointmentDto);

            return View(appointmentViewModel);
        }

        [HttpPost]
        public ActionResult Edit(AppointmentViewModel appointmentViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(appointmentViewModel);
            }

            var appointmentDto = Mapper.Map<AppointmentDto>(appointmentViewModel);
            _appointmentService.Edit(appointmentDto);

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Delete(int id)
        {
            _appointmentService.Delete(id);

            return RedirectToAction("Index", "Home");
        }
    }
}