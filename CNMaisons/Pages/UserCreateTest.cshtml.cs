using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CNMaisons.Controller;
using CNMaisons.Domain;

namespace CNMaisons.Pages
{
    public class UserCreateTestModel : PageModel
    {
        [BindProperty]
        public string Email { get; set; } = string.Empty;
        [BindProperty]
        public string Password { get; set; } = string.Empty;
        [BindProperty]
        public string Role { get; set; } = string.Empty;
        [BindProperty]
        public string Submit {  get; set; } = string.Empty;
        public void OnGet()
        {
        }

        public void OnPost() 
        {
            User user = new()
            {
                Email = Email,
                Password = Password,
                Role = Role,
            };
            CNMS controller = new CNMS();
            controller.AddUser(user);
            
        }
    }
}
