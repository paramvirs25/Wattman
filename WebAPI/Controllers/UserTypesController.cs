using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

using WebApi.Helpers;
using WebApi.Models;
using WebApi.Services;
using WebApi.Helpers.Authorization;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserTypesController : ControllerBase
    {
        private IUserTypeService _userTypeService;

        public UserTypesController(
            IUserTypeService userTypeService)
        {
            _userTypeService = userTypeService;
        }
       
        /// <summary>
        /// Gets a list of all user types
        /// </summary>
        /// <returns>Returns user types</returns>
        [Authorize(Policy = Policies.AdminsAndAbove)]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _userTypeService.GetAll());
        }
    }
}
