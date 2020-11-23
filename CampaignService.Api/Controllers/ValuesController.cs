using CampaignService.Services.CampaignServices;
using CampaignService.Services.ShoppingCartItemServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace CampaignService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : Controller
    {
        private readonly ICampaignService campaignService;
        private readonly IShoppingCartItemService shoppingCartItemService;
        private readonly ILogger<ValuesController> logger;

        public ValuesController(ICampaignService campaignService, IShoppingCartItemService shoppingCartItemService, ILogger<ValuesController> logger)
        {
            this.campaignService = campaignService;
            this.shoppingCartItemService = shoppingCartItemService;
            this.logger = logger;
        }

        // GET api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var data = await campaignService.GetAllActiveCampaigns();
            logger.LogError("hoppala paşam");

            return Ok(data);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var data = await shoppingCartItemService.GetShoppingCartItems(id);
            return Ok(data);
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
