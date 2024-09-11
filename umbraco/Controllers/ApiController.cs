using Microsoft.AspNetCore.Mvc;

using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Web.Common.Controllers;

public class CustomContentController : UmbracoApiController
{
    private readonly IContentService _contentService;

    public CustomContentController(IContentService contentService)
    {
        _contentService = contentService;
    }

    [HttpPost]
    public IActionResult UpdateContent(int contentId, string newTitle)
    {
        // Step 2: Retrieve the Content Node
        var content = _contentService.GetById(contentId);
        if (content == null)
        {
            return NotFound();
        }
         
        content.SetValue("title", newTitle);

        // Step 4: Save and Publish the Content Node
        _contentService.SaveAndPublish(content);

        return Ok();
    }
}
