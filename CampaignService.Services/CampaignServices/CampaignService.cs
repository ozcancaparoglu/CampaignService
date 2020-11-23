using CampaignService.Data.Domains;
using CampaignService.Data.MapperConfiguration;
using CampaignService.Data.Models;
using CampaignService.Repositories;
using CampaignService.UnitOfWorks;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CampaignService.Services.CampaignServices
{
    public class CampaignService : ICampaignService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IAutoMapperConfiguration autoMapper;

        private readonly IGenericRepository<CampaignService_Campaigns> campaignRepo;

        public CampaignService(IUnitOfWork unitOfWork, IAutoMapperConfiguration autoMapper)
        {
            this.unitOfWork = unitOfWork;
            this.autoMapper = autoMapper;

            campaignRepo = this.unitOfWork.Repository<CampaignService_Campaigns>();
        }

        #region Methods

        public async Task<ICollection<CampaignModel>> GetAllActiveCampaigns()
        {
            var entityList = await campaignRepo.FindAllAsync(x => x.IsActive == true);

            return autoMapper.MapCollection<CampaignService_Campaigns, CampaignModel>(entityList).ToList();
            
        }


        #endregion
    }
}
