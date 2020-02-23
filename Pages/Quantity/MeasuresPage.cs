using Abc.Domain.Quantity;
using Facade.Quantity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Abc.Pages.Quantity
{
    public abstract class MeasuresPage : PageModel
    {
        protected internal readonly IMeasureRepository data; //teised ei näe kui testin?

        protected internal MeasuresPage(IMeasureRepository r) => data = r; // täpselt sama kui pakka data=r lihtsalt selle meetodi sisse. siin lihtslt lühemalt.

        [BindProperty]
        public MeasureView Item { get; set; }
    }
}
