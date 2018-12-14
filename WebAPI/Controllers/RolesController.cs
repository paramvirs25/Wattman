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
    public class RolesController : ControllerBase
    {
        private IRoleService _roleService;

        public RolesController(
            IRoleService roleService)
        {
            _roleService = roleService;
        }
       
        /// <summary>
        /// Gets a list of all roles
        /// </summary>
        /// <returns>Returns users roles</returns>
        [Authorize(Policy = Policies.AdminsAndAbove)]
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_roleService.GetAll());
        }
    }
}
