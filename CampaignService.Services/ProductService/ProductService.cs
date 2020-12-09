using CampaignService.Common.Cache;
using CampaignService.Common.Services;
using CampaignService.Data.Domains;
using CampaignService.Data.MapperConfiguration;
using CampaignService.Data.Models;
using CampaignService.Logging;
using CampaignService.Repositories;
using CampaignService.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CampaignService.Services.ProductService
{
    public class ProductService : CommonService, IProductService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IAutoMapperConfiguration autoMapper;
        private readonly IRedisCache redisCache;
        private readonly ILoggerManager loggerManager;
        private readonly IGenericRepository<Product> productRepo;

        public ProductService(IUnitOfWork unitOfWork, IAutoMapperConfiguration autoMapper, IRedisCache redisCache, ILoggerManager loggerManager)
        {
            this.unitOfWork = unitOfWork;
            this.autoMapper = autoMapper;
            this.redisCache = redisCache;
            this.loggerManager = loggerManager;
            productRepo = this.unitOfWork.Repository<Product>();
        }

        #region Method

        public string GetProductSku(int productId)
        {
            return productRepo.GetById(productId).Sku;
        }

        public virtual ICollection<CampaignModel> FilterCampaignsIncludeProductIds(ICollection<ShoppingCartItemModel> shoppingCartItems, ICollection<CampaignModel> modelList)
        {

            foreach (var shoppingCartItem in shoppingCartItems)
            {
                    return FilterPredication(modelList,
                    x => x.BuyConditionIncludedProductIdList != null && x.BuyConditionIncludedProductIdList.Contains(shoppingCartItem.ProductId),
                    x => x.BuyConditionIncludedProductIdList == null);
            }

            return null;
        }

        public virtual ICollection<CampaignModel> FilterCampaignsExcludeProductIds(ICollection<ShoppingCartItemModel> shoppingCartItems, ICollection<CampaignModel> modelList)
        {

            foreach (var shoppingCartItem in shoppingCartItems)
            {
                return FilterPredication(modelList,
                x => x.BuyConditionExcludedProductIdList != null && !x.BuyConditionExcludedProductIdList.Contains(shoppingCartItem.ProductId),
                x => x.BuyConditionExcludedProductIdList == null);
            }

            return null;
        }

        public virtual ICollection<CampaignModel> FilterCampaignsIncludeProductSku(ICollection<ShoppingCartItemModel> shoppingCartItems, ICollection<CampaignModel> modelList)
        {

            foreach (var shoppingCartItem in shoppingCartItems)
            {
                return FilterPredication(modelList,
                x => !string.IsNullOrEmpty(x.BuyConditionIncludedProductSkus) && x.BuyConditionIncludedProductSkus.Contains(shoppingCartItem.ProductSku),
                x => string.IsNullOrEmpty(x.BuyConditionIncludedProductSkus));
            }

            return null;
        }

        public virtual ICollection<CampaignModel> FilterCampaignsExcludeProductSku(ICollection<ShoppingCartItemModel> shoppingCartItems, ICollection<CampaignModel> modelList)
        {

            foreach (var shoppingCartItem in shoppingCartItems)
            {
                return FilterPredication(modelList,
                x => !string.IsNullOrEmpty(x.BuyConditionIncludedProductSkus) && !x.BuyConditionExcludedProductIdList.Contains(shoppingCartItem.ProductId),
                x => string.IsNullOrEmpty(x.BuyConditionIncludedProductSkus));
            }

            return null;
        }

        #endregion

    }
}
