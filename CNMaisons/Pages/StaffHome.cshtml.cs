using CNMaisons.Controller;
using CNMaisons.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;

namespace CNMaisons.Pages
{
    [Authorize]
    public class StaffHomeModel : PageModel
    {
        public string Email { get; set; } = string.Empty;
        public Employee Employee { get; set; } = new Employee();
        public void OnGet()
        {      
           Email = HttpContext.Session.GetString("Email")!;
           BCS controller = new BCS();
           Employee = controller.GetUserByEmail(Email);
        }
    }
}
