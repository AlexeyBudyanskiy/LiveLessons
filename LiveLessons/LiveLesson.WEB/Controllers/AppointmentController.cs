using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using AutoMapper;
using LiveLesson.WEB.ViewModels.Appointment;
using LiveLessons.BLL.DTO;
using LiveLessons.BLL.Interfaces;

namespace LiveLesson.WEB.Controllers
{
    /// <summary>
    /// Controller for appointments manipulations
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    [RoutePrefix("api/appointments")]
    public class AppointmentController : ApiController
    {
        private readonly IAppointmentService appointmentService;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppointmentController"/> class.
        /// </summary>
        /// <param name="appointmentService">The appointment service.</param>
        /// <param name="mapper">The mapper.</param>
        public AppointmentController(IAppointmentService appointmentService, IMapper mapper)
        {
            this.appointmentService = appointmentService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Gets all appointments.
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("")]
        public IHttpActionResult GetAll()
        {
            var appointmentsDto = appointmentService.GetAll();
            var appointmentsViewModel = mapper.Map<IEnumerable<AppointmentViewModel>>(appointmentsDto).ToList();

            return Ok(appointmentsViewModel);
        }

        /// <summary>
        /// Gets the specified appointment by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet, Route("{id:int}")]
        public IHttpActionResult Get(int id)
        {
            var appointmentDto = appointmentService.Get(id);
            var appointmentViewModel = mapper.Map<AppointmentViewModel>(appointmentDto);

            return Ok(appointmentViewModel);
        }

        /// <summary>
        /// Creates the specified appointment.
        /// </summary>
        /// <param name="createAppointmentViewModel">The create appointment view model.</param>
        /// <returns></returns>
        [HttpPost, Route("")]
        public IHttpActionResult Create(CreateAppointmentViewModel createAppointmentViewModel)
        {
            if (ModelState.IsValid)
            {
                var appointmentDto = mapper.Map<AppointmentDto>(createAppointmentViewModel);
                appointmentService.Create(appointmentDto);

                return StatusCode(HttpStatusCode.Created);
            }

            return BadRequest(ModelState);
        }

        /// <summary>
        /// Edits the specified appointment.
        /// </summary>
        /// <param name="createAppointmentViewModel">The create appointment view model.</param>
        /// <returns></returns>
        [HttpPut, Route("")]
        public IHttpActionResult Edit(CreateAppointmentViewModel createAppointmentViewModel)
        {
            if (ModelState.IsValid)
            {
                var appointmentDto = mapper.Map<AppointmentDto>(createAppointmentViewModel);
                appointmentService.Edit(appointmentDto);

                return Ok();
            }

            return BadRequest(ModelState);
        }

        /// <summary>
        /// Deletes the specified appointment by id.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpDelete, Route("{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            appointmentService.Delete(id);

            return Ok();
        }
    }
}