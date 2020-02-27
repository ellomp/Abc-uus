using System.Threading.Tasks;
using Abc.Domain.Quantity;
using Abc.Facade.Quantity;
using Abc.Pages.Quantity;
using Microsoft.AspNetCore.Mvc;

namespace Abc.Soft.Areas.Quantity.Pages.Measures
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
