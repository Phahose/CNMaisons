#nullable disable
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
        public List<string> errorMessage { get; set; } = new List<string>();
        public bool FlagError;


        [BindProperty]
        public string UserFirstName { get; set; } = string.Empty;
        
        [BindProperty]
        public string LastName { get; set; } = string.Empty;
        
        [BindProperty] 
        public string Email { get; set; } = string.Empty;

        [BindProperty] 
        public string AdminEmail { get; set; } = string.Empty;
        
        [BindProperty] 
        public string Password { get; set; } = string.Empty;
        [BindProperty] 
        public string ConfirmPassword { get; set; } = string.Empty;
        
        [BindProperty] 
        public string Role { get; set; } = string.Empty;

        [BindProperty]
        public string DefaultPassword { get; set; } = "true";

        [BindProperty]
        public IFormFile EmployeeImage { get; set; }
        public User Users { get; set; } = new User();
        public Employee Employee { get; set; } = new Employee();


        public void OnGet()
        {
            AdminEmail = HttpContext.Session.GetString("Email")!;
            CNMPMS controller = new CNMPMS();
            Users = controller.GetUserByEmail(AdminEmail);
            Employee = controller.GetAllEmployees(AdminEmail);
        }
        public void OnPost() 
        {

            AdminEmail = HttpContext.Session.GetString("Email")!;
            CNMPMS controller = new CNMPMS();
            Users = controller.GetUserByEmail(AdminEmail);
            Employee = controller.GetAllEmployees(AdminEmail);
            ModelState.Clear();

            if (string.IsNullOrEmpty(UserFirstName))
            {
                errorMessage.Add("Please Enter a Valid FirstName");
            }

            if (string.IsNullOrEmpty(LastName))
            {
                errorMessage.Add("Please Enter a Valid LastName");
            }

            if (string.IsNullOrEmpty(Email))
            {
                errorMessage.Add("Please Enter a Valid Email");
            }


            if (string.IsNullOrEmpty(Password))
            {
                errorMessage.Add("The Password is Required");
            }


            if (Role == "0")
            {
                errorMessage.Add("Please Select a Role For this User");
            }

            if (Password != ConfirmPassword)
            {
                errorMessage.Add("The Password and the Confirm Password Must Match");
            }


            if (ModelState.IsValid)
            {
                CNMPMS RequestDirector = new();

                User newUser = new();
                Employee newEmployee = new();

                byte[] employeeImageBytes = ConvertToByteArray(EmployeeImage);

                newEmployee.FirstName = UserFirstName;
                newEmployee.LastName = LastName;
                newUser.Email = Email;
                newUser.Password = Password;
                newUser.UserSalt = "placeholder";
                newUser.Role = Role;
                newUser.DefaultPassword = DefaultPassword;
                newEmployee.EmployeeImage = employeeImageBytes;
                string userConfirmationMessage = "Create Failed Error Occured";

                bool UserAccountConfirmation = false;

                Employee searchEmployee = RequestDirector.GetAllEmployees(Email);
                Tenant searchTenant = RequestDirector.GetAllTennants(Email);

                if (searchEmployee.FirstName != "")
                {
                    errorMessage.Add("This Email Already Exists as an Employee");
                }


                if (errorMessage.Count() == 0)
                {
                   UserAccountConfirmation = RequestDirector.CreateEmployeeAccount(newUser, newEmployee);
                }
               
                if (UserAccountConfirmation == true)
                {
                    Message = "User Account has been created succesfully.";
                }
                else
                {
                    Message = userConfirmationMessage;
                }
            }
        }

        private byte[] ConvertToByteArray(IFormFile file)
        {
            if (file != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    file.CopyTo(memoryStream);
                    return memoryStream.ToArray();
                }
            }
            else { return null; }
        }
    }
}
