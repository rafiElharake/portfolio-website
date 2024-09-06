namespace umbraco.Configuration
{
    public class umbracoConfig
    {
        public const string SectionName = "Umbraco:CMS:Global:Smtp";
        public SmtpSettings? Smtp { get; set; }
    }

    public class SmtpSettings
    {
        public string? From { get; set; }
        public string? Host { get; set; }
        public int Port { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? SecureSocketOptions { get; set; }
    }
}
