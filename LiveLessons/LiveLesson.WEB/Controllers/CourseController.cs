using System;
using System.Collections.Generic;
using System.Net;
using System.Web;
using System.Web.Http;
using AutoMapper;
using LiveLesson.WEB.Infrastructure;
using LiveLesson.WEB.ViewModels.Course;
using LiveLessons.BLL.DTO;
using LiveLessons.BLL.Interfaces;
using Microsoft.AspNet.Identity;

namespace LiveLesson.WEB.Controllers
{
    /// <summary>
    /// Controller for courses manipulations
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    [RoutePrefix("api/courses")]
    public class CourseController : ApiController
    {
        private readonly ICourseService courseService;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="CourseController"/> class.
        /// </summary>
        /// <param name="courseService">The course service.</param>
        /// <param name="mapper">The mapper.</param>
        public CourseController(ICourseService courseService, IMapper mapper)
        {
            this.courseService = courseService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Gets all courses.
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("")]
        public IHttpActionResult GetAll()
        {
            var coursesDto = courseService.GetAll();
            var coursesViewModel = mapper.Map<IEnumerable<CourseViewModel>>(coursesDto);

            return Ok(coursesViewModel);
        }

        [HttpGet, Route("my")]
        public IHttpActionResult Get()
        {
            var profileId = User.Identity.GetUserId();
            var coursesDto = courseService.GetByProfileId(profileId);
            var coursesViewModel = mapper.Map<IEnumerable<CourseViewModel>>(coursesDto);

            return Ok(coursesViewModel);
        }

        /// <summary>
        /// Gets the specified course by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet, Route("{id:int}")]
        public IHttpActionResult Get(int id)
        {
            var courseDto = courseService.Get(id);
            var courseViewModel = mapper.Map<CourseViewModel>(courseDto);

            return Ok(courseViewModel);
        }

        /// <summary>
        /// Get appointments sorted by distance from user. Paginated
        /// </summary>
        /// <param name="coordX">User coord x.</param>
        /// <param name="coordY">User coord y.</param>
        /// <param name="page">The page.</param>
        /// <param name="itemsPerPage">Items per page.</param>
        /// <returns></returns>
        [HttpGet, Route("nearest")]
        public IHttpActionResult Nearest(double coordX, double coordY, int page, int itemsPerPage)
        {
            var courseDto = courseService.FindNearest(coordX, coordY, page, itemsPerPage);
            var courseViewModel = mapper.Map<List<CourseViewModel>>(courseDto);

            return Ok(courseViewModel);
        }

        /// <summary>
        /// Searches courses by some string.
        /// </summary>
        /// <param name="coordX">User coord x.</param>
        /// <param name="coordY">User coord y.</param>
        /// <param name="page">The page.</param>
        /// <param name="itemsPerPage">Items per page.</param>
        /// <param name="searchString">The search string.</param>
        /// <returns></returns>
        [HttpGet, Route("search")]
        public IHttpActionResult Search(double coordX, double coordY, string searchString, int page, int itemsPerPage)
        {
            var courseDto = courseService.Search(coordX, coordY, searchString, page, itemsPerPage);
            var courseViewModel = mapper.Map<List<CourseViewModel>>(courseDto);

            return Ok(courseViewModel);
        }

        /// <summary>
        /// Creates the specified course.
        /// </summary>
        /// <param name="createCourseViewModel">Course view model.</param>
        /// <returns></returns>
        [HttpPost, Route("")]
        [Authorize]
        public IHttpActionResult Create(CreateCourseViewModel createCourseViewModel)
        {
            if (ModelState.IsValid)
            {
                var courseDto = mapper.Map<CourseDto>(createCourseViewModel);
                var profileId = User.Identity.GetUserId();
                courseDto.Teacher = new UserDto { ProfileId = profileId };
                courseService.Create(courseDto);

                return StatusCode(HttpStatusCode.Created);
            }

            return BadRequest(ModelState);
        }

        /// <summary>
        /// Edits the specified create course.
        /// </summary>
        /// <param name="createCourseViewModel">The create course view model.</param>
        /// <returns></returns>
        [HttpPut, Route("")]
        [Authorize]
        public IHttpActionResult Edit(CreateCourseViewModel createCourseViewModel)
        {
            if (ModelState.IsValid)
            {
                var courseDto = mapper.Map<CourseDto>(createCourseViewModel);
                var profileId = User.Identity.GetUserId();
                courseDto.Teacher = new UserDto { ProfileId = profileId };
                courseService.Edit(courseDto);

                return StatusCode(HttpStatusCode.Created);
            }

            return BadRequest(ModelState);
        }

        /// <summary>
        /// Deletes the specified course.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpDelete, Route("{id:int}")]
        [Authorize]
        public IHttpActionResult Delete(int id)
        {
            courseService.Delete(id);

            return Ok();
        }

        [HttpPost, Route("image")]
        [Authorize]
        public IHttpActionResult LoadImage()
        {
            var file = HttpContext.Current.Request.Files.Count > 0
                ? HttpContext.Current.Request.Files[0]
                : null;

            if (file == null)
            {
                return NotFound();
            }

            try
            {
                ImageLoader.SaveImage(file, HttpRuntime.AppDomainAppPath);
            }
            catch (ArgumentException)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}