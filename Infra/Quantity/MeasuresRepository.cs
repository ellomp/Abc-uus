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

            switch (SortOrder) //kui sortorder on ette antud ja väärtused on olemas, siis db teeb select lause siis sorteerib nt nime järgi ülevalt alla
            {
                case "name_desc":
                    measures = measures.OrderByDescending(s => s.Name);
                    break;
                case "ValidFrom":
                    measures = measures.OrderBy(s => s.ValidFrom);
                    break;
                case "ValidFrom_desc":
                    measures = measures.OrderByDescending(s => s.ValidFrom);
                    break;
                case "ValidTo":
                    measures = measures.OrderBy(s => s.ValidTo);
                    break;
                case "ValidTo_desc":
                    measures = measures.OrderByDescending(s => s.ValidTo);
                    break;
                case "Id":
                    measures = measures.OrderBy(s => s.Id);
                    break;
                case "Id_desc":
                    measures = measures.OrderByDescending(s => s.Id);
                    break;
                case "Code":
                    measures = measures.OrderBy(s => s.Code);
                    break;
                case "Code_desc":
                    measures = measures.OrderByDescending(s => s.Code);
                    break;
                case "Definition":
                    measures = measures.OrderBy(s => s.Definition);
                    break;
                case "Definition_desc":
                    measures = measures.OrderByDescending(s => s.Definition);
                    break;
                default:
                    measures = measures.OrderBy(s => s.Name);
                    break;
            }

            return measures.AsNoTracking(); 
        }
    }
}
