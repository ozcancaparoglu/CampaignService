using CampaignService.Common.Models;
using CampaignService.Data.Models;
using CampaignService.Services.CampaignFilterServices;
using CampaignService.Services.CampaignServices;
using CampaignService.Services.CategoryServices;
using CampaignService.Services.ProductService;
using CampaignService.Services.ShippingMethodServices;
using CampaignService.Services.ShoppingCartItemServices;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using CampaignService.Services.CampaignUsageHistoryServices;
using CampaignService.Services.OrderServices;

namespace CampaignService.Services.FilterServices
{
    public class FilterService : IFilterService
    {
        #region Fields
        private readonly ICampaignService campaignService;
        private readonly IShippingMethodService shippingMethodService;
        private readonly ICampaignFilterService campaignFilterService;
        private readonly ICampaignUsageHistoryService campaignUsageHistoryService;
        private readonly IOrderService orderService;
        private readonly ICategoryService categoryService;
        private readonly IShoppingCartItemService shoppingCartItemService;
        private readonly IProductService productService;
        #endregion

        #region Ctor
        public FilterService(ICampaignService campaignService, 
            IShippingMethodService shippingMethodService, 
            ICampaignFilterService campaignFilterService,
            ICampaignUsageHistoryService campaignUsageHistoryService,
            IOrderService orderService,
             ICategoryService categoryService, 
             IShoppingCartItemService shoppingCartItemService, 
             IProductService productService)
        {
            this.campaignService = campaignService;
            this.shippingMethodService = shippingMethodService;
            this.campaignFilterService = campaignFilterService;
            this.campaignUsageHistoryService = campaignUsageHistoryService;
            this.orderService = orderService;
            this.categoryService = categoryService;
            this.shoppingCartItemService = shoppingCartItemService;
            this.productService = productService;
        }
        #endregion

        #region Methods

        public async Task<ICollection<CampaignModel>> FilteredCampaigns(CampaignRequest request)
        {
            var filteredCampaigns = await campaignService.GetAllActiveCampaigns();

            filteredCampaigns = campaignUsageHistoryService.FilterCampaignsWithUsageHistory(request.CustomerId, filteredCampaigns);
            filteredCampaigns = campaignFilterService.FilterCampaignsWithCampaignFilter(request.CustomerId, filteredCampaigns);
            filteredCampaigns = orderService.FilterRestrictedNthOrder(request.CustomerId, filteredCampaigns);
            filteredCampaigns = campaignUsageHistoryService.FilterCampaignsWithUsageHistory(request.CustomerId, filteredCampaigns);

            filteredCampaigns = campaignService.FilterCampaignsWithCustomerRoleId(request.CustomerRoleIds.ToList(), filteredCampaigns);

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
