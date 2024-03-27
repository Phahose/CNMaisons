using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CNMaisons.Controller;
using CNMaisons.Domain;
using System.ComponentModel.DataAnnotations;

namespace CNMaisons.Pages
{
    public class CreateEmployeeModel : PageModel
    {

        [BindProperty]
        [Required]
        public string FirstName { get; set; }  = string.Empty;
        [BindProperty]
        [Required]
        public string LastName { get; set; } = string.Empty;
        [BindProperty]
        [Required]
        public int PhoneNumber { get; set; } 
        [BindProperty]
        [Required]
        public string Email { get; set; } = string.Empty;
        [BindProperty]
        [Required]
        public string Password { get; set; } = string.Empty;
        [BindProperty]
        [Required]
        public string Role { get; set; } = string.Empty;
        [BindProperty]
        public string Submit {  get; set; } = string.Empty;
        public string SuccessMessage {  get; set; } = string.Empty;
        public string SuccessClass {  get; set; } = string.Empty;
        public string ErrorMessage {  get; set; } = string.Empty;
        public string ErrorClass {  get; set; } = string.Empty;

        public void OnGet()
        {
        }

        public void OnPost() 
        {
            bool success = false;
            User user = new()
            {
                Email = Email,
                Password = Password,
                Role = Role,
            };
            CNMPMS RequestDirector = new CNMPMS();

            User searchUser = new();
            searchUser = RequestDirector.GetUserByEmail(Email); 


            if (searchUser.Email != "")
            {
                ErrorClass = "error_message";
                ErrorMessage = "This Employee Email Already Exists Try Signing In";
            }
            else
            {
                success = RequestDirector.CreateUserAccount(user);
                if (success == true)
                {
                    SuccessClass = "success_message";
                    SuccessMessage = "Employee Created Successfully";
                }
                if (success == false)
                {
                    ErrorClass = "error_message";
                    ErrorMessage = "Employee Could Not Be Created Error Occured";
                }
            }
            
        }
    }
}
