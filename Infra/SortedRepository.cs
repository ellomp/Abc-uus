using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Abc.Data.Common;
using Abc.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Abc.Infra
{
    public abstract class SortedRepository<TDomain, TData> : BaseRepository<TDomain, TData>, ISorting
        where TData : PeriodData, new()
        where TDomain : Entity<TData>, new()
    {
        public string SortOrder { get; set; }
        public string DescendingString => "_desc";
        protected SortedRepository(DbContext c, DbSet<TData> s) : base(c, s)
        {
        }
        protected internal IQueryable<TData> SetSorting(IQueryable<TData> data)
        {
            var expression = CreateExpression();
            return expression is null ? data : SetOrderBy(data, expression);
        }

        internal Expression<Func<TData, object>> CreateExpression()
        {
            var property = FindProperty();
            return property is null ? null : LambdaExpression(property);
        }
        internal Expression<Func<TData, object>> LambdaExpression(PropertyInfo p)
        {
            var param = Expression.Parameter(typeof(TData));
            var property = Expression.Property(param, p);
            var body = Expression.Convert(property, typeof(object));

            return Expression.Lambda<Func<TData, object>>(body, param);
        }
        internal PropertyInfo FindProperty()
        {
            var name = GetName();
            return typeof(TData).GetProperty(name);
        }
        internal string GetName()
        {
            if (string.IsNullOrEmpty(SortOrder)) return string.Empty;
            var idx = SortOrder.IndexOf(DescendingString, StringComparison.Ordinal);
            if(idx > 0) return SortOrder.Remove(idx);
            return SortOrder;
        }

        internal IQueryable<TData> SetOrderBy(IQueryable<TData> data, Expression<Func<TData, object>> e)
        {
            if (data is null) return null;
            if (e is null) return data;

            try
            {
                return IsDescending() ? data.OrderByDescending(e) : data.OrderBy(e);
            }
            catch
            {
                return data;
            }
        }

        internal bool IsDescending() => !string.IsNullOrEmpty(SortOrder) && SortOrder.EndsWith(DescendingString);
        
    }
}