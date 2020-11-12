using System.Threading.Tasks;

namespace CampaignService.Common.Cache
{
    public interface IRedisCache
    {
        Task<TItem> GetAsync<TItem>(string key);
        Task SetAsync<TITem>(string key, TITem item, int Time);
        void Remove(string key);
        bool IsCached(string key);
    }
}