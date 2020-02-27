using System.Collections.Generic;
using System.Threading.Tasks;
using Abc.Domain.Quantity;
using Abc.Facade.Quantity;
using Abc.Pages.Quantity;

namespace Abc.Soft.Areas.Quantity.Pages.Measures
{
    public class IndexModel : MeasuresPage
    {
        public IndexModel(IMeasureRepository r) : base(r) { }
 
        public async Task OnGetAsync(string sortOrder,
            string currentFilter, string searchString, int? pageIndex)
        {
            CurrentSort = sortOrder;
            //esimesed 2 rid selleks et saaks sorteerida measuerite nimekirja browseris
            NameSort = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            DateSort = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            CurrentFilter = searchString;
            data.SortOrder = sortOrder;
            SearchString = CurrentFilter;
            data.SearchString = SearchString;
            data.PageIndex = pageIndex?? 1; //kui see page index on 0 siis tee 1ks
            PageIndex = data.PageIndex;
            var l = await data.Get(); //annab listi
            Items = new List<MeasureView>();
            foreach(var e in l) { Items.Add(MeasureViewFactory.Create(e)); }

            HasNextPage = data.HasNextPage;
            HasPreviousPage = data.HasPreviousPage;
        }

        public string CurrentSort { get; set; }
        public string DateSort { get; set; }
        public string NameSort { get; set; }

        public bool HasPreviousPage { get; set; }
        public bool HasNextPage { get; set; }
        public int PageIndex { get; set; }

        public string SearchString;
        public string CurrentFilter { get; set; }
    }
}
 