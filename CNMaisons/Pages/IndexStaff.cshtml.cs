using CNMaisons.Controller;
using CNMaisons.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;

namespace CNMaisons.Pages
{
    [Authorize (Roles ="LandLord,Staff")]
    public class IndexStaffModel : PageModel
    {
        public string Email { get; set; } = string.Empty;
        [BindProperty]
        public string Submit { get; set; } = string.Empty;
        public User Users { get; set; } = new User();
        public Employee Employee { get; set; } = new Employee();
        public bool IsPasswordChaned { get; set; }
        [BindProperty]
        public string Password { get; set; } = string.Empty;
        [BindProperty]
        public string ConfirmPassword { get; set; } = string.Empty;
        [BindProperty]
        public string PasswordConfirm { get; set; } = string.Empty;
        public string SuccessMessage {  get; set; } = string.Empty;
        public string ErrorMessage {  get; set; } = string.Empty;
        public void OnGet()
        {      
           Email = HttpContext.Session.GetString("Email")!;
           CNMPMS controller = new CNMPMS();
           Users = controller.GetUserByEmail(Email);

           Employee = controller.GetAllEmployees(Email);
           if (Users.DefaultPassword != "true")
           {
               IsPasswordChaned = true;
           }
        }

        public void OnPost()
        {
            Email = HttpContext.Session.GetString("Email")!;
            CNMPMS controller = new CNMPMS();
            Users = controller.GetUserByEmail(Email);
            Employee = controller.GetAllEmployees(Email);

            ModelState.Clear();
            switch (Submit)
            {
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
                        
                        User modifiedUser = new()
                        {
                            Email = Users.Email,
                            Password = Password,
                            Role = Users.Role,
                            DeactivateAccountStatus = Users.DeactivateAccountStatus,
                            DefaultPassword = "false",
                        };
                        bool success= controller.ModifyUser(modifiedUser);
                        if (success == true)
                        {
                            IsPasswordChaned = true;
                            SuccessMessage = "Account Secured Successfully";
                        }
                        else
                        {
                            IsPasswordChaned = false;
                            ErrorMessage = "Failed Error Occured";
                        }
                    }
                break;
                case "Update":
                    if (Password == string.Empty)
                    {
                        ModelState.AddModelError("PasswordError", "Must Enter a Value for Password");
                    }
                    if (ConfirmPassword == string.Empty)
                    {
                        ModelState.AddModelError("ConfirmPasswordError", "Must Confirm Your Password");
                    }
                    if (ConfirmPassword != Password)
                    {
                        ModelState.AddModelError("PasswordMatchError", "Passwords Must Match");
                    }

                    if (ModelState.IsValid)
                    {

                        User modifiedUser = new()
                        {
                            Email = Users.Email,
                            Password = Password,
                            Role = Users.Role,
                            DeactivateAccountStatus = Users.DeactivateAccountStatus,
                            DefaultPassword = "false",
                        };
                        bool success = controller.ModifyUser(modifiedUser);
                        if (success == true)
                        {
                            IsPasswordChaned = true;
                            SuccessMessage = "Account Secured Successfully";
                        }
                        else
                        {
                            IsPasswordChaned = false;
                            ErrorMessage = "Failed Error Occured";
                        }
                    }
                    break;
            }
        }
    }
}
