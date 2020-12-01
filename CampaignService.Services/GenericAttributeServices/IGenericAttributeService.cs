using CampaignService.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CampaignService.Services.GenericAttributeServices
{
    public interface IGenericAttributeService
    {
        Task<ICollection<GenericAttributeModel>> GetGenericAttribute(GenericAttributeModel model);
    }
}