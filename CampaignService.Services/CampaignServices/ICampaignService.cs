using CampaignService.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CampaignService.Services.CampaignServices
{
    public interface ICampaignService
    {
        #region Db Methods
        
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
        ICollection<CampaignModel> FilterCampaignsWithCountryId(string countryId, ICollection<CampaignModel> modelList);
        
        /// <summary>
        /// Active campaigns that can be benefited filter by customer email address
        /// </summary>
        /// <param name="email">Customer email</param>
        /// <param name="modelList">Active campaigns</param>
        /// <returns></returns>
        ICollection<CampaignModel> FilterCampaignsWithCustomerMail(string email, ICollection<CampaignModel> modelList);

        /// <summary>
        /// Active campaigns that can be benefited filter by domain email address
        /// </summary>
        /// <param name="email">Customer email domain</param>
        /// <param name="modelList">Active campaigns</param>
        /// <returns></returns>
        ICollection<CampaignModel> FilterCampaignsWithCustomerMailDomain(string email, ICollection<CampaignModel> modelList);
        
        /// <summary>
        /// Active campaigns that can be benefited filter by device type
        /// </summary>
        /// <param name="deviceType">Device type</param>
        /// <param name="modelList">Active campaigns</param>
        /// <returns></returns>
        ICollection<CampaignModel> FilterCampaignsWithDeviceTypes(string deviceType, ICollection<CampaignModel> modelList);

        /// <summary>
        /// Active campaigns that can be benefited filter by installment count
        /// </summary>
        /// <param name="installmentCount">Installment count</param>
        /// <param name="modelList">Active campaigns</param>
        /// <returns></returns>
        ICollection<CampaignModel> FilterCampaignsWithInstallmentCount(int installmentCount, ICollection<CampaignModel> modelList);

        /// <summary>
        /// Active campaigns that can be benefited filter by pick-up (mağazadan teslimat)
        /// </summary>
        /// <param name="pickUp">Pickup</param>
        /// <param name="modelList">Active campaigns</param>
        /// <returns></returns>
        ICollection<CampaignModel> FilterCampaignsWithPickUp(bool pickUp, ICollection<CampaignModel> modelList);

        /// <summary>
        /// Active campaigns that can be benefited filter by bankname
        /// </summary>
        /// <param name="bankName">Bank name</param>
        /// <param name="modelList">Active campaigns</param>
        /// <returns></returns>
        ICollection<CampaignModel> FilterCampaignsWithBankName(string bankName, ICollection<CampaignModel> filteredCampaigns);

        /// <summary>
        /// Active campaigns that can be benefited filter by credit card bankname
        /// </summary>
        /// <param name="cartbankName">Credit card bankname</param>
        /// <param name="modelList">Active campaigns</param>
        /// <returns></returns>
        ICollection<CampaignModel> FilterCampaignsWithCreditCartBankName(string cartbankName, ICollection<CampaignModel> modelList);

        /// <summary>
        /// Active campaigns that can be benefited filter by payment method
        /// </summary>
        /// <param name="paymentMethodSystemName">Payment method system name</param>
        /// <param name="modelList">Active campaigns</param>
        /// <returns></returns>
        ICollection<CampaignModel> FilterCampaignsWithPaymentMethodSystemName(string paymentMethodSystemName, ICollection<CampaignModel> modelList);

        #endregion

    }
}