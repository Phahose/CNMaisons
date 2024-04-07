using CNMaisons.Controller;
using CNMaisons.Domain;
using CNMaisons.TechnicalService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CNMaisons.Pages
{
    public class SignLeaseFormModel : PageModel
    {
        public bool ViewFormNow = false;
        public string Message { get; set; } = string.Empty;
        public CNMPMS RequestDirector { get; set; } = new();

        public Tenant tenantForReview = new();

        [BindProperty]
        public string FindTenantID { get; set; } = string.Empty;


        [BindProperty]
        public string Submit { get; set; } = string.Empty;

        public string ListMessage { get; set; } = string.Empty;
        public bool ShowForm = false;
        public Tenant aTenantForReview = new Tenant();

        [BindProperty]
        public string ApprovalStatus { get; set; } = string.Empty;

        [BindProperty]
        public string Email { get; set; } = string.Empty;

        [BindProperty]
        public string User { get; set; } = string.Empty;

        [BindProperty]
        public IFormFile? LeaseFormForSigning { get; set; }

        public void OnGet()
        {
            Email = HttpContext.Session.GetString("Email")!;
            CNMPMS TenantRequestDirector = new();
            aTenantForReview = TenantRequestDirector.GetSpecificTenantApplication(Email);
            if (aTenantForReview == null)
            {
                ListMessage = "You have been reviewed";
            }
        }
        public IActionResult OnPost()
        {
            ModelState.Clear();
            Message = "";

            switch (Submit)
            {
                case "Close":
                    return RedirectToPage("/Index");


                case "Find":
                    if (FindTenantID != null)
                    {
                        ViewFormNow = false;
                        if (ModelState.IsValid)
                        {
                            SetSessionString("FindTenantID1", FindTenantID);  // save content for furtre retreival

                            CNMPMS RequestDirector = new();
                            tenantForReview = RequestDirector.ViewTenant(FindTenantID);
                            string name = tenantForReview.FirstName;
                            if (tenantForReview.FirstName != null)
                            {
                                ViewFormNow = true;
                                Message = "Below are the detail of the Tenant's Lease application.";
                                return Page();
                            }
                            else
                            {
                                Message = "There seems to be an error.";
                                ViewFormNow = false;
                            }
                        }
                    }
                    return Page();


                case "Submit Review":
                    if (ApprovalStatus != "Pending")
                    {
                        ViewFormNow = false;
                        if (ModelState.IsValid)
                        {
                            FindTenantID = HttpContext.Session.GetString("FindTenantID1") ?? string.Empty;

                            byte[] LeaseForm = ConvertToByteArray(LeaseFormForSigning);

                            //Form Signed
                            //Approved
                            //Rejected
                            if (ApprovalStatus == "Awaiting Signature")
                            {
                                CNMPMS RequestDirector = new();
                                String Confirmation = RequestDirector.ReviewAwaitingApplication(FindTenantID, ApprovalStatus, LeaseForm);
                                if (Confirmation == "Successful!")
                                {
                                    ViewFormNow = false;
                                    Message = "Tenant's Lease application reviewed.";
                                    OnGet();
                                    return Page();
                                }
                            }
                            





                        }


                    }
                    return Page();

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
