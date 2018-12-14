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
    public class ContentController : ControllerBase
    {
        private IContentService _contentService;
        private readonly AppSettings _appSettings;
        private OperatingUser _operatingUser;

        public ContentController(
            IContentService contentService,
            IOptions<AppSettings> appSettings,
            OperatingUser operatingUser)
        {
            _contentService = contentService;
            _appSettings = appSettings.Value;
            _operatingUser = operatingUser;
        }

        /// <summary>
        /// Gets Content by Id
        /// </summary>
        /// <param name="id">Id of Content to find</param>
        /// <returns>Returns content</returns>
        [Authorize(Policy = Policies.AdminsAndAbove)]
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ContentModel>> GetById(int id)
        {
            return await _contentService.GetById(id);
        }

        /// <summary>
        /// Gets all active content list
        /// </summary>
        /// <returns>Returns list of all active content</returns>
        [Authorize(Policy = Policies.AdminsAndAbove)]
        [Route("list")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContentListModel>>> GetList()
        {
            return await _contentService.GetList();
        }

        /// <summary>
        /// Gets data for 'Create' content screen
        /// </summary>
        /// <returns>Returns data for 'Create' content screen</returns>
        [Authorize(Policy = Policies.AdminsAndAbove)]
        [Route("getForCreate")]
        [HttpGet]
        public ActionResult<ContentCreateGetModel> GetForCreate()
        {
            return _contentService.GetForCreate();
        }

        /// <summary>
        /// Gets content's detail for editing
        /// </summary>
        /// <returns></returns>
        [Authorize(Policy = Policies.AdminsAndAbove)]
        [Route("getForEdit/{id}")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ContentEditGetModel>> GetForEdit(int id)
        {
            return await _contentService.GetForEdit(id);
        }

        /// <summary>
        /// Creates a content
        /// </summary>
        /// <param name="createModel"></param>
        /// <returns></returns>
        /// <response code="200"></response>
        /// <response code="400"></response> 
        [HttpPost("create")]
        [Authorize(Policy = Policies.AdminsAndAbove)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<bool>> Create([FromBody]ContentBaseModel createModel)
        {
            return await _contentService.Create(createModel, _operatingUser.GetUserId(HttpContext));
        }

        /// <summary>
        /// Updates any user's login details
        /// </summary>
        /// <param name="updateModel"></param>
        /// <returns></returns>
        [HttpPost("update")]
        [Authorize(Policy = Policies.AdminsAndAbove)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<bool>> Update([FromBody]ContentBaseModel updateModel)
        {
            return await _contentService.Update(updateModel, _operatingUser.GetUserId(HttpContext));
        }

        /// <summary>
        /// Delete content by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Policy = Policies.AdminsAndAbove)]
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            await _contentService.Delete(id, _operatingUser.GetUserId(HttpContext));
            return Ok();
        }
    }
}
