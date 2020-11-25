using CampaignService.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CampaignService.Services.CampaignServices
{
    public interface ICampaignService
    {
        #region Async(Db) Methods
        
        /// <summary>
        /// Gets all active and valid campaigns
        /// </summary>
        /// <returns></returns>
        Task<ICollection<CampaignModel>> GetAllActiveCampaigns();
        
        #endregion

        #region Filter Methods

        /// <summary>
        /// Active campaigns that can be benefited filter by country
        /// </summary>
        /// <param name="countryId">Country id</param>
        /// <param name="modelList">Active campaigns</param>
        /// <returns></returns>
        ICollection<CampaignModel> GetActiveCampaignsWithCountryId(string countryId, ICollection<CampaignModel> modelList);
        
        /// <summary>
        /// Active campaigns that can be benefited filter by customer email address
        /// </summary>
        /// <param name="email">Customer email</param>
        /// <param name="modelList">Active campaigns</param>
        /// <returns></returns>
        ICollection<CampaignModel> GetActiveCampaignsWithCustomerMail(string email, ICollection<CampaignModel> modelList);

        /// <summary>
        /// Active campaigns that can be benefited filter by domain email address
        /// </summary>
        /// <param name="email">Customer email domain</param>
        /// <param name="modelList">Active campaigns</param>
        /// <returns></returns>
        ICollection<CampaignModel> GetActiveCampaignsWithCustomerMailDomain(string email, ICollection<CampaignModel> modelList);

        /// <summary>
        /// Active campaigns that can be benefited filter by device type
        /// </summary>
        /// <param name="deviceType">Device type</param>
        /// <param name="modelList">Active campaigns</param>
        /// <returns></returns>
        ICollection<CampaignModel> GetActiveCampaignsWithDeviceTypes(string deviceType, ICollection<CampaignModel> modelList);

        /// <summary>
        /// Active campaigns that can be benefited filter by installment count
        /// </summary>
        /// <param name="installmentCount">Installment count</param>
        /// <param name="modelList">Active campaigns</param>
        /// <returns></returns>
        ICollection<CampaignModel> GetActiveCampaignsWithInstallmentCount(int installmentCount, ICollection<CampaignModel> modelList);

        /// <summary>
        /// Active campaigns that can be benefited filter by pick-up (mağazadan teslimat)
        /// </summary>
        /// <param name="pickUp">Pickup</param>
        /// <param name="modelList">Active campaigns</param>
        /// <returns></returns>
        ICollection<CampaignModel> GetActiveCampaignsWithPickUp(bool pickUp, ICollection<CampaignModel> modelList);
        
        #endregion

    }
}