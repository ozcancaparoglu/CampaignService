using CampaignService.Data.Domains;
using CampaignService.Data.MapperConfiguration;
using CampaignService.Data.Models;
using CampaignService.Repositories;
using CampaignService.UnitOfWorks;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CampaignService.Services.ShippingMethodServices
{
    public class ShippingMethodService : IShippingMethodService
    {
        #region Fields
        private readonly IUnitOfWork unitOfWork;
        private readonly IAutoMapperConfiguration autoMapper;
        private readonly IGenericRepository<ShippingMethod> shippingMethodRepo;
        #endregion

        #region Ctor
        public ShippingMethodService(IUnitOfWork unitOfWork, IAutoMapperConfiguration autoMapper)
        {
            this.unitOfWork = unitOfWork;
            this.autoMapper = autoMapper;
            shippingMethodRepo = this.unitOfWork.Repository<ShippingMethod>();
        }
        #endregion

        #region Methods
        public async Task<ICollection<ShippingMethodModel>> GetShippingMethods()
        {
            var entityList = await shippingMethodRepo.GetAllAsync();
            return autoMapper.MapCollection<ShippingMethod, ShippingMethodModel>(entityList).ToList();
        }

        #endregion

    }
}
