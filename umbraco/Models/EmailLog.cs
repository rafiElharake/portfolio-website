namespace umbraco.Models;
public class EmailLog
{
    public int EmailId { get; set; }
    public string SenderEmail { get; set; } 
    public string Text { get; set; }
    public DateTime SentDate { get; set; }
}
