using System.Collections.Generic;
using System.Threading.Tasks;
using Facade.Quantity;
using Abc.Pages.Quantity;
using Abc.Domain.Quantity;
using Abc.Facade.Quantity;

namespace Soft
{
    public class IndexModel : MeasuresPage
    {
        public IndexModel(IMeasureRepository r) : base(r) { }
 
        public async Task OnGetAsync()
        {
            var l = await data.Get();
            Items = new List<MeasureView>();
            foreach(var e in l) { Items.Add(MeasureViewFactory.Create(e)); }
        }
    }
}
