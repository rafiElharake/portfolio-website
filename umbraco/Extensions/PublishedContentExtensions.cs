using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Web.Common.PublishedModels;

using static Umbraco.Cms.Core.PropertyEditors.ListViewConfiguration;

namespace umbraco.Extensions;

public static class PublishedContentExtensions
{
    public static Home? GetHome(this IPublishedContent publishedContent)
    {
        return publishedContent.AncestorOrSelf<Home>();
    }
    public static SiteSettings? GetSiteSettings(this IPublishedContent publishedContent)
    {
        var homepage = GetHome(publishedContent);
        return homepage?.FirstChild<SiteSettings>();
    }
    public static string GetMetaTitleOrName(this IPublishedContent publishedContent, string? metaTitle)
    {
        if (!string.IsNullOrWhiteSpace(metaTitle)) return metaTitle;

        return publishedContent.Name;
    }
    public static string? GetSiteName(this IPublishedContent publishedContent)
    {
        var homePage = publishedContent.GetHome();
        if (homePage == null) return null;
        var siteSettings = homePage.GetSiteSettings();
        if (siteSettings == null) return null;
        return siteSettings?.SiteName ?? null;
    }
}