using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Abc.Pages.Quantity;
using Abc.Domain.Quantity;
using Abc.Facade.Quantity;

namespace Soft
{
    public class DetailsModel : MeasuresPage
    {
        public DetailsModel(IMeasureRepository r) : base(r) { }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null) return NotFound(); 
             
            Item = MeasureViewFactory.Create(await data.Get(id));

            if (Item == null) return NotFound();
        
            return Page();
        }
    }
}
