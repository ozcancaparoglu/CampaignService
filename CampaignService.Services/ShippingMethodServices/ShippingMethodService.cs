using CampaignService.Data.Domains;
using CampaignService.Data.MapperConfiguration;
using CampaignService.Data.Models;
using CampaignService.Repositories;
using CampaignService.UnitOfWorks;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

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

        #region FilterMethods
        public ICollection<CampaignModel> GetActiveCampaignsWithShippingMethod(string lastShippingOption, ICollection<CampaignModel> modelList)
        {
            var shippingMethodBase = modelList.Where(x => !string.IsNullOrWhiteSpace(x.SelectedShipmentMethod) && x.SelectedShipmentMethod.Contains(lastShippingOption));

            var shippingMethodNull = modelList.Where(x => string.IsNullOrWhiteSpace(x.SelectedShipmentMethod));

            return shippingMethodBase.Union(shippingMethodNull).ToList();
        }

        #endregion

    }
}
