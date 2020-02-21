using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Facade.Quantity
{
    public class MeasureView
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Definition { get; set; }
        [DataType(DataType.Date)]
        [DisplayName("Valid from")]
        public DateTime? ValidFrom { get; set; }
        [DataType(DataType.Date)]
        [DisplayName("Valid to")]
        public DateTime? ValidTo { get; set; }

    }
}
