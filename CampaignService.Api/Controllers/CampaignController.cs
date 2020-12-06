using CampaignService.Common.Models;
using CampaignService.Logging;
using CampaignService.Services.FilterServices;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CampaignService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CampaignController : ControllerBase
    {
        private readonly IFilterService filterService;
        private readonly ILoggerManager loggerManager;

        public CampaignController(IFilterService filterService, ILoggerManager loggerManager)
        {
            this.filterService = filterService;
            this.loggerManager = loggerManager;
        }

        // GET api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            int[] roleIds = { 1,3 };
            var campaignRequest = new CampaignRequest
            {
                CustomerId = 12201763,
                Email = "yanikoglu@superonline.com",
                //CustomerId = 19496140,
                //Email = "semaimer34@gmail.com",
                CustomerRoleIds = roleIds
            };
            var filteredCampaigns = await filterService.FilteredCampaigns(campaignRequest);

            return Ok(filteredCampaigns);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

