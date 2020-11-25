using CampaignService.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CampaignService.Services.ShippingMethodServices
{
    public interface IShippingMethodService
    {
        Task<ICollection<ShippingMethodModel>> GetShippingMethods();
    }
}