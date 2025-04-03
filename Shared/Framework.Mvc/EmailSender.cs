using Framework.Mvc.Identity;
using Framework.Mvc.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;

namespace Framework.Mvc
{
    // This class is used by the application to send email for account confirmation and password reset.
    // For more details see https://go.microsoft.com/fwlink/?LinkID=532713
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            return Task.CompletedTask;
        }
    }

    public class NoOpIdentityEmailSender : IEmailSender<ApplicationUser>
    {
        private readonly IConfigurationRoot _configRoot;

        public NoOpIdentityEmailSender(IConfiguration configRoot)
        {
            _configRoot = (IConfigurationRoot)configRoot;
        }

        public Task SendConfirmationLinkAsync(ApplicationUser user, string email, string confirmationLink)
        {
            var clientUISettingSection = _configRoot.GetSection(nameof(ClientUISetting));
            var clientUISetting = clientUISettingSection.Get<ClientUISetting>();

            Uri uri = new Uri(confirmationLink);
            Uri uiUri = new Uri(new Uri(clientUISetting!.RootUrl), uri.PathAndQuery);
            System.Console.WriteLine(uiUri.ToString());
            return Task.CompletedTask;
        }

        public Task SendPasswordResetCodeAsync(ApplicationUser user, string email, string resetCode)
        {
            System.Console.WriteLine(resetCode);

            return Task.CompletedTask;
        }

        public Task SendPasswordResetLinkAsync(ApplicationUser user, string email, string resetLink)
        {
            var clientUISettingSection = _configRoot.GetSection(nameof(ClientUISetting));
            var clientUISetting = clientUISettingSection.Get<ClientUISetting>();

            Uri uri = new Uri(resetLink);
            Uri uiUri = new Uri(new Uri(clientUISetting!.RootUrl), uri.PathAndQuery);
            System.Console.WriteLine(uiUri.ToString());

            return Task.CompletedTask;
        }
    }
}

