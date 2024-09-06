using Umbraco.Cms.Core.Models;

namespace umbraco.Extensions;

public static class MediaWithCropsExtensions
{
    public static string GetAltText(this MediaWithCrops mediaItem, string Alt = "alt")
    {
        var altText = mediaItem.Value<string>("altText");
        if (string.IsNullOrWhiteSpace(altText)) return string.Empty;
        return altText;
    }

}