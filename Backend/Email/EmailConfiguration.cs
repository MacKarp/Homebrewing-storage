namespace Backend.Email
{
    public class EmailConfiguration : IEmailConfiguration
    {
        public string SmtpServer { get; set; }
        public int SmtpPort { get; set; }
        public bool Ssl { get; set; }
        public string SmtpUserName { get; set; }
        public string SmtpPassword { get; set; }
    }
}