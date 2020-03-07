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
    }
}
 