using System.Data; 
using Microsoft.EntityFrameworkCore; 
using Slimsy.DependencyInjection; 
using umbraco;
using umbraco.Configuration;
using System.Data.SQLite;
using Microsoft.Data.Sqlite;
using umbraco.Services;
WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IEmailMessageService, EmailMessageService>();

// Register the IDbConnection with the appropriate SQLite connection
builder.Services.AddScoped<IDbConnection>(sp =>
{
    return new SqliteConnection("Data Source=Umbraco.sqlite.db");
});

builder.CreateUmbracoBuilder()
    .AddBackOffice()
    .AddWebsite()
    .AddSlimsy()
    .AddDeliveryApi()
    .AddComposers()
    .Build();

builder.Services.Configure<umbracoConfig>(builder.Configuration.GetSection("Umbraco:CMS:Global:Smtp"));
WebApplication app = builder.Build();

await app.BootUmbracoAsync();


app.UseUmbraco()
    .WithMiddleware(u =>
    {
        u.UseBackOffice();
        u.UseWebsite();
    })
    .WithEndpoints(u =>
    {
        u.UseInstallerEndpoints();
        u.UseBackOfficeEndpoints();
        u.UseWebsiteEndpoints();
    });

await app.RunAsync();