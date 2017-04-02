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
    [RoutePrefix("api/examples")]
    public class ApiExampleController : ApiController
    {
        private readonly IExampleService _exampleService;

        public ApiExampleController(IExampleService exampleService)
        {
            _exampleService = exampleService;
        }

        [HttpGet, Route("")]
        public IHttpActionResult GetAll()
        {
            var examplesDto = _exampleService.GetAll();
            var examplesViewModel = Mapper.Map<IEnumerable<ExampleViewModel>>(examplesDto).ToList();

            return Ok(examplesViewModel);
        }

        [HttpGet, Route("{id:int}")]
        public IHttpActionResult Get(int id)
        {
                var exampleDto = _exampleService.Get(id);
                var exampleViewModel = Mapper.Map<ExampleViewModel>(exampleDto);

                return Ok(exampleViewModel);
        }

        [HttpPost, Route("")]
        public IHttpActionResult Create(ExampleViewModel exampleViewModel)
        {
            if (ModelState.IsValid)
            {
                var exampleDto = Mapper.Map<ExampleDto>(exampleViewModel);
                _exampleService.Create(exampleDto);

                return StatusCode(HttpStatusCode.Created);
            }

            return BadRequest(ModelState);
        }

        [HttpPut, Route("")]
        public IHttpActionResult Edit(ExampleViewModel exampleViewModel)
        {
            if (ModelState.IsValid)
            {
                var exampleDto = Mapper.Map<ExampleDto>(exampleViewModel);
                _exampleService.Edit(exampleDto);

                return Ok();
            }

            return BadRequest(ModelState);
        }

        [HttpDelete, Route("{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            _exampleService.Delete(id);

            return Ok();
        }
    }
}