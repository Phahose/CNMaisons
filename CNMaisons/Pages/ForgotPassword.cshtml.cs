using Azure.Core.Pipeline;
using CNMaisons.Controller;
using CNMaisons.Domain;
using CNMaisons.TechnicalService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Data;
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
        public Token NewToken { get; set; } = new();
        public string Disabled { get; set; } = "hideItem";
        public bool CodeSent { get; set; } = false;
        public bool CorrectcodeInput { get; set; } = false;
        public bool RequestCode {  get; set; } = true; 
        [BindProperty]
        public string Password { get; set; } = string.Empty;
        [BindProperty]
        public string PasswordConfirm { get; set; } = string.Empty;
        [BindProperty]
        public string Submit { get; set; } = string.Empty;
        [BindProperty]
        public string InputedToken { get; set; } = string.Empty;

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            CNMPMS controller = new CNMPMS();
            var user = controller.GetUserByEmail(Email);
    
            ModelState.Clear();
            switch (Submit)
            {
                case "Login":
                    if (!ModelState.IsValid)
                    {
                        return Page();
                    }


                    if (user == null)
                    {
                        Message = "User with provided email does not exist.";
                        AlertClass = "alert-danger";
                        return Page();
                    }

                    // Generate a unique token
                    var token = GenerateRandomCode(6);
                    HttpContext.Session.SetString("Code", token.ToString());
                    HttpContext.Session.SetString("ResetEmail", Email);

                    // Save token to the DB
                    NewToken = new()
                    {
                        Email = user.Email,
                        TokenCode = token.ToString(),
                        ExpiresAt = DateTime.Now.AddMinutes(10)
                    };

                    controller.AddToken(NewToken);

                    // Send email to user with password reset link
                    SendPasswordResetEmail(user.Email, token);

                    CodeSent = true;
                    Message = "Password reset link has been sent to your email.";
                    AlertClass = "alert-success";
                    RequestCode = false;

                    break;
                case "Confirm":
                  string code =  HttpContext.Session.GetString("Code")!;

                  Token userTokenInfo = new();
                  userTokenInfo =  controller.GetToken(code);

                    if (userTokenInfo.ExpiresAt < DateTime.Now)
                    {
                        Message = "This Code is Expired Request a New One and Try again";
                    }
                    else if (userTokenInfo.IsUsed == true)
                    {
                        Message = "This token has Already Been Used";
                    }
                    else if (InputedToken != userTokenInfo.TokenCode)
                    {
                        Message = "Invalid Token Please check your email and Enter in a Valid token";
                    }
                    else if (userTokenInfo.ExpiresAt > DateTime.Now  && userTokenInfo.IsUsed == false && InputedToken == userTokenInfo.TokenCode)
                    {
                        CodeSent = false;
                        CorrectcodeInput = true;            
                        RequestCode = false;
                    }                    
                  
                break;
                case "Update Password":
                    if (Password == string.Empty)
                    {
                        ModelState.AddModelError("PasswordError", "Must Enter a Value for Password");
                    }
                    if (PasswordConfirm == string.Empty)
                    {
                        ModelState.AddModelError("ConfirmPasswordError", "Must Confirm Your Password");
                    }
                    if (PasswordConfirm != Password)
                    {
                        ModelState.AddModelError("PasswordMatchError", "Passwords Must Match");
                    }

                    if (ModelState.IsValid)
                    {
                        string newemail = HttpContext.Session.GetString("ResetEmail")!;
                        User updatingUser = controller.GetUserByEmail(newemail);

                        User modifiedUser = new()
                        {
                            Email = updatingUser.Email,
                            Password = Password,
                            Role = updatingUser.Role,
                            DeactivateAccountStatus = updatingUser.DeactivateAccountStatus,
                            DefaultPassword = "false",
                        };
                        bool success = controller.ModifyUser(modifiedUser);


                    }
                    break;

            }
            return Page();
        }
           
        public string GenerateRandomCode(int length)
        {
            const string chars = "0123456789"; // Use only numbers
            var random = new Random();

        
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public void SendPasswordResetEmail(string email, string token)
        {
            // get the user
            CNMPMS RequestManager = new CNMPMS();
            var user = RequestManager.GetUserByEmail(Email);


            Message = $"Password Reset Confirmation ";
            string messageBody = "Hello " + user.Email + ",\n" +
                                    "\nYou Requested to Reset Your Password " +
                                    "\nYour code is " + $"{token}";
            string messageSubject = $"Password Reset Attempt";

            string mailConfirmation;
            RequestManager = new CNMPMS();
            mailConfirmation = RequestManager.PostEmail(Email, messageBody, messageSubject);
        }

    }
} 

