using Abc.Domain.Quantity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using Abc.Facade.Quantity;

namespace Abc.Pages.Quantity
{
    public abstract class MeasuresPage : PageModel
    {
        protected internal readonly IMeasureRepository data; //teised ei näe kui testin?

        protected internal MeasuresPage(IMeasureRepository r)
        {
            data = r;
            PageTitle = "Measures";
        }

        [BindProperty]
        public MeasureView Item { get; set; }
        public IList<MeasureView> Items { get; set; }

        public string CurrentSort { get; set; } = "Sort";
        public string CurrentFilter { get; set; } = "Filer";
        public string PageIndex { get; set; } = "3";
        public string TotalPages { get; set; } = "10";
        public string PageTitle { get; set; }
        public string PageSubTitle { get; set; }
        public string ItemId => Item.Id;



    }
}
