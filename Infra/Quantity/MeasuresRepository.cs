 using Abc.Domain.Quantity;
 using System.Linq;
using Abc.Data.Quantity;

namespace Abc.Infra.Quantity
{
    public class MeasuresRepository : UniqueEntityRepository<Measure, MeasureData>, IMeasureRepository
    {
        public MeasuresRepository(QuantityDbContext c) : base(c, c.Measures)
        {
        }

        protected internal override Measure toDomainObject(MeasureData d) => new Measure(d);

        protected internal override IQueryable<MeasureData> AddFiltering(IQueryable<MeasureData> set)
        {
            if (string.IsNullOrEmpty(SearchString)) return set;
            return set.Where(s => s.Name.Contains(SearchString)
                                  || s.Code.Contains(SearchString)
                                  || s.Id.Contains(SearchString)
                                  || s.Definition.Contains(SearchString)
                                  || s.ValidFrom.ToString().Contains(SearchString)//et selle järgi saaks filtreerida peame  enne tegema stringiks ses tsee on tegelikult datetime tüüpi muutuja
                                  || s.ValidTo.ToString().Contains(SearchString));
        }
    }
}
 