using umbraco.Models.Search;

namespace umbraco.Services;

public interface ISearchService
{
    public SearchResponseModel Search(SearchRequestModel request);
}
