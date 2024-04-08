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
        public bool ViewFormNow = false;
        public string Message { get; set; } = string.Empty;
        public CNMPMS RequestDirector;

        public Tenant aTenant = new();

     

        [BindProperty]
        public string Submit { get; set; } = string.Empty;

        public string ListMessage { get; set; } = string.Empty;
        public bool ShowForm = false;
        public List<Tenant> ListOfTenantsPendingReview = new List<Tenant>();

        [BindProperty]
        public string ApprovalStatus { get; set; } = string.Empty;


        public void OnGet()
        {
            string Email = HttpContext.Session.GetString("Email")!;
            var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            
            CNMPMS RequestDirector = new();
            aTenant = RequestDirector.ViewTenant(Email);
            string check = aTenant.TenantID;           
        }


        public IActionResult OnPost()
        {
            ModelState.Clear();
            Message = "";

            switch (Submit)
            {
                case "Close":
                    return RedirectToPage("Index");                    
                    break;

            }
            return Page();
        }
        void SetSessionString(string key, string value)
        {
            if (value != null)
            {
                HttpContext.Session.SetString(key, value);
            }
            else
            {
                HttpContext.Session.SetString(key, "");
            }
        }

    }
}
