using CNMaisons.Controller;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CNMaisons.Domain;
using System.Security.Cryptography.X509Certificates;

namespace CNMaisons.Pages
{

    
    public class ReviewModel : PageModel
    {
        public string ListMessage { get; set; } = string.Empty;
        public bool ShowForm = false;
        public List<Tenant> ListOfTenantsPendingReview = new List<Tenant>();


        public void OnGet()
        {
            BCS tenantController = new();
            ListOfTenantsPendingReview = tenantController.GetPendingLeaseApplication();
            if (ListOfTenantsPendingReview == null)
            {
                ListMessage = "All have been reviewed";                
            }
        }
    }
}
