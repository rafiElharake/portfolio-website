using System.Data; 
using Dapper; 
using umbraco.Models; 
namespace umbraco.Services;

public class EmailMessageService : IEmailMessageService
{
    private readonly IDbConnection _dbConnection;
    public EmailMessageService(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }
    public async Task SaveEmailLogAsync(EmailLog emailLog)
    {
        var query = "INSERT INTO EmailLogs (SenderEmail, Text, SentDate) VALUES (@SenderEmail, @Text, @SentDate)";
        await _dbConnection.ExecuteAsync(query, new
        {
            emailLog.SenderEmail,
            emailLog.Text,
            SentDate = emailLog.SentDate
        });
    }
}