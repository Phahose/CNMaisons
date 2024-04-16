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


    }
}
