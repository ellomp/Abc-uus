using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abc.Data.Common;
using Abc.Domain.Common;
using Microsoft.EntityFrameworkCore;

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
        public async virtual Task<List<TDomain>> Get()
        {
            throw new NotImplementedException();
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