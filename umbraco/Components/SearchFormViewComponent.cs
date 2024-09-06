using Microsoft.AspNetCore.Mvc;

using umbraco.Models.Search;

namespace umbraco.Components;

[ViewComponent(Name = "SearchForm")]
public class SearchFormViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(SearchRequestModel model)
    {
 
        return View(model);
    }
}