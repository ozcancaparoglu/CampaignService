using CampaignService.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CampaignService.Services.ShippingMethodServices
{
    public interface IShippingMethodService
    {
        /// <summary>
        /// Returns all active campaigns with shipping method
        /// </summary>
        /// <param name="lastShippingOption"></param>
        /// <param name="modelList"></param>
        /// <returns></returns>
        ICollection<CampaignModel> FilterCampaignsWithShippingMethod(string lastShippingOption, ICollection<CampaignModel> modelList);

        /// <summary>
        /// Returns shipping methods
        /// </summary>
        /// <returns></returns>
        Task<ICollection<ShippingMethodModel>> GetShippingMethods();
    }
}