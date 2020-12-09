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
            CreateMap<Product, ProductModel>().MaxDepth(depth).ReverseMap();
            CreateMap<Product_Category_Mapping, ProductCategoryMappingModel>().MaxDepth(depth).ReverseMap();
            CreateMap<Product_Manufacturer_Mapping, ProductManufacturerMappingModel>().MaxDepth(depth).ReverseMap();
            CreateMap<ProductSpecificationAttributeMapping, ProductSpecificationAttributeMappingModel>().MaxDepth(depth).ReverseMap();
            CreateMap<ShoppingCartItem, ShoppingCartItemModel>().MaxDepth(depth).ReverseMap();
            CreateMap<GenericAttribute, GenericAttributeModel>().MaxDepth(depth).ReverseMap();
            CreateMap<ShippingMethod, ShippingMethodModel>().MaxDepth(depth).ReverseMap();
        }
    }
}
