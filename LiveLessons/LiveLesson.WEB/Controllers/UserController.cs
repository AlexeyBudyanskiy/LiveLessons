using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using AutoMapper;
using LiveLesson.WEB.ViewModels.User;
using LiveLessons.BLL.DTO;
using LiveLessons.BLL.Interfaces;
using Microsoft.AspNet.Identity;

namespace LiveLesson.WEB.Controllers
{
    /// <summary>
    ///Controller for users manipulations
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    [RoutePrefix("api/users")]
    public class UserController : ApiController
    {
        private readonly IUserService userService;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class.
        /// </summary>
        /// <param name="userService">The user service.</param>
        /// <param name="mapper">The mapper.</param>
        public UserController(IUserService userService, IMapper mapper)
        {
            this.userService = userService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Gets all users.
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("")]
        [Authorize]
        public IHttpActionResult GetAll()
        {
            var usersDto = userService.GetAll();
            var usersViewModel = mapper.Map<IEnumerable<UserViewModel>>(usersDto).ToList();

            return Ok(usersViewModel);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("me")]
        [Authorize]
        public IHttpActionResult Get()
        {
            var profileId = User.Identity.GetUserId();
            var userDto = userService.GetByProfileId(profileId);
            var userViewModel = mapper.Map<UserViewModel>(userDto);

            return Ok(userViewModel);
        }

        /// <summary>
        /// Gets user by specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet, Route("{id:int}")]
        [Authorize]
        public IHttpActionResult Get(int id)
        {
                var userDto = userService.Get(id);
                var userViewModel = mapper.Map<UserViewModel>(userDto);

                return Ok(userViewModel);
        }

        /// <summary>
        /// Edits the specified user view model.
        /// </summary>
        /// <param name="updateUserViewModel">The user view model.</param>
        /// <returns></returns>
        [HttpPut, Route("")]
        [Authorize]
        public IHttpActionResult Edit(UpdateUserViewModel updateUserViewModel)
        {
            var currentUser = userService.GetByProfileId(User.Identity.GetUserId());

            if (currentUser == null || currentUser.Id != updateUserViewModel.Id)
            {
                return StatusCode(HttpStatusCode.Forbidden);
            }

            if (ModelState.IsValid)
            {
                var userDto = mapper.Map<UserDto>(updateUserViewModel);
                userService.Edit(userDto);

                return Ok();
            }

            return BadRequest(ModelState);
        }
    }
}