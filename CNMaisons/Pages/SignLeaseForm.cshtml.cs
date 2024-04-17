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

    public class SignLeaseFormModel : PageModel
    {
        public bool ViewFormNow = false;
        public string Message { get; set; } = string.Empty;
        public string MessageForFile { get; set; } = string.Empty;
        public CNMPMS RequestDirector { get; set; } = new();

        public Tenant tenantForReview = new();

        [BindProperty]
        public string FindEmail { get; set; } = string.Empty;

        [BindProperty]
        public string FindTenantID { get; set; } = string.Empty;

        [BindProperty]
        public string CurrentStatus { get; set; } = string.Empty;


        [BindProperty]
        public string Submit { get; set; } = string.Empty;

        public string ListMessage { get; set; } = string.Empty;
        public bool ShowForm = false;
        public Tenant aTenantsPendingReview = new Tenant();

        [BindProperty]
        public string ApprovalStatus { get; set; } = string.Empty;

        [BindProperty]
        public string Email { get; set; } = string.Empty;


        [BindProperty]
        public IFormFile? LeaseFormForSigning { get; set; }


        [BindProperty]
        public IFormFile? YourSignedForm { get; set; }

        public string UserEmail { get; set; } = string.Empty;
        public Employee Employee { get; set; } = new Employee();
        public User Users { get; set; } = new User();
        public void OnGet()
        {
            Email = HttpContext.Session.GetString("Email")!;
            var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            CNMPMS tenantController = new();
            aTenantsPendingReview = tenantController.GetSpecificTenantApplication(Email);
            if (aTenantsPendingReview == null)
            {
                ListMessage = "All have been reviewed";
            }
            if (HttpContext.Session.GetString("Email") != null)
            {
                UserEmail = HttpContext.Session.GetString("Email")!;
            }
            Users = tenantController.GetUserByEmail(UserEmail);
            Employee = tenantController.GetAllEmployees(UserEmail);
            tenantForReview = tenantController.GetAllTennants(UserEmail);
        }
        public IActionResult OnPost()
        {
            Email = HttpContext.Session.GetString("Email")!;
            var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            CNMPMS tenantController = new();
            aTenantsPendingReview = tenantController.GetSpecificTenantApplication(Email);
            tenantForReview = tenantController.GetAllTennants(UserEmail);
            if (aTenantsPendingReview == null)
            {
                ListMessage = "All have been reviewed";
            }
            if (HttpContext.Session.GetString("Email") != null)
            {
                UserEmail = HttpContext.Session.GetString("Email")!;
            }
            Users = tenantController.GetUserByEmail(UserEmail);
            Employee = tenantController.GetAllEmployees(UserEmail);
            ModelState.Clear();
            Message = "";

            switch (Submit)
            {
                case "Close":
                    return RedirectToPage("/IndexTenant");


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
                                Message = "";
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
                    if (ApprovalStatus == "Signed" && YourSignedForm != null)
                    {
                        if (ModelState.IsValid)
                        {
                            FindTenantID = HttpContext.Session.GetString("FindTenantID1") ?? string.Empty;


                            byte[] SignedForm = ConvertToByteArray(YourSignedForm);

                            CNMPMS RequestDirector = new();
                            String Confirmation = RequestDirector.SubmitSignedCopy(FindTenantID, ApprovalStatus, SignedForm);
                            if (Confirmation == "Successful!")
                            {
                                ViewFormNow = false;
                                MessageForFile = "Tenant's Lease application uploaded him to sign.";
                                OnGet();
                                return Page();
                            }
                        }
                    }
                    else
                    {
                        Message = "Please attach the signed lease form.";
                        ViewFormNow = true;
                        return Page();
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
            else { return new byte[0]; }
        }
    }
}