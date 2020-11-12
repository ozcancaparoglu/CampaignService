using CampaignService.Common.Entities;
using CampaignService.Repositories;
using System.Threading.Tasks;

namespace CampaignService.UnitOfWorks
{
    public interface IUnitOfWork
    {
        IGenericRepository<T> Repository<T>() where T : EntityBase;
        int Commit();
        Task<int> CommitAsync();
        void Rollback();
    }
}