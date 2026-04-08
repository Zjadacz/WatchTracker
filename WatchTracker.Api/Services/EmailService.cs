using sib_api_v3_sdk.Api;
using sib_api_v3_sdk.Client;
using sib_api_v3_sdk.Model;
using Task = System.Threading.Tasks.Task;

namespace WatchTracker.Api.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
            Configuration.Default.ApiKey["api-key"] = _config["Brevo:ApiKey"];
        }

        public async Task SendEmailAsync(string toEmail, string subject, string htmlContent)
        {
            var apiInstance = new TransactionalEmailsApi();

            var sendSmtpEmail = new SendSmtpEmail(
                sender: new SendSmtpEmailSender(
                    email: _config["Brevo:FromEmail"],
                    name: _config["Brevo:FromName"]
                ),
                to: new List<SendSmtpEmailTo> {
                    new SendSmtpEmailTo(toEmail)
                },
                subject: subject,
                htmlContent: htmlContent
            );

            await apiInstance.SendTransacEmailAsync(sendSmtpEmail);
        }
    }
}
