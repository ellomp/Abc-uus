 using Abc.Domain.Quantity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Abc.Data.Quantity;

namespace Abc.Infra.Quantity
{
    public class MeasuresRepository : UniqueEntityRepository<Measure, MeasureData>, IMeasureRepository
    {
        public MeasuresRepository(QuantityDbContext c) : base(c, c.Measures)
        {
        }
        public override async Task<List<Measure>> Get()
        {
            var l = await createPaged(createFiltered(createSorted()));
            HasNextPage = l.HasNextPage; //pihol on siil l asemel list
            HasPreviousPage = l.HasPreviousPage; //sama
            return (l.Select(e => new Measure(e))).ToList(); //1. valid kõik 2. teeb ära Measure teisenduse 3. annan listi tagasi.
        }
        private async Task<PaginatedList<MeasureData>> createPaged(IQueryable<MeasureData> dataSet)
        {
            return await PaginatedList<MeasureData>.CreateAsync(
                dataSet, PageIndex, PageSize);
        }
        private IQueryable<MeasureData> createFiltered(IQueryable<MeasureData> set)
        {
            if (string.IsNullOrEmpty(SearchString)) return set;
            return set.Where(s => s.Name.Contains(SearchString)
                                  || s.Code.Contains(SearchString)
                                  || s.Id.Contains(SearchString)
                                  || s.Definition.Contains(SearchString)
                                  || s.ValidFrom.ToString().Contains(SearchString)//et selle järgi saaks filtreerida peame  enne tegema stringiks ses tsee on tegelikult datetime tüüpi muutuja
                                  || s.ValidTo.ToString().Contains(SearchString));
        }
        private IQueryable<MeasureData> createSorted()
        {
            IQueryable<MeasureData> measures = from s in dbSet select s;

            measures = SetSorting(measures);
            return measures.AsNoTracking(); 
        }
    }
}
