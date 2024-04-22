using CNMaisons.Controller;
using CNMaisons.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace CNMaisons.Pages
{
    public class ForgotPasswordModel : PageModel
    {
        [BindProperty]
        public string Email { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public string AlertClass { get; set; } = string.Empty;

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            CNMPMS controller = new CNMPMS();
            var user = controller.GetUserByEmail(Email);
            if (user == null)
            {
                Message = "User with provided email does not exist.";
                AlertClass = "alert-danger";
                return Page();
            }

            // Generate a unique token
            var token = GenerateRandomCode(6);

            // Save token to the database
            //user.ResetPasswordToken = token;
           // user.ResetPasswordTokenExpiry = DateTime.UtcNow.AddHours(1); // Token expires in 1 hour
          //  _context.SaveChanges();

            // Send email to user with password reset link
            SendPasswordResetEmail(user.Email, token);

            Message = "Password reset link has been sent to your email.";
            AlertClass = "alert-success";
            return Page();
        }

        /* private string GenerateToken()
         {
             using (var rng = new RNGCryptoServiceProvider())
             {
                 var tokenData = new byte[32];
                 rng.GetBytes(tokenData);
                 return Convert.ToBase64String(tokenData);
             }
         }*/
        private string GenerateRandomCode(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        private void SendPasswordResetEmail(string email, string token)
        {
            // Code to send email to user with password reset link
            // You can use any email sending library or service here
            // Example: SendGrid, MailKit, SmtpClient, etc.
        }
    }
}
