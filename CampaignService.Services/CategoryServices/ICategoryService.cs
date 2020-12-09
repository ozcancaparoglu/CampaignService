using CampaignService.Data.Domains;
using CampaignService.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CampaignService.Services.CategoryServices
{
    public interface ICategoryService
    {
        ICollection<CampaignModel> FilterCampaignsWithProductCategoryIds(ICollection<ShoppingCartItemModel> shoppingCartItems, ICollection<CampaignModel> modelList);
        Task<ICollection<int>> GetProductCategoryIds(int productId);
    }
}