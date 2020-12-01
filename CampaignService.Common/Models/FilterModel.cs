using CampaignService.Common.Enums;
using System;
using System.Collections.Generic;

namespace CampaignService.Common.Models
{
    //public sealed class FilterModel
    //{
    //    private FilterModel()
    //    {
    //    }
    //    private static FilterModel instance = null;

    //    public List<FilterItem> Filters { get; set; }

    //    public static FilterModel Instance
    //    {
    //        get
    //        {
    //            if (instance == null)
    //            {
    //                instance = new FilterModel() { Filters = new List<FilterItem>() };
    //            }
    //            return instance;
    //        }
    //    }
    //}

    public class FilterModel
    {
        public int Skip { get; set; }
        public int Take { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public List<FilterItem> Filters { get; set; }
    }

    public class FilterItem
    {
        public string Field { get; set; }
        public FilterOperatorEnum Operator { get; set; }
        public object Value { get; set; }
    }

    
}
