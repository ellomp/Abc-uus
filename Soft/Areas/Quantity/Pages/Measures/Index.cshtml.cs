using System;
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
        public string SearchString;
        public IndexModel(IMeasureRepository r) : base(r) { }
 
        public async Task OnGetAsync(string sortOrder, string searchString)
        {
            //esimesed 2 rid selleks et saaks sorteerida measuerite nimekirja browseris
            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            DateSort = sortOrder == "Date" ? "date_desc" : "Date";
            data.SortOrder = sortOrder;
            SearchString = searchString;
            data.SearchString = SearchString;
            var l = await data.Get();
            Items = new List<MeasureView>();
            foreach(var e in l) { Items.Add(MeasureViewFactory.Create(e)); }
        }

        public string DateSort { get; set; }

        public string NameSort { get; set; }
    }
}
 