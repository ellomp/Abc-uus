using System;
using System.Collections.Generic;
using System.Text;
using Abc.Domain.Common;

namespace Abc.Infra
{
    public class FilteredRepository<T> : SortedRepository<T>, ISearching
    {
        public string SearchString { get; set; }
    }
}