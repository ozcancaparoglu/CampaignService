using CampaignService.Common.Enums;
using CampaignService.Data.Models;
using System.Threading.Tasks;

namespace CampaignService.Services.GenericAttributeServices
{
    public interface IGenericAttributeService
    {
        #region Db Methods

        /// <summary>
        /// Gets GenericAttribute with model. Automatically find fields create expression with operatorEnum parameter and joins expressions
        /// </summary>
        /// <param name="model">Generic attribute model with values to search on db</param>
        /// <param name="operatorEnum">Operator enum default IsEqualTo</param>
        /// <returns>GenericAttribute Model</returns>
        Task<GenericAttributeModel> GetGenericAttribute(GenericAttributeModel model, FilterOperatorEnum operatorEnum = FilterOperatorEnum.IsEqualTo);

        #endregion
    }
}