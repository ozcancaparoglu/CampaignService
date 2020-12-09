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
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace CampaignService.Services.CategoryServices
{
    public class CategoryService : CommonService, ICategoryService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IAutoMapperConfiguration autoMapper;
        private readonly IRedisCache redisCache;
        private readonly ILoggerManager loggerManager;
        private readonly IGenericRepository<Product_Category_Mapping> productCategoryMapRepo;

        public CategoryService(IUnitOfWork unitOfWork, IAutoMapperConfiguration autoMapper, IRedisCache redisCache, ILoggerManager loggerManager)
        {
            this.unitOfWork = unitOfWork;
            this.autoMapper = autoMapper;
            this.redisCache = redisCache;
            this.loggerManager = loggerManager;
            productCategoryMapRepo = this.unitOfWork.Repository<Product_Category_Mapping>();
        }

        #region Methods

        public async Task<ICollection<int>> GetProductCategoryIds(int productId)
        {
            var entityList = await productCategoryMapRepo.FindAllAsync(x => x.ProductId == productId);
            return autoMapper.MapCollection<Product_Category_Mapping, ProductCategoryMappingModel>(entityList).Select(s => s.CategoryId).ToList();
        }

        public virtual ICollection<CampaignModel> FilterCampaignsWithProductCategoryIds(ICollection<ShoppingCartItemModel> shoppingCartItems, ICollection<CampaignModel> modelList)
        {
            try
            {
                //var includeProductCategoryCampaign = modelList.Where(s =>  s.BuyConditionCategoriesList.Any(x => x > 0)).ToList();
                //var excludeProductCategoryCampaign = modelList.Where(s => s.BuyConditionCategoriesList != null && s.BuyConditionCategoriesList.Any(x => x < 0)).ToList();

                var filterIncludeCampaignCategory = FilterCampaignsIncludeProductCategoryIds(shoppingCartItems, modelList);
                var filterExcludeCampaignCategory = FilterCampaignsExcludeProductCategoryIds(shoppingCartItems, modelList);

                return filterExcludeCampaignCategory;
            }
            catch (Exception ex)
            {

                throw;
            }
           
        }

        private ICollection<int> FilterCampaignsIncludeProductCategoryIds(ICollection<ShoppingCartItemModel> shoppingCartItems, ICollection<CampaignModel> modelList)
        {
            List<int> excludeCampaignId = new List<int>();
            List<CampaignModel> buyConditionalCategoryList = modelList.Where(x => !string.IsNullOrWhiteSpace(x.BuyConditionCategories)).ToList();
            List<int> list = shoppingCartItems.SelectMany(s => s.Product.ProductCategoryMappingModel.Select(s => s.CategoryId)).ToList();
            if (buyConditionalCategoryList == null || buyConditionalCategoryList.Count == 0)
                return excludeCampaignId;
            foreach (var item in buyConditionalCategoryList)
            {
                var inters = item.BuyConditionCategoriesList.Intersect(list).ToList();
                if (inters.Count == 0)
                    excludeCampaignId.Add(item.Id);
            }
           
            return excludeCampaignId;
        }

        private ICollection<CampaignModel> FilterCampaignsExcludeProductCategoryIds(ICollection<ShoppingCartItemModel> shoppingCartItems, ICollection<CampaignModel> modelList)
        {
            foreach (var shoppingCartItem in shoppingCartItems)
            {
                foreach (var productCategoryId in shoppingCartItem.ProductCategoryIds)
                {
                    return FilterPredication(modelList,
                    x => x.BuyConditionCategoriesList != null && !x.BuyConditionCategoriesList.Contains(-productCategoryId),
                    x => x.BuyConditionCategoriesList != null);
                }

            }
            return null;
        }

        #endregion
    }
}
