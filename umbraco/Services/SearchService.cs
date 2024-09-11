using Examine;
using UmbracoConstants = Umbraco.Cms.Core.Constants;
using umbraco.Models.Search;
using Examine.Search;
using Umbraco.Cms.Infrastructure.Examine;
using Umbraco.Cms.Web.Common.PublishedModels;
using Lucene.Net.Analysis.Core;
using umbraco.Extensions;

namespace umbraco.Services;

public class SearchService : ISearchService
{
    private readonly IExamineManager _examineManager;
    private readonly string[] _docTypesToIgnore = [PageTag.ModelTypeAlias, PageTags.ModelTypeAlias, SiteSettings.ModelTypeAlias];
    public SearchService(IExamineManager examineManager)
    {
        _examineManager = examineManager ?? throw new ArgumentNullException(nameof(examineManager));

    }
    public SearchResponseModel Search(SearchRequestModel request)
    {
        if (request == null || !_examineManager.TryGetIndex(
            UmbracoConstants.UmbracoIndexes.ExternalIndexName, out IIndex? index))
        {
            return new SearchResponseModel();
        }
        IBooleanOperation? query = index.Searcher.CreateQuery(IndexTypes.Content)
            .GroupedNot(["umbracoNaviHide"], ["1"])
            .And().GroupedNot(["__NodeTypeAlias"], _docTypesToIgnore);
        string[]? terms = !string.IsNullOrWhiteSpace(request.Query) ? request.Query.Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Where(x => !StopAnalyzer.ENGLISH_STOP_WORDS_SET.Contains(x.ToLower()) && x.Length > 2).ToArray() : null;

        if (terms != null && terms.Length > 0)
        {
            query = query.And().Group(q => q.GroupedOr(["metaTitle"], terms.Boost(80))
            .Or()
            .GroupedOr(["nodeName"], terms.Boost(70))
            .Or()
            .GroupedOr(["mainContent"], terms.Boost(70))
            .Or()
            .GroupedOr(["headerContent"], terms.Boost(70))
            .Or()
            .GroupedOr(["metaTitle"], terms.Fuzzy())
            .Or()
            .GroupedOr(["metaTitle"], terms.MultipleCharacterWildcard())
            .Or()
            .GroupedOr(["nodeName"], terms.Fuzzy())
            .Or()
            .GroupedOr(["nodeName"], terms.MultipleCharacterWildcard())
            .Or()
            .GroupedOr(["metaDescription"], terms.Boost(50))
            .Or()
            .GroupedOr(["headerContent"], terms.Boost(40))
            .Or()
            .GroupedOr(["mainContent"], terms.Boost(40))

            , BooleanOperation.Or);
        }
        ISearchResults? results = query.Execute(new QueryOptions(request.Skip, request.PageSize));

        return new SearchResponseModel(request.Query, results.TotalItemCount, results);
    }

}
