using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options; 
using umbraco.Configuration;
using umbraco.Models;
using umbraco.Models.ViewModels;
using umbraco.Services; 
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.Mail;
using Umbraco.Cms.Core.Models.Email;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Persistence;
using Umbraco.Cms.Web.Website.Controllers;

namespace umbraco.Controllers.Surface;

public class ContactSurfaceController : SurfaceController
{
    private readonly IEmailSender _emailSender;
    private readonly ILogger<ContactSurfaceController> _logger;
    private readonly umbracoConfig _umbracoConfig;
    private readonly IEmailMessageService _emailMessageService;
    private readonly IContentService _contentService;

    public ContactSurfaceController(
        IUmbracoContextAccessor umbracoContextAccessor,
        IUmbracoDatabaseFactory databaseFactory,
        ServiceContext services,
        AppCaches appCaches,
        IProfilingLogger profilingLogger,
        IPublishedUrlProvider publishedUrlProvider,
        IEmailSender emailSender,
        ILogger<ContactSurfaceController> logger,
        IOptions<umbracoConfig> umbracoConfig,
        IEmailMessageService emailMessageService,
        IContentService contentService) : base(umbracoContextAccessor, databaseFactory, services, appCaches, profilingLogger, publishedUrlProvider)
    {
        _emailSender = emailSender;
        _logger = logger;
        _umbracoConfig = umbracoConfig.Value;
        _emailMessageService = emailMessageService;
        _contentService = contentService;

    }
    public async Task<IActionResult> Submit(ContactViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return CurrentUmbracoPage();
        }

        try
        {
            var subject = string.Format("Enquiry from: {0} - {1}", model.Name, model.Email);
             
            var toAddress = "rafiharake4@gmail.com";
            var fromAddress = model.Email;
            var returnmessage = "Thank you for your message. We will get back to you soon.";
            EmailMessage message = new(fromAddress, toAddress, subject, model.Message, false);
            EmailMessage message2 = new(toAddress,fromAddress , subject, returnmessage, false);
            await _emailSender.SendAsync(message, emailType: "Contact");
            await _emailSender.SendAsync(message2, emailType: "Contact");
            EmailLog emailLog = new()
            {
                SenderEmail = fromAddress,
                Text = model.Message,
                SentDate = DateTime.Now
            };
            await _emailMessageService.SaveEmailLogAsync(emailLog); 
            var parentId = 1134;  
            var content = _contentService.Create("Email from " + model.Email, parentId, "emails");
            content.SetValue("text", model.Message);
            _contentService.SaveAndPublish(content);
            TempData["ContactSuccess"] = true; 

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Contact Form Submission Error");
            TempData["ContactSuccess"] = false;
        }

        return RedirectToCurrentUmbracoPage();
    }
}

