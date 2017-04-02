using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using LiveLessons.BLL.DTO;
using LiveLessons.BLL.Interfaces;
using LiveLessons.WEB.ViewModels;

namespace LiveLessons.WEB.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            var coursesDto = _courseService.GetAll();
            var coursesViewModel = Mapper.Map<IEnumerable<CourseViewModel>>(coursesDto);

            return View(coursesViewModel);
        }

        [HttpGet]
        public ActionResult Get(int id)
        {
            var courseDto = _courseService.Get(id);
            var courseViewModel = Mapper.Map<CourseViewModel>(courseDto);

            return View(courseViewModel);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CourseViewModel courseViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(courseViewModel);
            }

            var courseDto = Mapper.Map<CourseDto>(courseViewModel);
            _courseService.Create(courseDto);

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Edit(int id)
        {
            var courseDto = _courseService.Get(id);
            var courseViewModel = Mapper.Map<CourseViewModel>(courseDto);

            return View(courseViewModel);
        }

        [HttpPost]
        public ActionResult Edit(CourseViewModel courseViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(courseViewModel);
            }

            var courseDto = Mapper.Map<CourseDto>(courseViewModel);
            _courseService.Edit(courseDto);

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Delete(int id)
        {
            _courseService.Delete(id);

            return RedirectToAction("Index", "Home");
        }
    }
}