using Microsoft.AspNetCore.Mvc;

using umbraco.Models.Search;
using umbraco.Models.ViewModels;

namespace umbraco.Components;

[ViewComponent(Name = "PaginationView")]
public class PaginationViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(PaginationViewModel model)
    {
        model??=new PaginationViewModel();
        return View(model);
    }
}