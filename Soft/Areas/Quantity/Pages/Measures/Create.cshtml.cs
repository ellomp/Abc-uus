
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Facade.Quantity;
using Abc.Facade.Quantity;
using Abc.Domain.Quantity;
using Abc.Pages.Quantity;

namespace Soft
{
    public class CreateModel : MeasuresPage
    {
        public CreateModel(IMeasureRepository r) : base(r) { }
        public IActionResult OnGet() => Page(); //sama kui panna siia meetodi sisse return Page();
 
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page(); //kontrollib, kas mudel on õigesti täidetud, nt panid MeasureView.cs kirja et Name või Id on [Required], 
            //siin vaatabki, et kas kasutaja sisestas required asjad.
            await data.Update(MeasureViewFactory.Create(Item));      //kui õigesti siis teeb update ja näitame listi

            return RedirectToPage("./Index");
        }



    }
}
