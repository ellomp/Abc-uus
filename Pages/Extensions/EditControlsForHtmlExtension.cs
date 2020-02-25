 using System;
using System.Collections.Generic;
 using System.Linq.Expressions;
 using Microsoft.AspNetCore.Html;
 using Microsoft.AspNetCore.Mvc.Rendering;

 namespace Abc.Pages.Extensions
{
    public static class EditControlsForHtmlExtension //peab kõikidel asjadel olema nime lõpus
    {
        public static IHtmlContent EditControlsFor<TClassType, TPropertyType>(
            this IHtmlHelper<TClassType> htmlHelper, Expression<Func<TClassType, TPropertyType>> expression) //Expression<> on argument, mis on nagu funtsioon mida see extenstion kasutab kui ta saab
        {
            var s = htmlString(htmlHelper, expression);
            return new HtmlContentBuilder(s);
        }

        internal static List<object> htmlString<TClassType, TPropertyType>(
            IHtmlHelper<TClassType> htmlHelper, Expression<Func<TClassType, 
                TPropertyType>> expression) 
        {
            return new List<object> {
                new HtmlString("<div class=\"form-group\">"),
                htmlHelper.LabelFor(expression, new {@class = "text-dark"}),
                htmlHelper.EditorFor(expression, new {htmlAttributes = new {@class = "form-control"}}),
                htmlHelper.ValidationMessageFor(expression, "", new {@class = "text-danger"}),
                new HtmlString("</div>")
            };
        }
    }
}
 //<div class="form-group">
 //    <label asp-for="Movie.Title" class="control-label"></label>
 //    <input asp-for="Movie.Title" class="form-control" />
 //    <span asp-validation-for="Movie.Title" class="text-danger"></span>
 //</div>