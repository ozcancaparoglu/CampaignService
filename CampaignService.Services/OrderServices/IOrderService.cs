using CampaignService.Data.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CampaignService.Services.OrderServices
{
    public interface IOrderService
    {
        #region Db Methods
        int GetCustomerOrderCount(int customerId);
        Task<decimal> GetCustomerOrdersTotalInGivenTime(int customerId, DateTime startDate, DateTime endDate);
        #endregion

        #region Filter Methods
        ICollection<CampaignModel> FilterRestrictedNthOrder(int customerId, ICollection<CampaignModel> modelList);
        #endregion
    }
}