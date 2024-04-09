using CNMaisons.Controller;
using CNMaisons.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;

namespace CNMaisons.Pages
{
    [Authorize]
    public class IndexTenantModel : PageModel
    {
        public string Email { get; set; } = string.Empty;
        public User Users { get; set; } = new User();
        public Tenant Tenant { get; set; } = new Tenant();
        public void OnGet()
        {      
           Email = HttpContext.Session.GetString("Email")!;
           CNMPMS controller = new CNMPMS();
           Users = controller.GetUserByEmail(Email);

           Tenant = controller.ViewTenant(Email);
        }
    }
}
