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

        #region Async(Db) Methods

        public async Task<ICollection<CampaignModel>> GetAllActiveCampaigns()
        {
            var now = DateTime.UtcNow;

            var entityList = await campaignRepo.FindAllAsync(x => x.IsActive == true && (x.StartDate <= now && x.EndDate >= now));

            return autoMapper.MapCollection<CampaignService_Campaigns, CampaignModel>(entityList).ToList();
        }

        #endregion

        #region Filter Methods

        private ICollection<CampaignModel> GetActiveCampaignsWithFilters(ICollection<CampaignModel> modelList,
            Func<CampaignModel, bool> predicate, Func<CampaignModel, bool> predicate2 = null)
        {
            var predicateList = modelList.Where(predicate);

            if (predicate2 == null)
                return predicateList.ToList();

            var predicateList2 = modelList.Where(predicate2);

            return predicateList.Union(predicateList2).ToList();
        }

        public ICollection<CampaignModel> GetActiveCampaignsWithCustomerMail(string email, ICollection<CampaignModel> modelList)
        {
            return GetActiveCampaignsWithFilters(modelList,
                x => !string.IsNullOrWhiteSpace(x.Customers) && x.Customers.Contains(email),
                x => string.IsNullOrWhiteSpace(x.Customers));

            //var customerBased = modelList.Where(x => !string.IsNullOrWhiteSpace(x.Customers) && x.Customers.Contains(email));

            //var customerNull = modelList.Where(x => string.IsNullOrWhiteSpace(x.Customers));

            //return customerBased.Union(customerNull).ToList();
        }
        public ICollection<CampaignModel> GetActiveCampaignsWithCustomerMailDomain(string email, ICollection<CampaignModel> modelList)
        {
            string emailDomain = $"@{email.Split('@')[1]}";

            return GetActiveCampaignsWithFilters(modelList,
                x => !string.IsNullOrWhiteSpace(x.CorporateDomainNames) && x.CorporateDomainNames.Contains(emailDomain),
                x => string.IsNullOrWhiteSpace(x.CorporateDomainNames));

            //var customerBased = modelList.Where(x => !string.IsNullOrWhiteSpace(x.CorporateDomainNames) && x.CorporateDomainNames.Contains(emailDomain));

            //var customerNull = modelList.Where(x => string.IsNullOrWhiteSpace(x.CorporateDomainNames));

            //return customerBased.Union(customerNull).ToList();
        }
        public ICollection<CampaignModel> GetActiveCampaignsWithDeviceTypes(string deviceType, ICollection<CampaignModel> modelList)
        {

            return GetActiveCampaignsWithFilters(modelList,
                x => !string.IsNullOrWhiteSpace(x.DeviceTypes) && x.DeviceTypes.Contains(deviceType),
                x => string.IsNullOrWhiteSpace(x.DeviceTypes));

            //var deviceTypeBased = modelList.Where(x => !string.IsNullOrWhiteSpace(x.DeviceTypes) && x.DeviceTypes.Contains(deviceType));

            //var deviceTypeNull = modelList.Where(x => string.IsNullOrWhiteSpace(x.DeviceTypes));

            //return deviceTypeBased.Union(deviceTypeNull).ToList();
        }
        public ICollection<CampaignModel> GetActiveCampaignsWithInstallmentCount(int installmentCount, ICollection<CampaignModel> modelList)
        {
            return GetActiveCampaignsWithFilters(modelList,
                x => x.InstallmentCount > 0 && x.InstallmentCount == installmentCount,
                x => x.InstallmentCount == 0);

            //var installmentCountBased = modelList.Where(x => x.InstallmentCount > 0 && x.InstallmentCount == installmentCount);

            //var installmentCountNull = modelList.Where(x => x.InstallmentCount == 0);

            //return installmentCountBased.Union(installmentCountNull).ToList();
        }
        public ICollection<CampaignModel> GetActiveCampaignsWithCountryId(string countryId, ICollection<CampaignModel> modelList)
        {
            return GetActiveCampaignsWithFilters(modelList,
                x => !string.IsNullOrWhiteSpace(x.CountryIds) && x.CountryIds.Contains(countryId),
                x => string.IsNullOrWhiteSpace(x.CountryIds));

            //var countryIdBased = modelList.Where(x => !string.IsNullOrWhiteSpace(x.CountryIds) && x.CountryIds.Contains(countryId));

            //var countryIdNull = modelList.Where(x => string.IsNullOrWhiteSpace(x.CountryIds));

            //return countryIdBased.Union(countryIdNull).ToList();
        }
        public ICollection<CampaignModel> GetActiveCampaignsWithPickUp(bool pickUp, ICollection<CampaignModel> modelList)
        {

            return GetActiveCampaignsWithFilters(modelList,
                x => x.PickupInStore == pickUp,
                x => x.PickupInStore == false);

            //var pickUpBased = modelList.Where(x => x.PickupInStore == pickUp);

            //var pickUpNull = modelList.Where(x => x.PickupInStore == false);

            //return pickUpBased.Union(pickUpNull).ToList();
        }

        #endregion
    }
}
