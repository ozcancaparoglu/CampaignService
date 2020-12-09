using CampaignService.Data.Domains;
using CampaignService.Data.MapperConfiguration;
using CampaignService.Data.Models;
using CampaignService.Repositories;
using CampaignService.Services.CategoryServices;
using CampaignService.Services.ProductService;
using CampaignService.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampaignService.Services.ShoppingCartItemServices
{
    public class ShoppingCartItemService : IShoppingCartItemService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IAutoMapperConfiguration autoMapper;
        private readonly IGenericRepository<ShoppingCartItem> shoppingCartItemRepo;
        private readonly ICategoryService categoryService;
        private readonly IProductService productService;


        public ShoppingCartItemService(IUnitOfWork unitOfWork, IAutoMapperConfiguration autoMapper, ICategoryService categoryService, IProductService productService)
        {
            this.unitOfWork = unitOfWork;
            this.autoMapper = autoMapper;
            shoppingCartItemRepo = this.unitOfWork.Repository<ShoppingCartItem>();
            this.categoryService = categoryService;
            this.productService = productService;
        }

        #region Methods

        public async Task<ICollection<ShoppingCartItemModel>> GetShoppingCartItems(int customerId)
        {
            try
            {
                var entityList = shoppingCartItemRepo.Filter(x => x.CustomerId == customerId, null, "Product,Product.Product_Category_Mapping,Product.Product_Manufacturer_Mapping");
                return autoMapper.MapCollection<ShoppingCartItem, ShoppingCartItemModel>(entityList).ToList();
            }
            catch (Exception ex)
            {

                throw;
            }
          
        }

        #endregion
    }
}
