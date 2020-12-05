﻿using CampaignService.Common.Cache;
using CampaignService.Common.Enums;
using CampaignService.Common.Models;
using CampaignService.Common.Services;
using CampaignService.Data.Domains;
using CampaignService.Data.MapperConfiguration;
using CampaignService.Data.Models;
using CampaignService.Logging;
using CampaignService.Repositories;
using CampaignService.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CampaignService.Services.GenericAttributeServices
{
    public class GenericAttributeService : CommonService, IGenericAttributeService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IAutoMapperConfiguration autoMapper;
        private readonly IRedisCache redisCache;
        private readonly ILoggerManager loggerManager;

        private readonly IGenericRepository<GenericAttribute> genericAttributeRepo;

        public GenericAttributeService(IUnitOfWork unitOfWork, IAutoMapperConfiguration autoMapper, IRedisCache redisCache, ILoggerManager loggerManager)
        {
            this.unitOfWork = unitOfWork;
            this.autoMapper = autoMapper;
            this.redisCache = redisCache;
            this.loggerManager = loggerManager;

            genericAttributeRepo = this.unitOfWork.Repository<GenericAttribute>();
        }

        #region Db Methods

        /// <summary>
        /// Gets Generic Attribute with model.
        /// </summary>
        /// <param name="model">Generic attribute model</param>
        /// <returns>List of Generic Attributes</returns>
        public async Task<GenericAttributeModel> GetGenericAttribute(GenericAttributeModel model)
        {
            var filterModel = new FilterModel { Filters = new List<FilterItem>() };
            var properties = model.GetType().GetProperties();

            foreach (var property in properties)
            {
                var value = property.GetValue(model, null);

                if (value == null || value.ToString() == "0" || value.GetType() == typeof(DateTime))
                    continue;

                filterModel.Filters.Add(new FilterItem
                {
                    Field = property.Name,
                    Operator = FilterOperatorEnum.IsEqualTo,
                    Value = value
                });
            }

            var expression = GenericExpressionBinding<GenericAttribute>(filterModel);

            var entity = await genericAttributeRepo.FindAsync(expression);

            return autoMapper.MapObject<GenericAttribute, GenericAttributeModel>(entity);

        }

        #endregion
    }
}
