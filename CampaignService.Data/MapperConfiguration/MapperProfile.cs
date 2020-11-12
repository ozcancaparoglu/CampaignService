using AutoMapper;
using CampaignService.Data.Domains;
using CampaignService.Data.Models;

namespace CampaignService.Data.MapperConfiguration
{
    public class MapperProfile : Profile
    {
        private readonly int depth = 5;

        public MapperProfile()
        {
            CreateMap<CampaignService_Campaigns, CampaignModel>().MaxDepth(depth).ReverseMap();
            CreateMap<CampaignService_CampaignCouponCode, CampaignCouponCodeModel>().MaxDepth(depth).ReverseMap();
            CreateMap<CampaignService_CampaignCouponUsage, CampaignCouponUsageModel>().MaxDepth(depth).ReverseMap();
            CreateMap<CampaignService_CampaignFilter, CampaignFilterModel>().MaxDepth(depth).ReverseMap();
            CreateMap<CampaignService_CampaignUsageHistory,CampaignUsageHistoryModel>().MaxDepth(depth).ReverseMap();
            CreateMap<CampaignService_Log, CampaignLogModel>().MaxDepth(depth).ReverseMap();
        }
    }
}
