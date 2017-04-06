using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using AutoMapper;
using LiveLesson.WEB.Infrastructure;
using LiveLesson.WEB.ViewModels;
using LiveLessons.BLL.DTO;
using LiveLessons.BLL.Interfaces;

namespace LiveLesson.WEB.Controllers
{
    [RoutePrefix("api/courses")]
    public class CourseController : ApiController
    {
        private readonly ICourseService _courseService;
        private readonly IMapper _mapper;

        private string _testProfileId = "profileId";

        public CourseController(ICourseService courseService, IMapper mapper)
        {
            _courseService = courseService;
            _mapper = mapper;
        }

        [HttpGet, Route("")]
        public IHttpActionResult GetAll()
        {
            var coursesDto = _courseService.GetAll();
            var coursesViewModel = _mapper.Map<IEnumerable<CourseViewModel>>(coursesDto).ToList();

            return Ok(coursesViewModel);
        }

        [HttpGet, Route("{id:int}")]
        public IHttpActionResult Get(int id)
        {
                var courseDto = _courseService.Get(id);
                var courseViewModel = _mapper.Map<CourseViewModel>(courseDto);

                return Ok(courseViewModel);
        }

        [HttpGet, Route("nearest")]
        public IHttpActionResult Nearest(double x, double y)
        {
            var courseDto = _courseService.FindNearest(x,y);
            var courseViewModel = _mapper.Map<List<CourseViewModel>>(courseDto);

            return Ok(courseViewModel);
        }

        [HttpGet, Route("search")]
        public IHttpActionResult Search(double x, double y, string searchString)
        {
            var courseDto = _courseService.Search(x, y, searchString);
            var courseViewModel = _mapper.Map<List<CourseViewModel>>(courseDto);

            return Ok(courseViewModel);
        }

        [HttpPost, Route("")]
        public IHttpActionResult Create(CreateCourseViewModel createCourseViewModel)
        {
            if (ModelState.IsValid)
            {
                var courseDto = _mapper.Map<CourseDto>(createCourseViewModel);
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
                var courseDto = _mapper.Map<CourseDto>(createCourseViewModel);
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

        [HttpPost]
        public IHttpActionResult LoadImage(HttpPostedFileBase file)
        {
            if (file == null)
            {
                return NotFound();
            }

            try
            {
                ImageLoader.SaveImage(file);
            }
            catch (ArgumentException)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}