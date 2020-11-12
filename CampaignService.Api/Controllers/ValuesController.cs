using CampaignService.Services.CampaignServices;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CampaignService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private ICampaignService campaignService;

        public ValuesController(ICampaignService campaignService)
        {
            this.campaignService = campaignService;
        }

        // GET api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var data = await campaignService.GetAllActiveCampaigns();
            return Ok(data);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
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
