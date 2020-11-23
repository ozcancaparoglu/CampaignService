using CampaignService.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CampaignService.Services.ShoppingCartItemServices
{
    public interface IShoppingCartItemService
    {
        Task<ICollection<ShoppingCartItemModel>> GetShoppingCartItems(int customerId);
    }
}