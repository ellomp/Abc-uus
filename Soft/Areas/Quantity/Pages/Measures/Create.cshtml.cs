
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Facade.Quantity;
using Abc.Facade.Quantity;
using Abc.Domain.Quantity;

namespace Soft
{
    public class CreateModel : PageModel
    {
        private readonly IMeasureRepository data;

        public CreateModel(IMeasureRepository r) => data = r; // täpselt sama kui pakka data=r lihtsalt selle meetodi sisse. siin lihtslt lühemalt.

        public IActionResult OnGet() => Page(); //sama kui panna siia meetodi sisse return Page();
  
        [BindProperty]
        public MeasureView MeasureView { get; set; }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            await data.Update(MeasureViewFactory.Create(MeasureView)); 

            return RedirectToPage("./Index");
        }
    }
}
