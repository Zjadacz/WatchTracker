using System.Text;

namespace WatchTracker.Api.Services
{
    public class FileEmailService : IEmailService
    {
        private readonly string _dumpFileName;
        public FileEmailService(IConfiguration configuration)
        {
            _dumpFileName = configuration["EmailDumpFile"] ?? "";
        }

        public async Task SendEmailAsync(string toEmail, string subject, string htmlContent)
        {
            var sb = new StringBuilder();
            sb.AppendLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            sb.AppendLine($"To: {toEmail}");
            sb.AppendLine($"Subject: {subject}");
            sb.AppendLine(htmlContent);
            await File.AppendAllTextAsync(_dumpFileName, sb.ToString());
        }
    }
}
