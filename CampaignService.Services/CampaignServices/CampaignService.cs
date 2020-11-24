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
            var now = DateTime.UtcNow;

            var entityList = await campaignRepo.FindAllAsync(x => x.IsActive == true && (x.StartDate <= now && x.EndDate >= now));

            return autoMapper.MapCollection<CampaignService_Campaigns, CampaignModel>(entityList).ToList();
        }

        public async Task<ICollection<CampaignModel>> GetActiveCampaignsWithCustomerMail(string email)
        {
            var entityList = await GetAllActiveCampaigns();

            var customerBased = entityList.Where(x => !string.IsNullOrWhiteSpace(x.Customers) && x.Customers.Contains(email)).ToList();

            var customerNull = entityList.Where(x => string.IsNullOrWhiteSpace(x.Customers)).ToList();

            return customerBased.Union(customerNull).ToList();
        }
        public async Task<ICollection<CampaignModel>> GetActiveCampaignsWithCustomerMailDomain(string email)
        {
            var entityList = await GetAllActiveCampaigns();

            string emailDomain = $"@{email.Split('@')[1]}";

            var customerBased = entityList.Where(x => !string.IsNullOrWhiteSpace(x.CorporateDomainNames) && x.CorporateDomainNames.Contains(emailDomain)).ToList();

            var customerNull = entityList.Where(x => string.IsNullOrWhiteSpace(x.CorporateDomainNames)).ToList();

            return customerBased.Union(customerNull).ToList();
        }
        public async Task<ICollection<CampaignModel>> GetActiveCampaignsWithDeviceTypes(string deviceType)
        {
            var entityList = await GetAllActiveCampaigns();

            var deviceTypeBased = entityList.Where(x => !string.IsNullOrWhiteSpace(x.DeviceTypes) && x.DeviceTypes.Contains(deviceType)).ToList();

            var deviceTypeNull = entityList.Where(x => string.IsNullOrWhiteSpace(x.DeviceTypes)).ToList();

            return deviceTypeBased.Union(deviceTypeNull).ToList();
        }
        public async Task<ICollection<CampaignModel>> GetActiveCampaignsWithInstallmentCount(int installmentCount)
        {
            var entityList = await GetAllActiveCampaigns();

            var installmentCountBased = entityList.Where(x => x.InstallmentCount > 0 && x.InstallmentCount == installmentCount).ToList();

            var installmentCountNull = entityList.Where(x => x.InstallmentCount == 0).ToList();

            return installmentCountBased.Union(installmentCountNull).ToList();
        }
        public async Task<ICollection<CampaignModel>> GetActiveCampaignsWithCountryId(string countryId)
        {
            var entityList = await GetAllActiveCampaigns();

            var countryIdBased = entityList.Where(x => !string.IsNullOrWhiteSpace(x.CountryIds) && x.CountryIds.Contains(countryId)).ToList();

            var countryIdNull = entityList.Where(x => string.IsNullOrWhiteSpace(x.CountryIds)).ToList();

            return countryIdBased.Union(countryIdNull).ToList();
        }
        public async Task<ICollection<CampaignModel>> GetActiveCampaignsWithPickUp(bool pickUp)
        {
            var entityList = await GetAllActiveCampaigns();

            var pickUpBased = entityList.Where(x => x.PickupInStore == pickUp).ToList();

            var pickUpNull = entityList.Where(x => x.PickupInStore == false).ToList();

            return pickUpBased.Union(pickUpNull).ToList();
        }
        #endregion
    }
}
