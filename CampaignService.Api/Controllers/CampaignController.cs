using CampaignService.Common.Models;
using CampaignService.Services.FilterServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace CampaignService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CampaignController : Controller
    {
        private readonly ILogger<CampaignController> logger;
        private readonly IFilterService filterService;

        public CampaignController(ILogger<CampaignController> logger,
            IFilterService filterService)
        {
            this.logger = logger;
            this.filterService = filterService;
        }

        // GET api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var campaignRequest = new CampaignRequest
            {
                CustomerId = 19496140,
                Email = "semaimer34@gmail.com"
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
