using CampaignService.Common.Services;
using CampaignService.Data.Domains;
using CampaignService.Data.MapperConfiguration;
using CampaignService.Data.Models;
using CampaignService.Repositories;
using CampaignService.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CampaignService.Services.CampaignServices
{
    public class CampaignService : CommonService, ICampaignService
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

        #region Async(Db) Methods

        public async Task<ICollection<CampaignModel>> GetAllActiveCampaigns()
        {
            var now = DateTime.UtcNow;

            var entityList = await campaignRepo.FindAllAsync(x => x.IsActive == true && (x.StartDate <= now && x.EndDate >= now));

            return autoMapper.MapCollection<CampaignService_Campaigns, CampaignModel>(entityList).ToList();
        }

        #endregion

        #region Filter Methods

        public ICollection<CampaignModel> GetActiveCampaignsWithCustomerMail(string email, ICollection<CampaignModel> modelList)
        {
            return FilterPredication(modelList,
                x => !string.IsNullOrWhiteSpace(x.Customers) && x.Customers.Contains(email),
                x => string.IsNullOrWhiteSpace(x.Customers));
        }
        public ICollection<CampaignModel> GetActiveCampaignsWithCustomerMailDomain(string email, ICollection<CampaignModel> modelList)
        {
            string emailDomain = $"@{email.Split('@')[1]}";

            return FilterPredication(modelList,
                x => !string.IsNullOrWhiteSpace(x.CorporateDomainNames) && x.CorporateDomainNames.Contains(emailDomain),
                x => string.IsNullOrWhiteSpace(x.CorporateDomainNames));
        }
        public ICollection<CampaignModel> GetActiveCampaignsWithDeviceTypes(string deviceType, ICollection<CampaignModel> modelList)
        {

            return FilterPredication(modelList,
                x => !string.IsNullOrWhiteSpace(x.DeviceTypes) && x.DeviceTypes.Contains(deviceType),
                x => string.IsNullOrWhiteSpace(x.DeviceTypes));
        }
        public ICollection<CampaignModel> GetActiveCampaignsWithInstallmentCount(int installmentCount, ICollection<CampaignModel> modelList)
        {
            return FilterPredication(modelList,
                x => x.InstallmentCount > 0 && x.InstallmentCount == installmentCount,
                x => x.InstallmentCount == 0);
        }
        public ICollection<CampaignModel> GetActiveCampaignsWithCountryId(string countryId, ICollection<CampaignModel> modelList)
        {
            return FilterPredication(modelList,
                x => !string.IsNullOrWhiteSpace(x.CountryIds) && x.CountryIds.Contains(countryId),
                x => string.IsNullOrWhiteSpace(x.CountryIds));

        }
        public ICollection<CampaignModel> GetActiveCampaignsWithPickUp(bool pickUp, ICollection<CampaignModel> modelList)
        {
            return FilterPredication(modelList,
                x => x.PickupInStore == pickUp,
                x => x.PickupInStore == false);
        }

        #endregion
    }
}
