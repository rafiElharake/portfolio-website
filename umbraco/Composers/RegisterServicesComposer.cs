using umbraco.Services;

using Umbraco.Cms.Core.Composing;

namespace umbraco.Composers;

public class RegisterServicesComposer : IComposer
{
    public void Compose(IUmbracoBuilder builder)
    {
        builder.Services.AddTransient<ISearchService, SearchService>();
    }
}
