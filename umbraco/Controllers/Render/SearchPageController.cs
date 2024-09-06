using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.Controllers;
using umbraco.Models.ContentModels;
using umbraco.Models.Search;
using umbraco.Models.ViewModels;
using umbraco.Services;
using umbraco.Helpers;

namespace umbraco.Controllers.Render;

public class SearchPageController(ILogger<RenderController> logger,
    ICompositeViewEngine compositeViewEngine, IUmbracoContextAccessor umbracoContextAccessor,
    IHttpContextAccessor httpContextAccessor, ISearchService searchService) : RenderController(logger, compositeViewEngine, umbracoContextAccessor)
{

    public override IActionResult Index()
    {
        var _httpContext = httpContextAccessor.HttpContext;
        var query = _httpContext.Request.Query["query"];
        var page=_httpContext.Request.Query["page"];
        var pagesize = 1;

        var _searchrequest = new SearchRequestModel(query, page, pagesize);
        var searchresponse = searchService.Search(_searchrequest);
        var pagination= new PaginationViewModel
        {
            TotalResults = searchresponse.TotalResultCount,
            TotalPages = (int)Math.Ceiling((double)searchresponse.TotalResultCount / _searchrequest.PageSize),
            ResultsPerPage = _searchrequest.PageSize,
            CurrentPage=_searchrequest.Page,
            PaginationUrlFormat = PaginationHelper.GetPaginationUrlFormat(Request.Path, Request?.QueryString.ToString(), page)
        };

        var model = new SearchPageContentModel(CurrentPage)
        {
            SearchRequest = _searchrequest,
            SearchResponse = searchresponse,
            Pagination = pagination
        }; 
        return CurrentTemplate(model);
    }
}
