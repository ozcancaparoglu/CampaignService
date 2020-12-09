using CampaignService.Common.Models;
using CampaignService.Data.Models;
using CampaignService.Services.CampaignServices;
using CampaignService.Services.CategoryServices;
using CampaignService.Services.ProductService;
using CampaignService.Services.ShippingMethodServices;
using CampaignService.Services.ShoppingCartItemServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CampaignService.Services.FilterServices
{
    public class FilterService : IFilterService
    {
        #region Fields
        private readonly ICampaignService campaignService;
        private readonly IShippingMethodService shippingMethodService;
        private readonly ICategoryService categoryService;
        private readonly IShoppingCartItemService shoppingCartItemService;
        private readonly IProductService productService;
        #endregion

        #region Ctor
        public FilterService(ICampaignService campaignService, IShippingMethodService shippingMethodService, ICategoryService categoryService, IShoppingCartItemService shoppingCartItemService, IProductService productService)
        {
            this.campaignService = campaignService;
            this.shippingMethodService = shippingMethodService;
            this.categoryService = categoryService;
            this.shoppingCartItemService = shoppingCartItemService;
            this.productService = productService;
        }
        #endregion

        #region Methods

        public async Task<ICollection<CampaignModel>> FilteredCampaigns(CampaignRequest request)
        {
            var filteredCampaigns = await campaignService.GetAllActiveCampaigns();

            filteredCampaigns = campaignService.FilterCampaignsWithCustomerMail(request.Email, filteredCampaigns);
            filteredCampaigns = campaignService.FilterCampaignsWithCustomerMailDomain(request.Email, filteredCampaigns);
            filteredCampaigns = campaignService.FilterCampaignsWithDeviceTypes(request.DeviceType.ToString(), filteredCampaigns);
            filteredCampaigns = campaignService.FilterCampaignsWithInstallmentCount(request.InstallmentCount, filteredCampaigns);
            filteredCampaigns = campaignService.FilterCampaignsWithPickUp(request.PickupInStore, filteredCampaigns);
            
            filteredCampaigns = campaignService.FilterCampaignsWithBankName(request.BankName, filteredCampaigns);
            filteredCampaigns = campaignService.FilterCampaignsWithCreditCartBankName(request.CardBankName, filteredCampaigns);
            filteredCampaigns = campaignService.FilterCampaignsWithPaymentMethodSystemName(request.PaymentMethodSystemName, filteredCampaigns);

            filteredCampaigns = shippingMethodService.FilterCampaignsWithShippingMethod(request.LastShippingOption, filteredCampaigns);

            var shoppingCarItems = await shoppingCartItemService.GetShoppingCartItems(request.CustomerId);

            filteredCampaigns = categoryService.FilterCampaignsWithProductCategoryIds(shoppingCarItems, filteredCampaigns);
            filteredCampaigns = productService.FilterCampaignsIncludeProductIds(shoppingCarItems, filteredCampaigns);
            filteredCampaigns = productService.FilterCampaignsExcludeProductIds(shoppingCarItems, filteredCampaigns);
            filteredCampaigns = productService.FilterCampaignsIncludeProductSku(shoppingCarItems, filteredCampaigns);
            filteredCampaigns = productService.FilterCampaignsExcludeProductSku(shoppingCarItems, filteredCampaigns);

            return filteredCampaigns;

        }

        #endregion


        #region Validations

        #endregion
    }
}
