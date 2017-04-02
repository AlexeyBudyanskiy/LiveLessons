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
    [RoutePrefix("api/courses")]
    public class ApiCourseController : ApiController
    {
        private readonly ICourseService _courseService;

        public ApiCourseController(ICourseService courseService)
        {
            _courseService = courseService;
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
        public IHttpActionResult Create(CourseViewModel courseViewModel)
        {
            if (ModelState.IsValid)
            {
                var courseDto = Mapper.Map<CourseDto>(courseViewModel);
                _courseService.Create(courseDto);

                return StatusCode(HttpStatusCode.Created);
            }

            return BadRequest(ModelState);
        }

        [HttpPut, Route("")]
        public IHttpActionResult Edit(CourseViewModel courseViewModel)
        {
            if (ModelState.IsValid)
            {
                var courseDto = Mapper.Map<CourseDto>(courseViewModel);
                _courseService.Edit(courseDto);

                return Ok();
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