using CampaignService.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CampaignService.Services.ProductService
{
    public interface IProductService
    {
        ICollection<CampaignModel> FilterCampaignsExcludeProductIds(ICollection<ShoppingCartItemModel> shoppingCartItems, ICollection<CampaignModel> modelList);
        ICollection<CampaignModel> FilterCampaignsExcludeProductSku(ICollection<ShoppingCartItemModel> shoppingCartItems, ICollection<CampaignModel> modelList);
        ICollection<CampaignModel> FilterCampaignsIncludeProductIds(ICollection<ShoppingCartItemModel> shoppingCartItems, ICollection<CampaignModel> modelList);
        ICollection<CampaignModel> FilterCampaignsIncludeProductSku(ICollection<ShoppingCartItemModel> shoppingCartItems, ICollection<CampaignModel> modelList);
        string GetProductSku(int productId);
    }
}
