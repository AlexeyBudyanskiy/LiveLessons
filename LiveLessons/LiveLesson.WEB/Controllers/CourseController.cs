using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using AutoMapper;
using LiveLesson.WEB.ViewModels;
using LiveLessons.BLL.DTO;
using LiveLessons.BLL.Interfaces;
using Microsoft.AspNet.Identity;

namespace LiveLesson.WEB.Controllers
{
    [RoutePrefix("api/courses")]
    public class CourseController : ApiController
    {
        private readonly ICourseService _courseService;
        private readonly IUserService _userService;

        private string _testProfileId = "profileId";

        public CourseController(ICourseService courseService, IUserService userService)
        {
            _courseService = courseService;
            _userService = userService;
        }

        [HttpGet, Route("")]
        public IHttpActionResult GetAll()
        {
            var coursesDto = _courseService.GetAll();
            var coursesViewModel = Mapper.Map<IEnumerable<CourseViewModel>>(coursesDto).ToList();

            return Ok(coursesViewModel);
        }

        [HttpGet, Route("{id:int}")]
        public IHttpActionResult Get(int id)
        {
                var courseDto = _courseService.Get(id);
                var courseViewModel = Mapper.Map<CourseViewModel>(courseDto);

                return Ok(courseViewModel);
        }

        [HttpPost, Route("")]
        public IHttpActionResult Create(CreateCourseViewModel createCourseViewModel)
        {
            if (ModelState.IsValid)
            {
                var courseDto = Mapper.Map<CourseDto>(createCourseViewModel);
                //var profileId = User.Identity.GetUserId();
                var profileId = _testProfileId;
                courseDto.Teacher = new UserDto { ProfileId = profileId };
                _courseService.Create(courseDto);

                return StatusCode(HttpStatusCode.Created);
            }

            return BadRequest(ModelState);
        }

        [HttpPut, Route("")]
        public IHttpActionResult Edit(CreateCourseViewModel createCourseViewModel)
        {
            if (ModelState.IsValid)
            {
                var courseDto = Mapper.Map<CourseDto>(createCourseViewModel);
                //var profileId = User.Identity.GetUserId();
                var profileId = _testProfileId;
                courseDto.Teacher = new UserDto { ProfileId = profileId };
                _courseService.Edit(courseDto);

                return StatusCode(HttpStatusCode.Created);
            }

            return BadRequest(ModelState);
        }

        [HttpDelete, Route("{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            _courseService.Delete(id);

            return Ok();
        }
    }
}