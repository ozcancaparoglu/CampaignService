using System;
using System.Threading.Tasks;

namespace CampaignService.Services.OrderServices
{
    public interface IOrderService
    {
        Task<decimal> GetCustomerOrdersTotalInGivenTime(int customerId, DateTime startDate, DateTime endDate);
    }
}