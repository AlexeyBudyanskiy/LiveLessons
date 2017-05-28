using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using AutoMapper;
using LiveLesson.WEB.ViewModels.Appointment;
using LiveLessons.BLL.DTO;
using LiveLessons.BLL.Interfaces;
using Microsoft.AspNet.Identity;

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
        private readonly IUserService userService;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppointmentController"/> class.
        /// </summary>
        /// <param name="appointmentService">The appointment service.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="userService">Injected user service</param>
        public AppointmentController(IAppointmentService appointmentService, IMapper mapper, IUserService userService)
        {
            this.appointmentService = appointmentService;
            this.mapper = mapper;
            this.userService = userService;
        }

        /// <summary>
        /// Gets all appointments.
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("")]
        [Authorize]
        public IHttpActionResult GetAll()
        {
            var appointmentsDto = appointmentService.GetAll();
            var appointmentsViewModel = mapper.Map<IEnumerable<AppointmentViewModel>>(appointmentsDto);

            return Ok(appointmentsViewModel);
        }

        [HttpGet, Route("my")]
        [Authorize]
        public IHttpActionResult Get()
        {
            var profileId = User.Identity.GetUserId();
            var appointmentsDto = appointmentService.GetByProfileId(profileId);
            var appointmentsViewModel = mapper.Map<IEnumerable<AppointmentViewModel>>(appointmentsDto);

            return Ok(appointmentsViewModel);
        }

        /// <summary>
        /// Gets the specified appointment by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet, Route("{id:int}")]
        [Authorize]
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
        [Authorize]
        public IHttpActionResult Create(CreateAppointmentViewModel createAppointmentViewModel)
        {
            var currentUser = userService.GetByProfileId(User.Identity.GetUserId());

            if (currentUser.Id != createAppointmentViewModel.StudentId)
            {
                return StatusCode(HttpStatusCode.Forbidden);
            }

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
        [Authorize]
        public IHttpActionResult Edit(CreateAppointmentViewModel createAppointmentViewModel)
        {
            var currentUser = userService.GetByProfileId(User.Identity.GetUserId());

            if (currentUser.Id != createAppointmentViewModel.StudentId)
            {
                return StatusCode(HttpStatusCode.Forbidden);
            }

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
        [Authorize]
        public IHttpActionResult Delete(int id)
        {
            appointmentService.Delete(id);

            return Ok();
        }
    }
}