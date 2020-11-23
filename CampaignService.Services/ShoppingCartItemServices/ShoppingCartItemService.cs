using CampaignService.Data.Domains;
using CampaignService.Data.MapperConfiguration;
using CampaignService.Data.Models;
using CampaignService.Repositories;
using CampaignService.UnitOfWorks;
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

        public ShoppingCartItemService(IUnitOfWork unitOfWork, IAutoMapperConfiguration autoMapper)
        {
            this.unitOfWork = unitOfWork;
            this.autoMapper = autoMapper;
            shoppingCartItemRepo = this.unitOfWork.Repository<ShoppingCartItem>();
        }

        #region Methods

        public async Task<ICollection<ShoppingCartItemModel>> GetShoppingCartItems(int customerId)
        {
            var entityList = await shoppingCartItemRepo.FindAllAsync(x => x.CustomerId == customerId);
            return autoMapper.MapCollection<ShoppingCartItem, ShoppingCartItemModel>(entityList).ToList();
        }

        #endregion
    }
}
