using AutoMapper;
using System.Collections.Generic;

namespace CampaignService.Data.MapperConfiguration
{
    public class AutoMapperConfiguration : IAutoMapperConfiguration
    {
        private readonly IMapper mapper;
        private readonly AutoMapper.MapperConfiguration configuration;

        public AutoMapperConfiguration()
        {
            configuration = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.AddMaps("CampaignService.Data");
            });

            mapper = new Mapper(configuration);
        }


        public TReturn MapObject<TMap, TReturn>(TMap obj) where TMap : class where TReturn : class
        {
            return mapper.Map<TMap, TReturn>(obj);
        }

        public IEnumerable<TReturn> MapCollection<TMap, TReturn>(IEnumerable<TMap> expression) where TMap : class where TReturn : class
        {
            return mapper.Map<IEnumerable<TMap>, IEnumerable<TReturn>>(expression);
        }
    }
}
