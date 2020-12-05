using CampaignService.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CampaignService.Services.GenericAttributeServices
{
    public interface IGenericAttributeService
    {
        #region Db Methods

        /// <summary>
        /// Gets Generic Attribute with model.
        /// </summary>
        /// <param name="model">Generic attribute model</param>
        /// <returns>List of Generic Attributes</returns>
        Task<GenericAttributeModel> GetGenericAttribute(GenericAttributeModel model);

        #endregion
    }
}