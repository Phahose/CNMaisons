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
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace CNMaisons.Pages
{
     
    [Authorize(Roles = "LandLord")]   // Restrict access to specified roles

    public class ReviewApplicationModel : PageModel
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

        public string UserRole { get; set; } = string.Empty;

        [BindProperty]
        public IFormFile? LeaseFormForSigningCopy { get; set; }
        public string UserEmail { get; set; } = string.Empty;
        public Employee Employee { get; set; } = new Employee();
        public User Users { get; set; } = new User();
        public void OnGet()
        {
            var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            CNMPMS tenantController = new();
            ListOfTenantsPendingReview = tenantController.GetPendingLeaseApplication();
            if (ListOfTenantsPendingReview == null)
            {
                ListMessage = "All have been reviewed";
            }
            if (HttpContext.Session.GetString("Email") != null)
            {
                UserEmail = HttpContext.Session.GetString("Email")!;
            }
            Users = tenantController.GetUserByEmail(UserEmail);
            Employee = tenantController.GetAllEmployees(UserEmail);
        }
        public IActionResult OnPost()
        {
            if (HttpContext.Session.GetString("Email") != null)
            {
                UserEmail = HttpContext.Session.GetString("Email")!;
            }
            Users = RequestDirector.GetUserByEmail(UserEmail);
            Employee = RequestDirector.GetAllEmployees(UserEmail);
            ModelState.Clear();
            Message = "";

            switch (Submit)
            {
                case "Close":
                    return RedirectToPage("/Index");
                    
                
                case "Refresh":
                    OnGet();
                    break;

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
                                OnGet();
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
                            FindEmail = HttpContext.Session.GetString("FindEmail1") ?? string.Empty;
                            string messageBody;
                            string messageSubject;
                            string mailConfirmation;
                            CNMPMS MailRequestManager = new CNMPMS();
                            String Confirmation;
                            CNMPMS RequestDirector = new CNMPMS();

                            if (ApprovalStatus == "Awaiting Signature" && LeaseFormForSigning != null)
                            {
                                byte[] LeaseForm = ConvertToByteArray(LeaseFormForSigning);

                                messageBody = "Hello,\n\nYour Lease Form is ready. You need to \n\t1. login, \n\t2. Download the Lease Form,\n\t3. Sign and upload it ...\n\nOnce Uploaded, the contract will be finaised.\n\nRegards\nCN Maisons Management";
                                messageSubject = "Sign this Lease Form and revert.";
                                mailConfirmation = MailRequestManager.PostEmail(FindEmail, messageBody, messageSubject);




                                Confirmation = RequestDirector.ReviewAwaitingApplication(FindTenantID, ApprovalStatus, LeaseForm);
                                if (Confirmation == "Successful!")
                                {
                                    ViewFormNow = false;
                                    MessageForFile = "Tenant's Lease application uploaded for him to sign.";
                                    OnGet();
                                    return Page();
                                }
                            }
                            //else
                            //{
                            //    Message = "Please attach the lease form.";
                            //    ViewFormNow = true;
                            //    return Page();
                            //}

                            if (ApprovalStatus == "Approved")
                            {
                                //byte[] LeaseForm = ConvertToByteArray(LeaseFormForSigning);
                                messageBody = "Hello,\n\nYour Lease Form has been APPROVED.\n\nYou can proceed with the payment.\n\tpay to :..........\n\nRegards\nCN Maisons Management";
                                messageSubject = "Lease Application Approved.";
                                MailRequestManager = new CNMPMS();
                                mailConfirmation = MailRequestManager.PostEmail(FindEmail, messageBody, messageSubject);

                            }

                            if (ApprovalStatus == "Rejected")
                            {
                                //byte[] LeaseForm = ConvertToByteArray(LeaseFormForSigning);
                                messageBody = "Hello,\n\nSorry we are unable toproceed with this lease agreement.\n\nRegards\nCN Maisons Management";
                                messageSubject = "Lease Agreement reject.";
                                MailRequestManager = new CNMPMS();
                                mailConfirmation = MailRequestManager.PostEmail(FindEmail, messageBody, messageSubject);

                            }

                            RequestDirector = new();
                            Confirmation = RequestDirector.ApproveOrRejectLeaseApplication(FindTenantID, ApprovalStatus);
                            if (Confirmation == "Successful!")
                            {
                                ViewFormNow = false;
                                MessageForFile = "Tenant's Lease application uploaded for him to sign.";
                                OnGet();
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

