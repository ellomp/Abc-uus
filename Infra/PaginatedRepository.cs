using System;
using System.Collections.Generic;
using System.Text;
using Abc.Domain.Common;

namespace Abc.Infra
{
    public class PaginatedRepository<T> : FilteredRepository<T>, IPaging 
    {
        public int PageIndex { get; set; }
        public bool HasNextPage { get; set; }
        public bool HasPreviousPage { get; set; }
    }
}