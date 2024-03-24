using CNMaisons.Controller;
using CNMaisons.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CNMaisons.Pages
{
    public class CreateAccountModel : PageModel
    {
        
        public string Message { get; set; } = string.Empty;
        
        [BindProperty]
        public string FIrstName { get; set; } = string.Empty;
        
        [BindProperty]
        public string LastName { get; set; } = string.Empty;
        
        [BindProperty] 
        public string Email { get; set; } = string.Empty;
        
        [BindProperty] 
        public string Password { get; set; } = string.Empty;
        
        [BindProperty] 
        public string Role { get; set; } = string.Empty;
        
        
        [BindProperty] 
        public string DefaultPassword { get; set; } = string.Empty;
        
        

        public void OnGet()
        {
        }
        public void OnPost() 
        {
            CNMS RequestDirector = new();
            
            User newUser = new();
            newUser.FIrstName = FIrstName;
            newUser.LastName = LastName;
            newUser.Email = Email;
            newUser.Password = Password;
            newUser.UserSalt = "placeholder";
            newUser.Role = Role;
            newUser.DefaultPassword = DefaultPassword;

            string UserAccountConfirmation = RequestDirector.CreateUserAccount(newUser);
            if (UserAccountConfirmation == "Successful!")
            {
                Message = "User Account has been created succesfully.";
            }
            else
            {
                Message = UserAccountConfirmation;
            }


        }
    }
}
