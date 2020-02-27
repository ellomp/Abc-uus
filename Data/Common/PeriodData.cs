using System;
using Abc.Data.Quantity;

namespace Abc.Data.Common
{
    public abstract class PeriodData 
    {
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
    }
}
