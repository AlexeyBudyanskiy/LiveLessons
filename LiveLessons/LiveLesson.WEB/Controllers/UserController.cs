using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using AutoMapper;
using LiveLesson.WEB.ViewModels;
using LiveLessons.BLL.DTO;
using LiveLessons.BLL.Interfaces;

namespace LiveLesson.WEB.Controllers
{
    [RoutePrefix("api/users")]
    public class UserController : ApiController
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet, Route("")]
        public IHttpActionResult GetAll()
        {
            var usersDto = _userService.GetAll();
            var usersViewModel = _mapper.Map<IEnumerable<UserViewModel>>(usersDto).ToList();

            return Ok(usersViewModel);
        }

        [HttpGet, Route("{id:int}")]
        public IHttpActionResult Get(int id)
        {
                var userDto = _userService.Get(id);
                var userViewModel = _mapper.Map<UserViewModel>(userDto);

                return Ok(userViewModel);
        }

        [HttpPut, Route("")]
        public IHttpActionResult Edit(UserViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                var userDto = _mapper.Map<UserDto>(userViewModel);
                _userService.Edit(userDto);

                return Ok();
            }

            return BadRequest(ModelState);
        }
    }
}