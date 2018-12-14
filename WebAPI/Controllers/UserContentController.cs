using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

using WebApi.Helpers;
using WebApi.Models;
using WebApi.Models.ContentModelExtensions;
using WebApi.Services;
using WebApi.Helpers.Authorization;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserContentController : ControllerBase
    {
        private IUserContentService _userContentService;
        private readonly AppSettings _appSettings;
        private OperatingUser _operatingUser;

        public UserContentController(
            IUserContentService userContentService,
            IOptions<AppSettings> appSettings,
            OperatingUser operatingUser)
        {
            _userContentService = userContentService;
            _appSettings = appSettings.Value;
            _operatingUser = operatingUser;
        }

        /// <summary>
        /// Gets user content list
        /// </summary>
        /// <returns>Returns list of user content</returns>
        [Authorize(Policy = Policies.AgentsAndAbove)]
        [Route("listLoggedIn")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserContentListModel>>> GetListLoggedIn()
        {
            return await _userContentService.GetListByUserId(_operatingUser.GetUserId(HttpContext));
        }

        [HttpPost("updateLoggedIn/{contentId}")]
        [Authorize(Policy = Policies.AgentsAndAbove)]
        public async Task<ActionResult<bool>> UpdateLoggedIn(int contentId)
        {
            return await _userContentService.MarkContentCompleted(_operatingUser.GetUserId(HttpContext), contentId);
        }
    }
}
