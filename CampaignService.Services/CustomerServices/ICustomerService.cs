using CampaignService.Data.Models;
using System.Threading.Tasks;

namespace CampaignService.Services.CustomerServices
{
    public interface ICustomerService
    {
        #region Db Methods

        /// <summary>
        /// Gets customer by id
        /// </summary>
        /// <param name="id">Customer id</param>
        /// <returns>Customer Model</returns>
        Task<CustomerModel> GetCustomerById(int id);

        #endregion
    }
}