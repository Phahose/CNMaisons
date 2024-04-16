using CNMaisons.Controller;
using CNMaisons.Domain;
using CNMaisons.TechnicalService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace CNMaisons.Pages
{
    [Authorize(Roles = "Tenant")]   // Restrict access to specified roles

    public class ViewTenantModel : PageModel
    {
        public string Message { get; set; } = string.Empty;
        [BindProperty]
        public string Submit { get; set; } = string.Empty;
        public Tenant aTenant { get; set; } = new();
        public User Users { get; set; } = new User();
        public void OnGet()
        {
            string Email = HttpContext.Session.GetString("Email")!;
            var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            
            CNMPMS RequestDirector = new();
            aTenant = RequestDirector.ViewTenant(Email);
            Users = RequestDirector.GetUserByEmail(Email);
            string check = aTenant.TenantID;           
        }


        public IActionResult OnPost()
        {
            ModelState.Clear();
            Message = "";

            switch (Submit)
            {
                case "Close":
                    return RedirectToPage("IndexTenant");                    

            }
            return Page();
        }
       

    }
}
