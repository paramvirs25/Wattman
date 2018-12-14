using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

using WebApi.Helpers;
using WebApi.Models;
using WebApi.Models.UserModelExtensions;
using WebApi.Services;
using WebApi.Helpers.Authorization;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using System.Net;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;
        private readonly AppSettings _appSettings;
        private OperatingUser _operatingUser;

        public UsersController(
            IUserService userService,
            IOptions<AppSettings> appSettings,
            OperatingUser operatingUser)
        {
            _userService = userService;
            //_mapper = mapper;
            _appSettings = appSettings.Value;
            _operatingUser = operatingUser;
        }

        /// <summary>
        /// Checks if username and password belongs to a valid user
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /authenticate
        ///     {
        ///        "username": "anyusername",
        ///        "password": "anypassword"
        ///     }
        ///
        /// </remarks>
        /// <param name="userModel"></param>
        /// <returns>Authenticated user details</returns>
        /// <response code="200">Returns authenticated user</response>
        /// <response code="400">If user is not valid</response> 
        [AllowAnonymous]
        [HttpPost("authenticate")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Authenticate([FromBody]UserAuthenticateModel userModel)
        {
            var userDetails = await _userService.Authenticate(userModel);

            string tokenString = JWTAuthentication.GetToken(userDetails, _appSettings.Secret);

            // return basic user info (without password) and token to store client side
            return Ok(new
            {
                userid = userDetails.UserId,
                token = tokenString
            });
        }

        /// <summary>
        /// Get logged in user
        /// </summary>
        /// <returns>Returns details of logged in user</returns>
        [Authorize(Policy = Policies.AgentsAndAbove)]
        [Route("getLoggedIn")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<UserDetailsModel>> GetLoggedIn()
        {
            return await _userService.GetById(_operatingUser.GetUserId(HttpContext));
        }

        /// <summary>
        /// Gets user by Id
        /// </summary>
        /// <param name="id">Id of user to find</param>
        /// <returns>Returns User</returns>
        [Authorize(Policy = Policies.AdminsAndAbove)]
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<UserDetailsModel>> GetById(int id)
        {
            return await _userService.GetById(id);
        }

        /// <summary>
        /// Gets a list of users
        /// </summary>
        /// <returns>Returns list of users</returns>
        [Authorize(Policy = Policies.AdminsAndAbove)]
        [Route("list")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserListModel>>> GetList()
        {
            return await _userService.GetList();
        }

        /// <summary>
        /// Gets data for 'Create' user screen
        /// </summary>
        /// <returns>Returns data for 'Create' user screen</returns>
        [Authorize(Policy = Policies.AdminsAndAbove)]
        [Route("getForCreate")]
        [HttpGet]
        public async Task<ActionResult<UserCreateGetModel>> GetForCreate()
        {
            return await _userService.GetForCreate();
        }

        /// <summary>
        /// Gets user's detail for editing
        /// </summary>
        /// <returns></returns>
        [Authorize(Policy = Policies.AdminsAndAbove)]
        [Route("getForEdit/{id}")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<UserEditGetModel>> GetForEdit(int id)
        {
            return await _userService.GetForEdit(id);
        }

        /// <summary>
        /// Get logged in user's details for editing
        /// </summary>
        /// <returns></returns>
        [Authorize(Policy = Policies.AgentsAndAbove)]
        [Route("getForEditLoggedIn")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<UserEditGetModel>> GetForEditLoggedIn()
        {
            return await _userService.GetForEdit(_operatingUser.GetUserId(HttpContext));
        }

        /// <summary>
        /// Delete User by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Policy = Policies.AdminsAndAbove)]
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            await _userService.Delete(id, _operatingUser.GetUserId(HttpContext));
            return Ok();
        }

        /// <summary>
        /// Creates a user if it already doesnot exits
        /// </summary>
        /// <param name="userCreateSaveModel"></param>
        /// <returns></returns>
        /// <response code="200">If Registratin succeeds</response>
        /// <response code="400">If registration failed</response> 
        [HttpPost("create")]
        [Authorize(Policy = Policies.AdminsAndAbove)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<bool>> Create([FromBody]UserCreateSaveModel userCreateSaveModel)
        {
            return await _userService.Create(userCreateSaveModel, _operatingUser.GetUserId(HttpContext));
        }

        //----Update Methods----

        /// <summary>
        /// Updates any user's login details
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        [HttpPost("update")]
        [Authorize(Policy = Policies.AdminsAndAbove)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<bool>> Update([FromBody]UserModel userModel)
        {
            return await _userService.Update(userModel, _operatingUser.GetUserId(HttpContext));
        }

        /// <summary>
        /// Updates any user's general details
        /// </summary>
        /// <param name="userDetailsBaseAdminModel"></param>
        /// <returns></returns>
        [HttpPost("updateDetail")]
        [Authorize(Policy = Policies.AdminsAndAbove)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<bool>> UpdateDetail([FromBody]UserDetailsBaseAdminModel userDetailsBaseAdminModel)
        {
            return await _userService.UpdateDetail(userDetailsBaseAdminModel, _operatingUser.GetUserId(HttpContext));
        }

        /// <summary>
        /// Updates logged in user login details
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        [HttpPost("updateLoggedIn")]
        [Authorize(Policy = Policies.AgentsAndAbove)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<bool>> UpdateLoggedIn([FromBody]UserModel userModel)
        {
            int operatingUserId = _operatingUser.GetUserId(HttpContext);
            userModel.UserId = operatingUserId;
            return await _userService.Update(userModel, operatingUserId);
        }

        /// <summary>
        /// Updates logged in user's general details
        /// </summary>
        /// <param name="userDetailsBaseModel"></param>
        /// <returns></returns>
        [HttpPost("updateDetailLoggedIn")]
        [Authorize(Policy = Policies.AgentsAndAbove)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<bool>> UpdateDetailLoggedIn([FromBody]UserDetailsBaseModel userDetailsBaseModel)
        {
            int operatingUserId = _operatingUser.GetUserId(HttpContext);
            userDetailsBaseModel.UserId = operatingUserId;
            return await _userService.UpdateDetailForLoggedIn(userDetailsBaseModel, operatingUserId);
        }
    }
}
