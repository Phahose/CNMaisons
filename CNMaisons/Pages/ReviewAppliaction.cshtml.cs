using CNMaisons.Controller;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CNMaisons.Domain;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Buffers;
using CNMaisons.TechnicalService;
using Microsoft.AspNetCore.Mvc.Razor;

namespace CNMaisons.Pages
{


    public class ReviewModel : PageModel
    {
        public bool ViewFormNow = false;
        public string Message { get; set; } = string.Empty;
        public CNMS RequestDirector;

        public Tenant tenantForReview = new();

        [BindProperty]
        public string findTenantID { get; set; } = string.Empty;


        [BindProperty]
        public string Submit { get; set; } = string.Empty;

        public string ListMessage { get; set; } = string.Empty;
        public bool ShowForm = false;
        public List<Tenant> ListOfTenantsPendingReview = new List<Tenant>();


        public void OnGet()
        {
            CNMS tenantController = new();
            ListOfTenantsPendingReview = tenantController.GetPendingLeaseApplication();
            if (ListOfTenantsPendingReview == null)
            {
                ListMessage = "All have been reviewed";
            }
        }
        public IActionResult OnPost()
        {
            ModelState.Clear();
            Message = "";

            switch (Submit)
            {
                case "Close":
                    return RedirectToPage("/IndexMain");
                    break;

                case "Find":
                    if (findTenantID != null)
                    {
                        if (ModelState.IsValid)
                        {


                            CNMS RequestDirector = new();
                            tenantForReview = RequestDirector.ViewTenant(findTenantID);
                            if (tenantForReview == null)
                            {
                                ViewFormNow = true;
                                Message = "Below are the detail of the tenant 's Lease application.";
                                return Page();
                            }


                            //bool Confirmation = false;
                            //User newUserAccount = new()
                            //{


                            //};
                            //RequestDirector = new();
                            //Confirmation= RequestDirector.CreateUserAccount()
                            //int s = myScoreCard.Count;
                            //if (myScoreCard != null)
                            //{
                            //    Message = "Your Scores are lised below.";
                            //    return Page();
                            //}
                            //else
                            //{
                            //    Message = "MembershipID or Scores does not exist.";
                            //    return Page();
                            //}

                        }
                    }
                    return Page();
                    break;
            }
            return Page();
        }
    }
}

