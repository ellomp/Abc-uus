using System;
using System.Linq;
using Abc.Data.Common;
using Abc.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Abc.Infra
{
    public abstract class PaginatedRepository<TDomain, TData> : FilteredRepository<TDomain, TData>, IPaging
        where TData : PeriodData, new()
        where TDomain : Entity<TData>, new()
    {
        public int PageIndex { get; set; }
        public int TotalPages => GetTotalPages(PageSize);
        public bool HasNextPage => PageIndex < TotalPages;
        public bool HasPreviousPage => PageIndex > 1;
        public int PageSize { get; set; } = 5; //mitu erinevatc nt aeg, mass jne tüüpi korraga lehel nähtav on

        protected PaginatedRepository(DbContext c, DbSet<TData> s) : base(c, s)
        {
        }

        internal int GetTotalPages(in int pageSize)
        {
            var count = GetItemsCount();
            var pages = CountTotalPages(count, pageSize);

            return pages;
        }

        internal int CountTotalPages(int count, in int pageSize)
        {
            return (int)Math.Ceiling(count / (double)pageSize);

        }

        internal int GetItemsCount()
        {
            var query = base.createSqlQuery();
            return query.CountAsync().Result;
        }

        protected internal override IQueryable<TData> createSqlQuery()
        {
            var query = base.createSqlQuery();
            query = AddSkipAndTake(query);
            return query;
        }

        private IQueryable<TData> AddSkipAndTake(IQueryable<TData> query)
        {
            var q = query.Skip(
                    (PageIndex - 1) * PageSize)
                .Take(PageSize);

            return q;
        }
    }
}