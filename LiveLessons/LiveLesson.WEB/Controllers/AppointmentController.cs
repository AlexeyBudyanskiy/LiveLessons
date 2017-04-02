using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using AutoMapper;
using LiveLesson.WEB.ViewModels;
using LiveLessons.BLL.DTO;
using LiveLessons.BLL.Interfaces;

namespace LiveLesson.WEB.Controllers
{
    [RoutePrefix("api/appointments")]
    public class AppointmentController : ApiController
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [HttpGet, Route("")]
        public IHttpActionResult GetAll()
        {
            var appointmentsDto = _appointmentService.GetAll();
            var appointmentsViewModel = Mapper.Map<IEnumerable<AppointmentViewModel>>(appointmentsDto).ToList();

            return Ok(appointmentsViewModel);
        }

        [HttpGet, Route("{id:int}")]
        public IHttpActionResult Get(int id)
        {
            var appointmentDto = _appointmentService.Get(id);
            var appointmentViewModel = Mapper.Map<AppointmentViewModel>(appointmentDto);

            return Ok(appointmentViewModel);
        }

        [HttpPost, Route("")]
        public IHttpActionResult Create(CreateAppointmentViewModel createAppointmentViewModel)
        {
            if (ModelState.IsValid)
            {
                var appointmentDto = Mapper.Map<AppointmentDto>(createAppointmentViewModel);
                _appointmentService.Create(appointmentDto);

                return StatusCode(HttpStatusCode.Created);
            }

            return BadRequest(ModelState);
        }

        [HttpPut, Route("")]
        public IHttpActionResult Edit(CreateAppointmentViewModel createAppointmentViewModel)
        {
            if (ModelState.IsValid)
            {
                var appointmentDto = Mapper.Map<AppointmentDto>(createAppointmentViewModel);
                _appointmentService.Edit(appointmentDto);

                return Ok();
            }

            return BadRequest(ModelState);
        }

        [HttpDelete, Route("{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            _appointmentService.Delete(id);

            return Ok();
        }
    }
}