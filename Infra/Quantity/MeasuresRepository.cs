using Abc.Domain.Quantity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Abc.Infra.Quantity
{
    public class MeasuresRepository : IMeasureRepository
    {
        public Task Add(Measure obj)
        {
            throw new System.NotImplementedException();
        }

        public Task Delete(string Id)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<Measure>> Get()
        {
            throw new System.NotImplementedException();
        }

        public Task<Measure> Get(string Id)
        {
            throw new System.NotImplementedException();
        }

        public Task Update(Measure obj)
        {
            throw new System.NotImplementedException();
        }
    }
}
