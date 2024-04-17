#nullable disable
using System.Net.Mail;
using System.Net;

namespace CNMaisons.TechnicalService
{
    public class Mails
    {
        private string password;
        private string smtpServer;
        private int port;
        private string fromEmail;
        public string ErrorMessage { get; set; } = string.Empty;


        public Mails()
        {

            ConfigurationBuilder EmailCredentialBuilder = new();
            EmailCredentialBuilder.SetBasePath(Directory.GetCurrentDirectory());
            EmailCredentialBuilder.AddJsonFile("appsettings.json");
            IConfiguration EmailCredentialConfiguration = EmailCredentialBuilder.Build();
            fromEmail = EmailCredentialConfiguration.GetConnectionString("From");
            password = EmailCredentialConfiguration.GetConnectionString("Password");
            smtpServer = EmailCredentialConfiguration.GetConnectionString("SmtpServer");

            string portString = EmailCredentialConfiguration.GetConnectionString("Port")!;
            if (int.TryParse(portString, out int Port))
            {
                port = Port;
            }

        }
        public string SendEmail(string toEmailAddress, string messageBody, string messageSubject)
        {
            string success;
            try
            {
                // Create the email client object
                using (SmtpClient client = new SmtpClient(smtpServer))
                {
                    client.Port = 587;
                    client.EnableSsl = true;


                    client.Credentials = new NetworkCredential(fromEmail, password);  //Ezra


                    // Create the email message
                    MailMessage message = new MailMessage(fromEmail, toEmailAddress);
                    message.Subject = messageSubject;
                    message.Body = messageBody;


                    // Send the email
                    client.Send(message);
                    success = "Successful.";

                }
            }
            catch (Exception ex)
            {
                success = $"Error sending email: {ex.Message}";
            }

            return success;
        }

        public static int GetEmailProvider(string emailAddress)
        {
            // Extract the domain part of the email address
            string[] parts = emailAddress.Split('@');
            if (parts.Length == 2)
            {
                string domain = parts[1].ToLower();

                // Check if the domain matches common email providers
                switch (domain)
                {
                    case "gmail.com":
                        return 587;
                    case "yahoo.com":
                        return 587;
                    case "hotmail.com":
                        return 587;
                    case "outlook.com":
                        return 587;
                    case "aol.com":
                        return 587;
                    case "icloud.com":
                        return 587;
                    case "live.com":
                        return 587;
                    case "mail.com":
                        return 587;
                    case "yandex.com":
                        return 587;
                    case "protonmail.com":
                        return 587;
                    default:
                        return 587;
                }
            }
            else
            {
                return 587;
            }
        }
    }
}
