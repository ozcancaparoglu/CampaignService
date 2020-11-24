using CampaignService.Data.Domains;
using CampaignService.Data.MapperConfiguration;
using CampaignService.Data.Models;
using CampaignService.Repositories;
using CampaignService.UnitOfWorks;
using System.Threading.Tasks;

namespace CampaignService.Services.GenericAttributeServices
{
    public class GenericAttributeService : IGenericAttributeService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IAutoMapperConfiguration autoMapper;

        private readonly IGenericRepository<GenericAttribute> genericAttributeRepo;

        public GenericAttributeService(IUnitOfWork unitOfWork, IAutoMapperConfiguration autoMapper)
        {
            this.unitOfWork = unitOfWork;
            this.autoMapper = autoMapper;
            genericAttributeRepo = this.unitOfWork.Repository<GenericAttribute>();
        }
        public async Task<GenericAttributeModel> GetByEntityKey(string key, int entityId)
        {
            var entity = await genericAttributeRepo.FindAsync(x => x.Key == key && x.EntityId == entityId);
            return autoMapper.MapObject<GenericAttribute, GenericAttributeModel>(entity);
        }
        private async Task<GenericAttributeModel> GetGenericAttributeByName(string key, int entityId, string keyGroup)
        {
            var entity = await genericAttributeRepo.FindAsync(x => x.EntityId == entityId && x.Key == key && x.KeyGroup == keyGroup);
            return autoMapper.MapObject<GenericAttribute, GenericAttributeModel>(entity);
        }
    }
}
