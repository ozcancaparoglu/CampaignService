using CampaignService.Common.Cache;
using CampaignService.Data.Domains;
using CampaignService.Data.MapperConfiguration;
using CampaignService.Data.Models;
using CampaignService.Logging;
using CampaignService.Repositories;
using CampaignService.UnitOfWorks;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CampaignService.Services.GenericAttributeServices
{
    public class GenericAttributeService : IGenericAttributeService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IAutoMapperConfiguration autoMapper;
        private readonly IRedisCache redisCache;
        private readonly ILoggerManager loggerManager;

        private readonly IGenericRepository<GenericAttribute> genericAttributeRepo;

        public GenericAttributeService(IUnitOfWork unitOfWork, IAutoMapperConfiguration autoMapper, IRedisCache redisCache, ILoggerManager loggerManager)
        {
            this.unitOfWork = unitOfWork;
            this.autoMapper = autoMapper;
            this.redisCache = redisCache;
            this.loggerManager = loggerManager;

            genericAttributeRepo = this.unitOfWork.Repository<GenericAttribute>();
        }

        #region Db Methods

        public async Task<ICollection<GenericAttributeModel>> GetGenericAttribute(GenericAttributeModel model)
        {
            var entityList = await genericAttributeRepo.FindAllAsync(x => x.Key == model.Key 
            || x.KeyGroup == model.KeyGroup 
            || x.EntityId == model.EntityId 
            || x.Value == model.Value 
            || x.StoreId == model.StoreId);

            return autoMapper.MapCollection<GenericAttribute, GenericAttributeModel>(entityList).ToList();

        }

        #endregion
    }
}
