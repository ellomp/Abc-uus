using Abc.Data.Common;
using Abc.Domain.Common;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Abc.Infra
{
    public abstract class BaseRepository<TDomain, TData> : ICrudMethods<TDomain>
        where TData : PeriodData, new()
        where TDomain : Entity<TData>, new()
    {
        protected internal DbSet<TData> dbSet;
        protected internal DbContext db; 

        protected BaseRepository (DbContext c, DbSet<TData> s)
        {
            db = c;
            dbSet = s;
        }
        public async Task Add(TDomain obj) //async et saaks teha samaegset töötlust, microsofti õpetusetes räägiti sellest pikalt
        {
            if (obj?.Data is null) return;
            dbSet.Add(obj.Data);
            await db.SaveChangesAsync();
        }
        public async Task Delete(string id)
        {
            var d = await dbSet.FindAsync(id); //otsib andmebaasist

            if (d is null) return;
            dbSet.Remove(d);
            await db.SaveChangesAsync();
        }
        public virtual async Task<List<TDomain>> Get()
        {
            var query = CreateSqlQuery(); //teen sql query, ei tea kuidas ag ateeme
            var set = await runSqlQueryAsync(query); //küsin db andmed ja vastavalt sellele queryle  mida teinud olem
            
            return toDomainObjectsList(set); //nii, vii see kõik listi, mis ei ole andmeobj list vaid valdkonna obj list
        }
        internal List<TDomain> toDomainObjectsList(List<TData> set) => set.Select(toDomainObject).ToList();

        protected internal abstract TDomain toDomainObject(TData periodData);
        
        internal async Task<List<TData>> runSqlQueryAsync(IQueryable<TData> query) => await query.AsNoTracking().ToListAsync();
        
        protected internal virtual IQueryable<TData> CreateSqlQuery()
        {
            var query = from s in dbSet select s;
            return query;
        }






        public async Task<TDomain> Get(string id)
        {
            if (id is null) return new TDomain();
            var d = await getData(id); 
            var obj = new TDomain{Data = d};
            return obj; //annan selle data talle tagasi
        }

        protected abstract Task<TData> getData(string id);

        public async Task Update(TDomain obj)
        {
            db.Attach(obj.Data).State = EntityState.Modified;

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