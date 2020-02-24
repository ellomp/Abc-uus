using Abc.Data.Quantity;
using Abc.Domain.Quantity;
using Facade.Quantity;
using System;

namespace Abc.Facade.Quantity
{
    public static class MeasureViewFactory //staatiliste klasside puhul ei ole klassi päritavad,
    {
        public static Measure Create(MeasureView v)
        {
            var d = new MeasureData
            {
                Id = v.Id, //võtab info viewst (v)
                Name = v.Name,
                Code = v.Code,
                Definition = v.Definition,
                ValidFrom = v.ValidFrom,
                ValidTo = v .ValidTo
            };
            return new Measure(d); 
            throw new NotImplementedException();
        }
        public static MeasureView Create(Measure o)
        {
            var v = new MeasureView
            {
                Id = o.Data.Id, //võtab info datast (d)
                Name = o.Data.Name,
                Code = o.Data.Code,
                Definition = o.Data.Definition,
                ValidFrom = o.Data.ValidFrom,
                ValidTo = o.Data.ValidTo
            };
            return v;
        }
    }
}
