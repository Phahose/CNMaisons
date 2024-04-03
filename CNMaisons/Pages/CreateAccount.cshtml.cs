using CNMaisons.Controller;
using CNMaisons.Domain;
using CNMaisons.TechnicalService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CNMaisons.Pages
{
    public class CreateAccountModel : PageModel
    {
        
        public string Message { get; set; } = string.Empty;
        public string errorMessage { get; set; } = string.Empty;
        public bool FlagError;


        [BindProperty]
        public string UserFirstName { get; set; } = string.Empty;
        
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

            errorMessage = "";
            ModelState.Clear();

            if (string.IsNullOrEmpty(UserFirstName))
            {
                errorMessage += "The FirstName is Required.\n";
            }

            if (string.IsNullOrEmpty(LastName))
            {
                errorMessage += "The LastName is Required.\n";
            }

            if (string.IsNullOrEmpty(Email))
            {
                errorMessage += "The Email is Required.\n";
            }


            if (string.IsNullOrEmpty(Password))
            {
                errorMessage += "The Password is Required.\n";
            }


            if (string.IsNullOrEmpty(Role))
            {
                errorMessage += "The Role is Required.\n";
            }



            if (ModelState.IsValid)
            {
                CNMPMS RequestDirector = new();

                User newUser = new();
                newUser.FirstName = UserFirstName;
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
                    errorMessage = "";
                }
                else
                {
                    Message = UserAccountConfirmation;
                }


            }

        }
    }
}
