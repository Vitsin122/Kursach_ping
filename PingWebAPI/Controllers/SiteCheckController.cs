using Microsoft.AspNetCore.Mvc;
using PingWebAPI.ApiModels.Dtos;
using PingWebAPI.Services.Implementations;
using PingWebAPI.Services.Interfaces;

namespace PingWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SiteCheckController : ControllerBase
    {
        private ICheckActivityService _checkActivityService;
        public SiteCheckController(ICheckActivityService checkActivityService)
        {
            _checkActivityService = checkActivityService;
        }

        [HttpPost("CheckSiteStatus")]
        public async Task<ActionResult<SiteWithStatusDto>> CheckSiteStatus(SiteToCheckDto siteToCheck)
        {
            try
            {
                return Ok(await _checkActivityService.CheckSiteActivity(siteToCheck));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("CheckSiteStatusList")]
        public async Task<ActionResult<List<SiteWithStatusDto>>> CheckSiteStatusList(List<SiteToCheckDto> siteToCheck)
        {
            try
            {
                return Ok(await _checkActivityService.CheckSiteActivityList(siteToCheck));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
