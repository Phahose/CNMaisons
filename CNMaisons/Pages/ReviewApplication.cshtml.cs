using CNMaisons.Controller;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CNMaisons.Domain;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Buffers;
using CNMaisons.TechnicalService;
using Microsoft.AspNetCore.Mvc.Razor;
using static System.Net.Mime.MediaTypeNames;
using System.Reflection;

namespace CNMaisons.Pages
{


    public class ReviewModel : PageModel
    {
        public bool ViewFormNow = false;
        public string Message { get; set; } = string.Empty;
        public string MessageForFile { get; set; } = string.Empty;
        public CNMPMS RequestDirector { get; set; } = new();

        public Tenant tenantForReview = new();

        [BindProperty]
        public string FindTenantID { get; set; } = string.Empty;

        [BindProperty]
        public string CurrentStatus { get; set; } = string.Empty;
        
        
        [BindProperty]
        public string Submit { get; set; } = string.Empty;

        [BindProperty]
        public string FindEmail { get; set; } = string.Empty; 

        public string ListMessage { get; set; } = string.Empty;
        public bool ShowForm = false;
        public List<Tenant> ListOfTenantsPendingReview = new List<Tenant>();

        [BindProperty]
        public string ApprovalStatus { get; set; } = string.Empty;


        [BindProperty]
        public IFormFile? LeaseFormForSigning { get; set; }


        [BindProperty]
        public IFormFile? LeaseFormForSigningCopy { get; set; }
        public void OnGet()
        {
            
            CNMPMS tenantController = new();
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
                    return RedirectToPage("/Index");
                

                case "Find":
                    if (FindTenantID != null)
                    {
                        ViewFormNow = false;
                        if (ModelState.IsValid)
                        {
                            SetSessionString("FindTenantID1", FindTenantID);  // save content for furtre retreival
                            SetSessionString("FindEmail1", FindEmail);  // save content for furtre retreival
                            

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
                    if (ApprovalStatus  != "--Make your selection--")
                    {
                        if (ModelState.IsValid)
                        {
                            FindTenantID = HttpContext.Session.GetString("FindTenantID1") ?? string.Empty;
                            FindEmail= HttpContext.Session.GetString("FindEmail1") ?? string.Empty;


                            if (ApprovalStatus == "Awaiting Signature" && LeaseFormForSigning != null)
                            {
                                byte[] LeaseForm = ConvertToByteArray(LeaseFormForSigning);

                                string messageBody = "Hello,\n\nYour Lease Form is ready. You need to \n\t1. login, \n\t2. Download the Lease Form,\n\t3. Sign and upload it ...\n\nOnce Uploaded, the contract will be finaised.\n\nRegards\nCN Maisons Management";
                                string messageSubject = "Sign this Lease Form and revert.";
                                string mailConfirmation;
                                CNMPMS MailRequestManager = new CNMPMS();
                                mailConfirmation = MailRequestManager.PostEmail(FindEmail, messageBody, messageSubject);
                                 



                                CNMPMS RequestDirector = new();
                                String Confirmation = RequestDirector.ReviewAwaitingApplication(FindTenantID, ApprovalStatus, LeaseForm);
                                if (Confirmation == "Successful!")
                                {
                                    ViewFormNow = false;
                                    MessageForFile = "Tenant's Lease application updaed for him to sign.";
                                    OnGet();
                                    return Page();
                                }
                            }
                            else
                            {
                                Message = "Please attach the lease form.";
                                ViewFormNow = true;
                                return Page();
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

