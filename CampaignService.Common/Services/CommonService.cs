using CampaignService.Common.Enums;
using CampaignService.Common.Extensions;
using CampaignService.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

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

        protected Expression<Func<T, bool>> GenericExpressionBinding<T>(FilterModel filterModel, ExpressionJoint joint = ExpressionJoint.And) where T : class
        {
            Expression<Func<T, bool>> finalExpression = null;

            foreach (var filter in filterModel.Filters)
            {
                var value = filter.Value;

                Expression<Func<T, bool>> predicate = CreatePredicate<T>(filter.Field, value, filter.Operator);

                if (finalExpression == null)
                    finalExpression = predicate;
                else if (joint == ExpressionJoint.And)
                    finalExpression = ExpressionExtensions.And(finalExpression, predicate);
                else
                    finalExpression = ExpressionExtensions.Or(finalExpression, predicate);
            }
            return finalExpression;
        }

        private Expression<Func<T, bool>> CreatePredicate<T>(string field, object searchValue, FilterOperatorEnum filterOperator) where T : class
        {
            var xType = typeof(T);
            var x = Expression.Parameter(xType, "type");
            var column = xType.GetProperties().FirstOrDefault(p => p.Name.ToLowerInvariant() == field.ToLowerInvariant());

            Expression body = null;

            switch (filterOperator)
            {
                case FilterOperatorEnum.IsEqualTo:
                    body = column == null
                ? (Expression)Expression.Constant(true)
                : Expression.Equal(
                    Expression.PropertyOrField(x, field),
                    Expression.Constant(searchValue));
                    break;
                case FilterOperatorEnum.IsNotEqualTo:
                    body = column == null
                 ? (Expression)Expression.Constant(true)
                 : Expression.NotEqual(
                     Expression.PropertyOrField(x, field),
                     Expression.Constant(searchValue));
                    break;
                case FilterOperatorEnum.StartsWith:
                    body = column == null
                 ? (Expression)Expression.Constant(true)
                 : Expression.Call(Expression.Property(x, column.Name), typeof(string).GetMethod("StartsWith", new[] { typeof(string) }), Expression.Constant(searchValue, typeof(string)));
                    break;
                case FilterOperatorEnum.Contains:
                    body = column == null
                 ? (Expression)Expression.Constant(true)
                 : Expression.Call(Expression.Property(x, column.Name), typeof(string).GetMethod("Contains", new[] { typeof(string) }), Expression.Constant(searchValue, typeof(string)));
                    break;
                case FilterOperatorEnum.DoesNotContain:
                    body = column == null
                ? (Expression)Expression.Constant(true)
                : Expression.Not(Expression.Constant(searchValue, typeof(string)), typeof(string).GetMethod("Contains", new[] { typeof(string) }));
                    break;
                case FilterOperatorEnum.EndsWith:
                    body = column == null
                 ? (Expression)Expression.Constant(true)
                 : Expression.Call(Expression.Property(x, column.Name), typeof(string).GetMethod("EndsWith", new[] { typeof(string) }), Expression.Constant(searchValue, typeof(string)));
                    break;
                case FilterOperatorEnum.IsNull:
                    body = column == null
                ? (Expression)Expression.Constant(true)
                : Expression.Equal(
                    Expression.PropertyOrField(x, field),
                    Expression.Constant(null, typeof(object)));
                    break;
                case FilterOperatorEnum.IsNotNull:
                    body = column == null
                ? (Expression)Expression.Constant(true)
                : Expression.NotEqual(
                    Expression.PropertyOrField(x, field),
                    Expression.Constant(null, typeof(object)));
                    break;
                case FilterOperatorEnum.IsEmpty:
                    body = column == null
                 ? (Expression)Expression.Constant(true)
                 : Expression.Equal(
                     Expression.PropertyOrField(x, field),
                     Expression.Constant(string.Empty, typeof(object)));
                    break;
                case FilterOperatorEnum.IsNotEmpty:
                    body = column == null
              ? (Expression)Expression.Constant(true)
              : Expression.NotEqual(
                  Expression.PropertyOrField(x, field),
                  Expression.Constant(string.Empty, typeof(object)));
                    break;
                case FilterOperatorEnum.HasNoValue:
                    body = column == null
               ? (Expression)Expression.Constant(true)
               : Expression.Equal(
                   Expression.PropertyOrField(x, field),
                   Expression.Constant(null, typeof(object)));
                    break;
                case FilterOperatorEnum.HasValue:
                    body = column == null
              ? (Expression)Expression.Constant(true)
              : Expression.Equal(
                  Expression.PropertyOrField(x, field),
                  Expression.Constant(null, typeof(object)));
                    break;
                case FilterOperatorEnum.Between:
                    body = column == null
               ? (Expression)Expression.Constant(true)
                : Expression.And(Expression.LessThanOrEqual(Expression.PropertyOrField(x, field), 
                Expression.Constant(Convert.ToDecimal(searchValue.ToString().Split(",")[1]), typeof(decimal))),
                Expression.GreaterThanOrEqual(Expression.PropertyOrField(x, field), 
                Expression.Constant(Convert.ToDecimal(searchValue.ToString().Split(",")[0]), typeof(decimal))));
                    break;

                case FilterOperatorEnum.IsEqualToNotNullGuid:
                    body = column == null
              ? (Expression)Expression.Constant(true)
              : Expression.Equal(Expression.PropertyOrField(x, field), 
              Expression.Constant(new Guid(searchValue.ToString()), typeof(Guid)));
                    break;
                case FilterOperatorEnum.IsEqualToGuid:
                    body = column == null
              ? (Expression)Expression.Constant(true)
              : Expression.Equal(Expression.PropertyOrField(x, field), 
              Expression.Constant(new Guid(searchValue.ToString()), typeof(Guid?)));
                    break;
                case FilterOperatorEnum.GreaterThan:
                    body = column == null
                ? (Expression)Expression.Constant(true)
                : Expression.GreaterThan(
                    Expression.PropertyOrField(x, field),
                    Expression.Constant(searchValue));
                    break;
                case FilterOperatorEnum.GreaterThanOrEqual:
                    body = column == null
                ? (Expression)Expression.Constant(true)
                : Expression.GreaterThanOrEqual(
                    Expression.PropertyOrField(x, field),
                    Expression.Constant(searchValue));
                    break;
                default:
                    break;
            }

            return Expression.Lambda<Func<T, bool>>(body, x);
        }

        protected IEnumerable<T> GetWithFilterPredication<T>(ICollection<T> modelList,
           Func<T, bool> predicate, Func<T, T> selector1 = null)
        {
            foreach (var item in modelList)
            {
                var predicateList = modelList.Where(predicate).Select(selector1);
                return predicateList.ToList();
            }
            return null;
        }
    }
}
