using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Abc.Domain.Common;

namespace Abc.Infra
{
    public class BaseRepository<T> : ICrudMethods<T>
    {
        public Task Add(T obj)
        {
            throw new NotImplementedException();
        }

        public Task Delete(string Id)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> Get()
        {
            throw new NotImplementedException();
        }

        public Task<T> Get(string Id)
        {
            throw new NotImplementedException();
        }

        public Task Update(T obj)
        {
            throw new NotImplementedException();
        }
    }
}