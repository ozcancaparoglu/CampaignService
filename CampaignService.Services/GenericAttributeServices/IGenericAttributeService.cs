using CampaignService.Data.Models;
using System.Threading.Tasks;

namespace CampaignService.Services.GenericAttributeServices
{
    public interface IGenericAttributeService
    {
        Task<GenericAttributeModel> GetByEntityKey(string key, int entityId);
    }
}