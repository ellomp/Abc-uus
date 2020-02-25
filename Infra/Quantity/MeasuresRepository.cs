using Abc.Domain.Quantity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Abc.Data.Quantity;

namespace Abc.Infra.Quantity
{
    public class MeasuresRepository : IMeasureRepository
    {
        private readonly QuantityDbContext db; 
        public string SortOrder { get; set; }

        public string SearchString { get; set; }
        public int PageSize { get; set; } = 1;
        public int PageIndex { get; set; } = 1;
        public bool HasNextPage { get; set; }
        public bool HasPreviousPage { get; set; }


        public MeasuresRepository(QuantityDbContext c)
        {
            db = c;
        }
        public async Task Add(Measure obj) //async et saaks teha samaegset töötlust, microsofti õpetusetes räägiti sellest pikalt
        {
            db.Measures.Add(obj.Data);
            await db.SaveChangesAsync();
        }

        public async Task Delete(string id)
        {
            var d = await db.Measures.FindAsync(id); //otsib andmebaasist

            if (d is null) return;
            db.Measures.Remove(d);
            await db.SaveChangesAsync();
        }

        public async Task<List<Measure>> Get()
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
            IQueryable<MeasureData> measures = from s in db.Measures select s;

            switch (SortOrder) //kui sortorder on ette antud ja väärtused on olemas, siis db teeb select lause siis sorteerib nt nime järgi ülevalt alla
            {
                case "name_desc":
                    measures = measures.OrderByDescending(s => s.Name);
                    break;
                case "Date":
                    measures = measures.OrderBy(s => s.ValidFrom);
                    break;
                case "date_desc":
                    measures = measures.OrderByDescending(s => s.ValidFrom);
                    break;
                default:
                    measures = measures.OrderBy(s => s.Name);
                    break;
            }

            return measures.AsNoTracking(); 
        }

        public async Task<Measure> Get(string id)
        {
            var d = await db.Measures.FirstOrDefaultAsync(m => m.Id == id);
            return new Measure(d); //annan selle data talle tagasi

        }

        public async Task Update(Measure obj)
        {
            var d = await db.Measures.FirstOrDefaultAsync(x => x.Id == obj.Data.Id);
            d.Code = obj.Data.Code;
            d.Name = obj.Data.Name;
            d.Definition = obj.Data.Definition;
            d.ValidFrom = obj.Data.ValidFrom;
            d.ValidTo = obj.Data.ValidTo;
            db.Measures.Update(d);


            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                //if (!MeasureViewExists(MeasureView.Id))
                //{
                //    return NotFound();
                //}
                //else
                //{
                    throw;
                //}
            }
        }
    }
}
