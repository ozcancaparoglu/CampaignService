using System;
using System.Collections.Generic;
using System.Linq;

namespace CampaignService.Common.Services
{
    public abstract class CommonService
    {
        /// <summary>
        /// Gets list with two expressions returns new list with two level expressions
        /// </summary>
        /// <typeparam name="T">Type of class</typeparam>
        /// <param name="modelList">List to filter expressions</param>
        /// <param name="predicate">First level of expression</param>
        /// <param name="predicate2">Second level of expression</param>
        /// <returns></returns>
        protected ICollection<T> FilterPredication<T>(ICollection<T> modelList,
           Func<T, bool> predicate, Func<T, bool> predicate2 = null)
        {
            var predicateList = modelList.Where(predicate);

            if (predicate2 == null)
                return predicateList.ToList();

            var predicateList2 = modelList.Where(predicate2);

            return predicateList.Union(predicateList2).ToList();
        }
    }
}
