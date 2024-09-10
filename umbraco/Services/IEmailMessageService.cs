using umbraco.Models; 
namespace umbraco.Services;

public interface IEmailMessageService
{
    Task SaveEmailLogAsync(EmailLog emailLog);
}
